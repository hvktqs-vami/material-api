using Mta.Vami.WebApi.Entities;
using Mta.Vami.WebApi.Repositories;
using Mta.Vami.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mta.Vami.WebApi.Core;

namespace Mta.Vami.WebApi.Services
{
    public class MaterialService : BaseService
    {
        private MaterialRepository _repo;
        private MaterialContentRepository _repoContent;
        private MaterialChemicalRepository _repoChemical;
        private MaterialChemicalTypeRepository _repoChemicalType;
        private MaterialEquivalentRepository _repoEquiv;
        private MaterialGroupRepository _repoGroup;
        private MaterialHighFatigueRepository _repoHighFatigue;
        private MaterialHighTempMecPropRepository _repoHighTempMecProp;
        private MaterialLowFatigueRepository _repoLowFatigue;
        private MaterialMechanicalPropGroupRepository _repoMechanicalPropGroup;
        private MaterialMechanicalPropRepository _repoMechanicalProp;
        private MaterialMechanicalPropTypeRepository _repoMechanicalPropType;
        private MaterialStandardRepository _repoStandard;
        private MaterialSubGroupRepository _repoSubGroup;
        private MaterialUsageRepository _repoUsage;

        public MaterialService(WorkingContext<MaterialService> context) : base(context)
        {
            _repo = new MaterialRepository();
            _repoContent = new MaterialContentRepository();
            _repoChemical = new MaterialChemicalRepository();
            _repoChemicalType = new MaterialChemicalTypeRepository();
            _repoEquiv = new MaterialEquivalentRepository();
            _repoGroup = new MaterialGroupRepository();
            _repoHighFatigue = new MaterialHighFatigueRepository();
            _repoHighTempMecProp = new MaterialHighTempMecPropRepository();
            _repoLowFatigue = new MaterialLowFatigueRepository();
            _repoMechanicalPropGroup = new MaterialMechanicalPropGroupRepository();
            _repoMechanicalProp = new MaterialMechanicalPropRepository();
            _repoMechanicalPropType = new MaterialMechanicalPropTypeRepository();
            _repoStandard = new MaterialStandardRepository();
            _repoSubGroup = new MaterialSubGroupRepository();
            _repoUsage = new MaterialUsageRepository();
        }

        public TModel Convert<TModel>(Material entity) where TModel : MaterialModel, new()
        {
            var model = new TModel
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                CreatedTime = entity.CreatedTime,
                CreatedUser = entity.CreatedUser,
                GroupId = entity.GroupId,
                HeatTreatment = entity.HeatTreatment,
                Name = entity.Name,
                StandardId = entity.StandardId,
                SubGroupId = entity.SubGroupId,
                UpdatedTime = entity.UpdatedTime,
                UpdatedUser = entity.UpdatedUser
            };

            return model;
        }

        public Material Convert<TModel>(TModel model) where TModel : MaterialModel, new()
        {
            var entity = new Material
            {
                Id = model.Id,
                CountryId = model.CountryId,
                CreatedTime = model.CreatedTime,
                CreatedUser = model.CreatedUser,
                GroupId = model.GroupId,
                HeatTreatment = model.HeatTreatment,
                Name = model.Name,
                StandardId = model.StandardId,
                SubGroupId = model.SubGroupId,
                UpdatedTime = model.UpdatedTime,
                UpdatedUser = model.UpdatedUser
            };

            return entity;
        }

