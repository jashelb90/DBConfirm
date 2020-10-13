﻿using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Models.Templates.Asbtract;
using Models.Extensions;
using Models.Abstract;

namespace Models
{
    public class TestRunner : ITestRunner
    {
        #region Setup

        private string ConnectionString { get; }
        private SqlConnection SqlConnection { get; set; }
        private SqlTransaction SqlTransaction { get; set; }

        private bool disposedValue;

        public TestRunner(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task InitialiseAsync()
        {
            SqlConnection = new SqlConnection(ConnectionString);
            await SqlConnection.OpenAsync();

            SqlTransaction = SqlConnection.BeginTransaction(Debugger.IsAttached ? IsolationLevel.ReadUncommitted : IsolationLevel.ReadCommitted);
        }

        private void DisposeConnections()
        {
            void Run(Action act)
            {
                try
                {
                    act();
                }
                catch (Exception) { }
            }
            Run(() => SqlTransaction.Rollback());
            Run(() => SqlTransaction.Dispose());
            Run(() => SqlConnection.Close());
            Run(() => SqlConnection.Dispose());
        }

        #endregion

        #region Actions

        public async Task ExecuteStoredProcedureNonQueryAsync(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(procedureName, SqlConnection, SqlTransaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                await command.ExecuteNonQueryAsync();
            }
        }

        public Task<QueryResult> ExecuteStoredProcedureQueryAsync(string procedureName, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            {
                ds.Locale = CultureInfo.InvariantCulture;

                using (SqlCommand command = new SqlCommand(procedureName, SqlConnection, SqlTransaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
                if (ds.Tables.Count > 0)
                {
                    return Task.FromResult(new QueryResult(ds.Tables[0]));
                }
                return Task.FromResult(new QueryResult());
            }
        }

        public Task<QueryResult> ExecuteViewAsync(string viewName)
        {
            return ExecuteCommandAsync($"SELECT * FROM {viewName}");
        }

        public async Task<int> CountRowsInViewAsync(string viewName)
        {
            return (await ExecuteCommandScalarAsync<int>($"SELECT COUNT(*) AS [Count] FROM {viewName}")).RawData;
        }

        public Task<IList<QueryResult>> ExecuteStoredProcedureMultipleDataSetAsync(string procedureName, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            {
                ds.Locale = CultureInfo.InvariantCulture;

                using (SqlCommand command = new SqlCommand(procedureName, SqlConnection, SqlTransaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
                return Task.FromResult((IList<QueryResult>)ds.Tables.Cast<DataTable>().Select(p => new QueryResult(p)).ToList());
            }
        }

        public Task<IList<QueryResult>> ExecuteStoredProcedureMultipleDataSetAsync(string procedureName, IDictionary<string, object> parameters)
        {
            return ExecuteStoredProcedureMultipleDataSetAsync(procedureName, parameters.ToSqlParameters());
        }

        public async Task<ScalarResult<T>> ExecuteStoredProcedureScalarAsync<T>(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(procedureName, SqlConnection, SqlTransaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);

                return new ScalarResult<T>((T)await command.ExecuteScalarAsync());
            }
        }

        public Task<ScalarResult<T>> ExecuteStoredProcedureScalarAsync<T>(string procedureName, IDictionary<string, object> parameters)
        {
            return ExecuteStoredProcedureScalarAsync<T>(procedureName, parameters.ToSqlParameters());
        }

        public async Task ExecuteCommandNoResultsAsync(string commandText, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(commandText, SqlConnection, SqlTransaction))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters);

                await command.ExecuteNonQueryAsync();
            }
        }

        public Task ExecuteCommandNoResultsAsync(string commandText, IDictionary<string, object> parameters)
        {
            return ExecuteCommandNoResultsAsync(commandText, parameters.ToSqlParameters());
        }

        public Task<QueryResult> ExecuteCommandAsync(string commandText, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            {
                ds.Locale = CultureInfo.InvariantCulture;

                using (SqlCommand command = new SqlCommand(commandText, SqlConnection, SqlTransaction))
                {
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
                if (ds.Tables.Count > 0)
                {
                    return Task.FromResult(new QueryResult(ds.Tables[0]));
                }
                return Task.FromResult(new QueryResult());
            }
        }

        public Task<QueryResult> ExecuteCommandAsync(string commandText, IDictionary<string, object> parameters)
        {
            return ExecuteCommandAsync(commandText, parameters.ToSqlParameters());
        }

        public async Task<ScalarResult<T>> ExecuteCommandScalarAsync<T>(string commandText, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(commandText, SqlConnection, SqlTransaction))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters);

                return new ScalarResult<T>((T)await command.ExecuteScalarAsync());
            }
        }

        public Task<ScalarResult<T>> ExecuteCommandScalarAsync<T>(string commandText, IDictionary<string, object> parameters)
        {
            return ExecuteCommandScalarAsync<T>(commandText, parameters.ToSqlParameters());
        }

        public Task<IList<QueryResult>> ExecuteCommandMultipleDataSetAsync(string commandText, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            {
                ds.Locale = CultureInfo.InvariantCulture;

                using (SqlCommand command = new SqlCommand(commandText, SqlConnection, SqlTransaction))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
                return Task.FromResult((IList<QueryResult>)ds.Tables.Cast<DataTable>().Select(p => new QueryResult(p)).ToList());
            }
        }

