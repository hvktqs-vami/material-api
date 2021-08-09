﻿using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Data;
using LinqToDB;
using LinqToDB.Mapping;
using LinqToDB.DataProvider;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Mta.Vami.WebApi.Core
{
    public class DataProvider
    {
        public string SettingName { get; protected set; }

        public string ConnectionString { get; set; }

        public DataProvider(string settingName)
        {
            SettingName = settingName;
            ConnectionString = CoreService.Configuration.GetConnectionString(settingName);
        }

        #region Utils

        private void UpdateParameterValue(DataConnection dataConnection, DataParameter parameter)
        {
            if (dataConnection is null)
                throw new ArgumentNullException(nameof(dataConnection));

            if (parameter is null)
                throw new ArgumentNullException(nameof(parameter));

            if (dataConnection.Command is IDbCommand command &&
                command.Parameters.Count > 0 &&
                command.Parameters.Contains(parameter.Name) &&
                command.Parameters[parameter.Name] is IDbDataParameter param)
            {
                parameter.Value = param.Value;
            }
        }

        private void UpdateOutputParameters(DataConnection dataConnection, DataParameter[] dataParameters)
        {
            if (dataParameters is null || dataParameters.Length == 0)
                return;

            foreach (var dataParam in dataParameters.Where(p => p.Direction == ParameterDirection.Output))
            {
                UpdateParameterValue(dataConnection, dataParam);
            }
        }

        /// <summary>
        /// Gets a connection to the database for a current data provider
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection to a database</returns>
        protected IDbConnection GetInternalDbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(nameof(connectionString));

            var cnn = MySql.Data.MySqlClient.MySqlClientFactory.Instance.CreateConnection();
            cnn.ConnectionString = ConnectionString;
            return cnn;
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        public virtual DataConnection CreateDataConnection(UnitOfWork uow = null)
        {
            DataConnection rs = null;
            if (uow == null)
            {
                var cnn = CreateDbConnection();
                rs = new DataConnection(LinqToDbDataProvider, cnn);
            }
            else
            {
                return uow.Sources[SettingName].Connection;
            }

            //rs.AddMappingSchema(AdditionalSchema);

            return rs;
        }


        /// <summary>
        /// Creates the database connection
        /// </summary>
        public virtual DataContext CreateDataContext()
        {
            DataContext rs = new DataContext(LinqToDbDataProvider, ConnectionString);
            return rs;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a connection to a database
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection to a database</returns>
        public virtual IDbConnection CreateDbConnection()
        {
            var dbConnection = GetInternalDbConnection(ConnectionString);
            return dbConnection;
        }

        /// <summary>
        /// Returns mapped entity descriptor.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Mapping descriptor</returns>
        public EntityDescriptor GetEntityDescriptor<TEntity>()
        {
            return MappingSchema.Default.GetEntityDescriptor(typeof(TEntity));
        }

        /// <summary>
        /// Returns queryable source for specified mapping class for current connection,
        /// mapped to database table or view.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Queryable source</returns>
        public virtual ITable<TEntity> GetTable<TEntity>(UnitOfWork uow = null) where TEntity : class
        {
            if (uow != null)
            {
                DataConnection ctx = CreateDataConnection(uow);
                return ctx.GetTable<TEntity>();
            }
            else
            {
                var ctx = CreateDataContext();
                return ctx.GetTable<TEntity>();
            }
        }

        public virtual CommandInfo GetCommand(DataConnection cnn, string sqlText, DataParameter[] parameters)
        {
            return new CommandInfo(cnn, sqlText, parameters);
        }

        ///// <summary>
        ///// Executes command returns number of affected records.
        ///// </summary>
        ///// <param name="sqlStatement">Command text</param>
        ///// <param name="dataParameters">Command parameters</param>
        ///// <returns>Number of records, affected by command execution.</returns>
        //public int ExecuteNonQuery(string sqlStatement, params DataParameter[] dataParameters)
        //{
        //    using var dataContext = CreateDataConnection();
        //    var command = new CommandInfo(dataContext, sqlStatement, dataParameters);
        //    var affectedRecords = command.Execute();

        //    UpdateOutputParameters(dataContext, dataParameters);

        //    return affectedRecords;
        //}

        ///// <summary>
        ///// Executes command using LinqToDB.Mapping.StoredProcedure command type and returns
        ///// single value
        ///// </summary>
        ///// <typeparam name="T">Result record type</typeparam>
        ///// <param name="procedureName">Procedure name</param>
        ///// <param name="parameters">Command parameters</param>
        ///// <returns>Resulting value</returns>
        //public T ExecuteStoredProcedure<T>(string procedureName, params DataParameter[] parameters)
        //{
        //    using var dataContext = CreateDataConnection();
        //    var command = new CommandInfo(dataContext, procedureName, parameters);

        //    var result = command.ExecuteProc<T>();
        //    UpdateOutputParameters(dataContext, parameters);

        //    return result;
        //}

        ///// <summary>
        ///// Executes command using LinqToDB.Mapping.StoredProcedure command type and returns
        ///// number of affected records.
        ///// </summary>
        ///// <param name="procedureName">Procedure name</param>
        ///// <param name="parameters">Command parameters</param>
        ///// <returns>Number of records, affected by command execution.</returns>
        //public int ExecuteStoredProcedure(string procedureName, params DataParameter[] parameters)
        //{
        //    using var dataContext = CreateDataConnection();
        //    var command = new CommandInfo(dataContext, procedureName, parameters);

        //    var affectedRecords = command.ExecuteProc();
        //    UpdateOutputParameters(dataContext, parameters);

        //    return affectedRecords;
        //}

        ///// <summary>
        ///// Executes command using System.Data.CommandType.StoredProcedure command type and
        ///// returns results as collection of values of specified type
        ///// </summary>
        ///// <typeparam name="T">Result record type</typeparam>
        ///// <param name="procedureName">Procedure name</param>
        ///// <param name="parameters">Command parameters</param>
        ///// <returns>Returns collection of query result records</returns>
        //public IList<T> QueryProc<T>(string procedureName, params DataParameter[] parameters)
        //{
        //    using var dataContext = CreateDataConnection();
        //    var command = new CommandInfo(dataContext, procedureName, parameters);
        //    var rez = command.QueryProc<T>()?.ToList();
        //    UpdateOutputParameters(dataContext, parameters);
        //    return rez ?? new List<T>();
        //}

        ///// <summary>
        ///// Executes SQL command and returns results as collection of values of specified type
        ///// </summary>
        ///// <typeparam name="T">Type of result items</typeparam>
        ///// <param name="sql">SQL command text</param>
        ///// <param name="parameters">Parameters to execute the SQL command</param>
        ///// <returns>Collection of values of specified type</returns>
        //public IList<T> Query<T>(string sql, params DataParameter[] parameters)
        //{
        //    using var dataContext = CreateDataConnection();
        //    return dataContext.Query<T>(sql, parameters)?.ToList() ?? new List<T>();
        //}

        #endregion

        #region Properties

        /// <summary>
        /// Sql server data provider
        /// </summary>
        protected IDataProvider LinqToDbDataProvider => new LinqToDB.DataProvider.MySql.MySqlDataProvider(ProviderName.MySql);

        /// <summary>
        /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
        /// </summary>
        public int SupportedLengthOfBinaryHash { get; } = 8000;

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        public virtual bool BackupSupported => true;

        /// <summary>
        /// Additional mapping schema
        /// </summary>
        protected MappingSchema AdditionalSchema
        {
            get
            {
                return null;

                //if (!(Singleton<MappingSchema>.Instance is null))
                //    return Singleton<MappingSchema>.Instance;

                //Singleton<MappingSchema>.Instance =
                //    new MappingSchema(ConfigurationName) { MetadataReader = new FluentMigratorMetadataReader() };

                //return Singleton<MappingSchema>.Instance;
            }
        }


        /// <summary>
        /// Name of database provider
        /// </summary>
        public string ConfigurationName => LinqToDbDataProvider.Name;

        #endregion
    }
}