        public Result<MaterialDetailModel> Get(long id)
        {
            var entity = _repo.Get(id);
            if (entity == null)
            {
                return Result.Error<MaterialDetailModel>(string.Format("Không tìm thấy Material: {0}", id));
            }

            var model = Convert<MaterialDetailModel>(entity);

            if (model.GroupId > 0)
            {
                var group = _repoGroup.Get(model.GroupId);
                model.GroupName = group == null ? "" : group.Name;
            }

            if (model.SubGroupId > 0)
            {
                var subGroup = _repoSubGroup.Get(model.SubGroupId);
                model.SubGroupName = subGroup == null ? "" : subGroup.Name;
            }

            if (model.StandardId > 0)
            {
                var standard = _repoStandard.Get(model.StandardId);
                model.StandardName = standard == null ? "" : standard.Name;
            }

            var content = _repoContent.GetTable().Where(x => x.MaterialId == model.Id).FirstOrDefault();
            if (content == null)
            {
                content = new MaterialContent();
                content.MaterialId = model.Id;
            }

            model.Content = content;

            var lstChemical = _repoChemical.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.ChemicalElements = lstChemical;

            var lstHighFatigue = _repoHighFatigue.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.HighFatigues = lstHighFatigue;

            var lstLowFatigue = _repoLowFatigue.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.LowFatigues = lstLowFatigue;

            var lstHighTempMec = _repoHighTempMecProp.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.HighTempMecProps = lstHighTempMec;

            var lstMechanical = _repoMechanicalProp.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.MechanicalProps = lstMechanical;

            var lstEquiv = _repoEquiv.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.Equivalents = lstEquiv;

            var lstUsage = _repoUsage.GetTable().Where(x => x.MaterialId == model.Id).ToList();
            model.Usages = lstUsage;

            return Result.Ok(model);
        }


        public Result<MaterialDetailModel> Create(MaterialSaveRequest request)
        {
            var entity = Convert(request);
            SetCreatedInfo(entity);

            using (var uow = new UnitOfWork(_repo))
            {
                try
                {
                    _repo.Insert(entity, uow);

                    CommitDetail(_repoChemical, request.ChemicalElements, null, entity.Id, uow);
                    CommitDetail(_repoUsage, request.Usages, null, entity.Id, uow);
                    CommitDetail(_repoEquiv, request.Equivalents, null, entity.Id, uow);
                    CommitDetail(_repoHighFatigue, request.HighFatigues, null, entity.Id, uow);
                    CommitDetail(_repoLowFatigue, request.LowFatigues, null, entity.Id, uow);
                    CommitDetail(_repoMechanicalProp, request.MechanicalProps, null, entity.Id, uow);
                    CommitDetail(_repoHighTempMecProp, request.HighTempMecProps, null, entity.Id, uow);

                    if (request.Content != null)
                    {
                        SetCreatedInfo(request.Content);
                        request.Content.MaterialId = entity.Id;
                        _repoContent.Insert(request.Content, uow);
                    }

                    uow.Commit();
                }
                catch (Exception ex)
                {
                    return Result.Exception<MaterialDetailModel>(string.Format("Lỗi khi thêm Material: {0}", ex.Message), ex);
                }
            }



            var rsGet = Get(entity.Id);
            return rsGet;
        }

        public Result<MaterialDetailModel> Update(MaterialSaveRequest request)
        {
            var rsGet = Get(request.Id);
            if (rsGet.IsError())
            {
                return rsGet;
            }

            var origin = rsGet.Data;
            var entity = Convert(request);
            SetUpdatedInfo(entity);
            entity.CreatedTime = origin.CreatedTime;
            entity.CreatedUser = origin.CreatedUser;

            using (var uow = new UnitOfWork(_repo))
            {
                try
                {
                    _repo.Update(entity, uow);

                    CommitDetail(_repoChemical, request.ChemicalElements, origin.ChemicalElements, entity.Id, uow);
                    CommitDetail(_repoUsage, request.Usages, origin.Usages, entity.Id, uow);
                    CommitDetail(_repoEquiv, request.Equivalents, origin.Equivalents, entity.Id, uow);
                    CommitDetail(_repoHighFatigue, request.HighFatigues, origin.HighFatigues, entity.Id, uow);
                    CommitDetail(_repoLowFatigue, request.LowFatigues, origin.LowFatigues, entity.Id, uow);
                    CommitDetail(_repoMechanicalProp, request.MechanicalProps, origin.MechanicalProps, entity.Id, uow);
                    CommitDetail(_repoHighTempMecProp, request.HighTempMecProps, origin.HighTempMecProps, entity.Id, uow);

                    if (request.Content != null)
                    {
                        request.Content.MaterialId = entity.Id;

                        if (origin.Content != null)
                        {
                            SetUpdatedInfo(request.Content, origin.Content);
                            _repoContent.Update(request.Content, uow);
                        }
                        else
                        {
                            SetCreatedInfo(request.Content);
                            _repoContent.Insert(request.Content, uow);
                        }
                    }

                    uow.Commit();
                }
                catch (Exception ex)
                {
                    return Result.Exception<MaterialDetailModel>(string.Format("Lỗi khi cập nhật Material: {0}", ex.Message), ex);
                }
            }

            rsGet = Get(entity.Id);
            return rsGet;
        }