        public Task<IList<QueryResult>> ExecuteCommandMultipleDataSetAsync(string commandText, IDictionary<string, object> parameters)
        {
            return ExecuteCommandMultipleDataSetAsync(commandText, parameters.ToSqlParameters());
        }

        public Task<QueryResult> ExecuteTableAsync(string tableName)
        {
            return ExecuteViewAsync(tableName);
        }

        public Task<int> CountRowsInTableAsync(string tableName)
        {
            return CountRowsInViewAsync(tableName);
        }

        private static string DelimitColumnName(string name)
        {
            name = name.TrimStart('[').TrimEnd(']');
            return $"[{name}]";
        }

        private async Task<(int? identity, string identityColumnName)> InsertDefaultWithIdentity(string tableName)
        {
            string command = $@"
                INSERT INTO
                    {tableName}
                DEFAULT VALUES;

                IF OBJECTPROPERTY(OBJECT_ID('{tableName}'), 'TableHasIdentity') = 1
                BEGIN
                    SELECT
                        SCOPE_IDENTITY() AS IdentityValue
                        ,name AS IdentityColumnName
                    FROM 
		                sys.identity_columns
	                WHERE
		                OBJECT_SCHEMA_NAME(object_id) + '.' + OBJECT_NAME(object_id) = '{tableName}'
                END
                ELSE
                BEGIN
                    SELECT NULL
                END";

            QueryResult data = await ExecuteCommandAsync(command);

            if (data.TotalRows == 0)
            {
                return (null, null);
            }
            return ((int?)data.RawData.Rows[0]["IdentityValue"], data.RawData.Rows[0]["IdentityColumnName"].ToString());
        }

        public Task<T> InsertAsync<T>() where T : ITemplate, new()
        {
            return InsertAsync(new T());
        }

        public async Task<T> InsertComplexAsync<T>(T complexTemplate) where T : IComplexTemplate
        {
            await complexTemplate.InsertAsync(this);
            return complexTemplate;
        }

        public async Task<T> InsertAsync<T>(T template) where T : ITemplate
        {
            await InsertDataAsync(template.TableName, template.DefaultData, template.CustomData);

            return template;
        }

        public Task<DataSetRow> InsertDataAsync(string tableName, DataSetRow data)
        {
            return InsertDataAsync(tableName, data, null);
        }

        private async Task<DataSetRow> InsertDataAsync(string tableName, DataSetRow defaultData, DataSetRow overrideData = null)
        {
            DataSetRow data = defaultData;
            if (overrideData != null)
            {
                data = data.Merge(overrideData);
            }

            if (data.All(p => p.Value == null))
            {
                var (defaultIdentityValue, defaultIdentityColumnName) = await InsertDefaultWithIdentity(tableName);

                return new DataSetRow
                {
                    [defaultIdentityColumnName] = defaultIdentityValue
                };
            }

            string command = $@"
                DECLARE @HasIdentity BIT = 0;
                DECLARE @IdentityColumnNameUpdated BIT = 0;
                DECLARE @IdentityColumnName NVARCHAR(255) = '';
                IF OBJECTPROPERTY(OBJECT_ID('{tableName}'), 'TableHasIdentity') = 1
                BEGIN
	                SELECT TOP (1)
                        @HasIdentity = 1
		                ,@IdentityColumnName = name
	                FROM 
		                sys.identity_columns
	                WHERE
		                OBJECT_SCHEMA_NAME(object_id) + '.' + OBJECT_NAME(object_id) = '{tableName}'
                END

                IF (@HasIdentity = 1)
                BEGIN

                    DECLARE @Columns TABLE (ColumnName NVARCHAR(255))
                    INSERT INTO
	                    @Columns
                    VALUES
	                    ({ string.Join("),(", data.Select(p => $"'{DelimitColumnName(p.Key)}'")) })

                    IF (EXISTS(SELECT * FROM @Columns WHERE ColumnName = '[' + @IdentityColumnName + ']'))
                    BEGIN
                        SET @IdentityColumnNameUpdated = 1;
                    END

	                IF (@IdentityColumnNameUpdated = 1)
	                BEGIN
		                SET IDENTITY_INSERT {tableName} ON;
	                END
                END

                INSERT INTO
                    {tableName}
                (
                    { string.Join(",", data.Select(p => DelimitColumnName(p.Key))) }
                )
                VALUES
                (
                    { string.Join(",", data.Select(p => $"@{p.Key}")) }
                );

                IF (@HasIdentity = 1)
                BEGIN
	                IF (@IdentityColumnNameUpdated = 1)
	                BEGIN
		                SET IDENTITY_INSERT {tableName} OFF;
	                END
	                ELSE
	                BEGIN
		                SELECT 
			                SCOPE_IDENTITY() AS IdentityValue
			                ,@IdentityColumnName AS IdentityColumnName
	                END
                END";

            QueryResult results = await ExecuteCommandAsync(command, data.ToSqlParameters());

            if (results.TotalRows == 0)
            {
                return data;
            }

            int insertedIdentityValue = Convert.ToInt32(results.RawData.Rows[0]["IdentityValue"]);
            string insertedIdentityColumnName = results.RawData.Rows[0]["IdentityColumnName"].ToString();

            (overrideData ?? data)[insertedIdentityColumnName] = insertedIdentityValue;

            return data;
        }


        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeConnections();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}