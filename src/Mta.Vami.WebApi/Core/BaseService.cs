using Microsoft.Extensions.Logging;
using Mta.Vami.WebApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public abstract class BaseService
    {
        public WorkingContext Context { get; set; }

        public ILogger Logger => Context.Logger;

        protected BaseService(WorkingContext context)
        {
            Context = context;
        }


        protected void SetCreatedInfo(BaseEntity entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.CreatedUser = Context.UserName;
            entity.UpdatedUser = "";
            entity.UpdatedTime = DateTime.Now;
        }

        protected void SetUpdatedInfo(BaseEntity entity, BaseEntity origin = null)
        {
            entity.UpdatedUser = Context.UserName;
            entity.UpdatedTime = DateTime.Now;

            if(origin != null)
            {
                entity.CreatedTime = origin.CreatedTime;
                entity.CreatedUser = origin.CreatedUser;
            }    
        }
    }

    public interface ICRUDService<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
    {
        WorkingContext Context { get; set; }

        Result<TEntity> Create(TEntity entity);

        Result Delete(TPrimaryKey key);

        Result<TEntity> Update(TEntity entity);

        TEntity Get(TPrimaryKey key);

        Result Validate(TEntity entity, ActionType action, TEntity originEntity = null);
    }

    public interface ICRUDService<TSearchParameter, TEntity, TPrimaryKey> : ICRUDService<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
                                                                                        where TSearchParameter : BaseSearchRequest
    {
        Result<PaginationSet<TEntity>> SearchPaging(TSearchParameter parameter);

        Result<List<TEntity>> Search(TSearchParameter parameter);
    }

    public abstract class BaseCRUDService<TEntity, TPrimaryKey, TRepository> : BaseService, ICRUDService<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
                                                                                            where TRepository : SqlRepository<TEntity, TPrimaryKey>, new()
    {
        protected TRepository _repo;

        public BaseCRUDService(WorkingContext context) : base(context)
        {
            _repo = new TRepository();
        }

        public virtual Result<TEntity> Create(TEntity entity)
        {
            SetCreatedInfo(entity);
            var rs = Validate(entity, ActionType.Create);
            if (rs.IsError())
            {
                return rs.To<TEntity>();
            }

            _repo.Insert(entity);
            return Result.Ok<TEntity>(entity);
        }

        public virtual Result Delete(TPrimaryKey key)
        {
            var entity = new TEntity
            {
                Id = key
            };

            var rs = Validate(entity, ActionType.Delete);
            if (rs.IsError())
            {
                return rs;
            }

            _repo.Delete(entity);
            return Result.Ok();
        }

        public virtual Result<TEntity> Update(TEntity entity)
        {
            var originEntity = Get(entity.Id);
            SetUpdatedInfo(entity);


            var rs = Validate(entity, ActionType.Update, originEntity);
            if (rs.IsError())
            {
                return rs.To<TEntity>();
            }

            entity.CreatedTime = originEntity.CreatedTime;
            entity.CreatedUser = originEntity.CreatedUser;
            _repo.Update(entity);
            return Result.Ok(entity);
        }

        public virtual TEntity Get(TPrimaryKey key)
        {
            return _repo.Get(key);
        }

        public virtual Result Validate(TEntity entity, ActionType action, TEntity originEntity = null)
        {
            if (action == ActionType.Update)
            {
                if (originEntity == null)
                {
                    return Result.Error("Dữ liệu cần update không tồn tại");
                }
            }

            return Result.Ok();
        }
    }

    public abstract class BaseCRUDService<TSearchParameter, TEntity, TPrimaryKey, TRepository> : BaseCRUDService<TEntity, TPrimaryKey, TRepository>, ICRUDService<TSearchParameter, TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
                                                                                         where TRepository : SqlRepository<TEntity, TPrimaryKey>, new()
                                                                                         where TSearchParameter : BaseSearchRequest
    {
        public BaseCRUDService(WorkingContext context) : base(context)
        {
        }

        public virtual Result<PaginationSet<TEntity>> SearchPaging(TSearchParameter parameter)
        {
            var query = _repo.GetTable();
            var rs = ApplySearchParameter(parameter, query);
            if (rs.IsError())
            {
                return rs.To<PaginationSet<TEntity>>();
            }

            query = rs.Data;
            var pageResult = query.ToPaging(parameter.Page, parameter.PageSize);
            return Result.Ok(pageResult);
        }

        public virtual Result<List<TEntity>> Search(TSearchParameter parameter)
        {
            var query = _repo.GetTable();
            var rs = ApplySearchParameter(parameter, query);
            if (rs.IsError())
            {
                return rs.To<List<TEntity>>();
            }

            query = rs.Data;
            var lst = query.ToList();
            return Result.Ok(lst);
        }

        public abstract Result<IQueryable<TEntity>> ApplySearchParameter(TSearchParameter parameter, IQueryable<TEntity> query);
    }
}