        protected void CommitDetail<TEntity, TPrimaryKey>(SqlRepository<TEntity, TPrimaryKey> repo, List<TEntity> lstNew, List<TEntity> lstOrigin, long materialId, UnitOfWork uow = null) where TEntity : BaseEntity<TPrimaryKey>
        {
            var prop = typeof(TEntity).GetProperty(nameof(MaterialContent.MaterialId));

            if (lstNew == null)
            {
                lstNew = new List<TEntity>();
            }

            if (lstOrigin == null || lstOrigin.Count == 0)
            {
                foreach (var obj in lstNew)
                {
                    SetCreatedInfo(obj);
                    prop.SetValue(obj, materialId);
                    repo.Insert(obj, uow);
                }

                return;
            }

            var lstNewId = lstNew.Select(x => x.Id).Distinct().ToList();
            var dicOrigin = lstOrigin.ToDictionary(x => x.Id);
            var lstDelete = lstOrigin.Where(x => !lstNewId.Contains(x.Id)).ToList();
            foreach (var obj in lstDelete)
            {
                repo.Delete(obj, uow);
            }

            if (lstNew.Count == 0)
            {
                return;
            }

            var defaultValue = default(TPrimaryKey);
            foreach (var obj in lstNew)
            {
                prop.SetValue(obj, materialId);

                if (obj.Id.Equals(defaultValue))
                {
                    SetCreatedInfo(obj);
                    repo.Insert(obj, uow);
                }
                else
                {
                    var origin = dicOrigin[obj.Id];
                    SetUpdatedInfo(obj, origin);
                    repo.Update(obj, uow);
                }
            }
        }

        public Result Delete(long id)
        {
            using (var uow = new UnitOfWork(_repo))
            {
                try
                {
                    _repoChemical.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoUsage.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoEquiv.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoContent.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoHighFatigue.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoLowFatigue.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoMechanicalProp.DeleteWhere(x => x.MaterialId == id, uow);
                    _repoHighTempMecProp.DeleteWhere(x => x.MaterialId == id, uow);

                    _repo.Delete(new Material { Id = id });

                    uow.Commit();
                }
                catch (Exception ex)
                {
                    return Result.Exception(string.Format("Lỗi khi xóa Material {0}: {1}", id, ex.Message), ex);
                }
            }

            return Result.Ok();
        }


