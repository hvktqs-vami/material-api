using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using LinqToDB;
using System.Net.WebSockets;
using LinqToDB.Data;
using LinqToDB.Mapping;
using LinqToDB.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
namespace Mta.Vami.WebApi.Core
{
    public class SqlRepository
    {
        protected DataProvider _dataProvider;

        public string SettingName { get; protected set; }

        public virtual UnitDatasource GetUnitDataSource(UnitOfWork uow = null)
        {
            UnitDatasource rs;
            if (uow != null && uow.Sources.Contains(SettingName))
            {
                rs = uow.Sources[SettingName];
            }
            else
            {
                var connection = _dataProvider.CreateDataConnection();

                rs = new UnitDatasource
                {
                    Connection = connection,
                    Name = SettingName
                };
            }
            rs.Connection.Connection.EnsureOpen();
            return rs;
        }

        public SqlRepository(string settingName)
        {
            SettingName = settingName;
            InitConfig();
        }

        protected virtual void InitConfig()
        {
            _dataProvider = new DataProvider(SettingName);
        }


        #region Execute Method

        public virtual int ExecuteNonQuery(string sqlText, DataParameter[] parameters, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                var command = _dataProvider.GetCommand(cnn.Connection, sqlText, parameters);
                return command.Execute();
            }
        }

        public virtual int ExecuteStoredProc(string procName, DataParameter[] parameters, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                var command = _dataProvider.GetCommand(cnn.Connection, procName, parameters);
                return command.ExecuteProc();
            }
        }

        public virtual T Execute<T>(string sqlText, DataParameter[] parameters, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                var command = _dataProvider.GetCommand(cnn.Connection, sqlText, parameters);
                var rs = command.Execute<T>();
                return rs;
            }
        }

        public virtual T ExecuteStoredProc<T>(string procName, DataParameter[] parameters, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                var command = _dataProvider.GetCommand(cnn.Connection, procName, parameters);
                var rs = command.ExecuteProc<T>();
                return rs;
            }
        }

        public virtual List<T> Query<T>(string sqlText, DataParameter[] parameters, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                var rs = cnn.Connection.Query<T>(sqlText, parameters).ToList();
                return rs;
            }
        }


        public virtual List<T> QueryStoredProc<T>(string procName, DataParameter[] parameters, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                var rs = cnn.Connection.QueryProc<T>(procName, parameters).ToList();
                return rs;
            }
        }

        #endregion
    }

    public class SqlRepository<TEntity, TPrimaryKey> : SqlRepository where TEntity : BaseEntity<TPrimaryKey>
    {

        protected EntityDescriptor EntityDescriptor { get; set; }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> GetTable(UnitOfWork uow = null)
        {
            return _dataProvider.GetTable<TEntity>(uow);
        }

        protected ColumnDescriptor PrimaryKeyField { get; set; }

        protected bool HasIdentity
        {
            get { return PrimaryKeyField != null && PrimaryKeyField.IsIdentity; }
        }

        public SqlRepository(string settingName) : base(settingName)
        {
            EntityDescriptor = _dataProvider.GetEntityDescriptor<TEntity>();
            PrimaryKeyField = EntityDescriptor.Columns.FirstOrDefault(x => x.IsPrimaryKey);
        }

        public TEntity Get(TPrimaryKey key, UnitOfWork uow = null)
        {
            var rs = GetTable(uow: uow).Where(x=>x.Id.Equals(key)).FirstOrDefault();
            return rs;
        }

        public bool Insert(TEntity entity, UnitOfWork uow = null)
        {        
            using (var cnn = GetUnitDataSource(uow))
            {
                if (HasIdentity)
                {
                    if(typeof(TPrimaryKey) == typeof(int))
                    {
                        var id = cnn.Connection.InsertWithInt32Identity(entity) as object;
                        entity.Id = (TPrimaryKey)id;
                    }    
                    else
                    {
                        var id = cnn.Connection.InsertWithInt64Identity(entity) as object;
                        entity.Id = (TPrimaryKey)id;
                    }                        
                }
                else
                {
                    cnn.Connection.Insert(entity);
                }

                return true;
            }
        }

        public bool BulkInsert(List<TEntity> list, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                cnn.Connection.BulkCopy(list);
                return true;
            }
        }

        public bool Update(TEntity entity, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                cnn.Connection.Update(entity);
                return true;
            }
        }

        public bool BulkUpdate(List<TEntity> list, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                foreach (var entity in list)
                {
                    cnn.Connection.Update(entity);
                }
                return true;
            }
        }

        public bool UpdateWhere(Expression<Func<TEntity, bool>> where, object parameter, UnitOfWork uow = null)
        {
            GetTable(uow: uow).Update(where, entity => entity);
            return true;
        }

        public bool UpdateColumn(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> setter, UnitOfWork uow = null)
        {
            GetTable(uow: uow).Update(where, setter);
            return true;
        }

        public bool Delete(TEntity entity, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                cnn.Connection.Delete(entity);
                return true;
            }
        }

        public bool BulkDelete(List<TEntity> list, UnitOfWork uow = null)
        {
            using (var cnn = GetUnitDataSource(uow))
            {
                foreach (var entity in list)
                {
                    cnn.Connection.Delete(entity);
                }
                return true;
            }
        }

        public bool DeleteWhere(Expression<Func<TEntity, bool>> where, UnitOfWork uow = null)
        {
            GetTable(uow: uow).Delete(where);
            return true;
        }

    }
}