        public virtual Result<PaginationSet<MaterialModel>> SearchPaging(MaterialSearchRequest parameter)
        {
            var query = _repo.GetTable();
            var rs = ApplySearchParameter(parameter, query);
            if (rs.IsError())
            {
                return rs.To<PaginationSet<MaterialModel>>();
            }

            query = rs.Data;

           var pageEntityResult = query.ToPaging(parameter.Page, parameter.PageSize);

            var pageResult = pageEntityResult.ToResult(x => Convert<MaterialModel>(x));

            if (pageResult.Items.Count > 0)
            {
                var lstGroup = _repoGroup.GetTable().ToList().ToDictionary(x => x.Id);
                var lstSubGroup = _repoSubGroup.GetTable().ToList().ToDictionary(x => x.Id);
                var lstStandard = _repoStandard.GetTable().ToList().ToDictionary(x => x.Id);

                foreach (var obj in pageResult.Items)
                {
                    obj.GroupName = "";
                    obj.SubGroupName = "";
                    obj.StandardName = "";
                    if (obj.GroupId > 0)
                    {
                        if (lstGroup.TryGetValue(obj.GroupId, out var group))
                        {
                            obj.GroupName = group.Name;
                        }
                    }

                    if (obj.SubGroupId > 0)
                    {
                        if (lstSubGroup.TryGetValue(obj.SubGroupId, out var subgroup))
                        {
                            obj.SubGroupName = subgroup.Name;
                        }
                    }

                    if (obj.StandardId > 0)
                    {
                        if (lstStandard.TryGetValue(obj.StandardId, out var standard))
                        {
                            obj.StandardName = standard.Name;
                        }
                    }
                }
            }

            return Result.Ok(pageResult);
        }

        public virtual Result<List<MaterialModel>> Search(MaterialSearchRequest parameter)
        {
            var query = _repo.GetTable();
            var rs = ApplySearchParameter(parameter, query);
            if (rs.IsError())
            {
                return rs.To<List<MaterialModel>>();
            }

            query = rs.Data;
            var lst = query.ToList();
            var lstResult = lst.Select(x => Convert<MaterialModel>(x)).ToList();

            if (lstResult.Count > 0)
            {
                var lstGroup = _repoGroup.GetTable().ToList().ToDictionary(x => x.Id);
                var lstSubGroup = _repoSubGroup.GetTable().ToList().ToDictionary(x => x.Id);
                var lstStandard = _repoStandard.GetTable().ToList().ToDictionary(x => x.Id);

                foreach (var obj in lstResult)
                {
                    obj.GroupName = "";
                    obj.SubGroupName = "";
                    obj.StandardName = "";
                    if (obj.GroupId > 0)
                    {
                        if (lstGroup.TryGetValue(obj.GroupId, out var group))
                        {
                            obj.GroupName = group.Name;
                        }
                    }

                    if (obj.SubGroupId > 0)
                    {
                        if (lstSubGroup.TryGetValue(obj.SubGroupId, out var subgroup))
                        {
                            obj.SubGroupName = subgroup.Name;
                        }
                    }

                    if (obj.StandardId > 0)
                    {
                        if (lstStandard.TryGetValue(obj.StandardId, out var standard))
                        {
                            obj.StandardName = standard.Name;
                        }
                    }
                }
            }


            return Result.Ok(lstResult);
        }

        public static readonly List<string> AllowSearchFields = new List<string>
        {
            nameof(Material.CountryId),
            nameof(Material.GroupId),
            nameof(Material.SubGroupId),
            nameof(Material.StandardId)
        };

        public virtual Result<IQueryable<Material>> ApplySearchParameter(MaterialSearchRequest parameter, IQueryable<Material> query)
        {
            var builder = new PredicateBuilder<Material>();
            if (parameter.ListExpression != null && parameter.ListExpression.Count > 0)
            {
                var rsValidate = parameter.ListExpression.Validate(AllowSearchFields);
                if(rsValidate.IsError())
                {
                    return rsValidate.To<IQueryable<Material>>();
                }

                var expWhere = parameter.ListExpression.ToExpression<Material>();
                builder.And(expWhere);
            }

            var exp = builder.Build();
            if (exp != null)
            {
                query = query.Where(exp);
            }

            if (parameter.OrderBys != null && parameter.OrderBys.Count > 0)
            {
                query = query.OrderBy(parameter.OrderBys);
            }

            return Result.Ok(query);
        }
    }
}
