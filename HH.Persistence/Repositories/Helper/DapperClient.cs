﻿//using Dapper;
//using MySqlConnector;
//using System.Data;
//using static Dapper.SqlMapper;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//namespace HH.Persitence.Repository.Helper
//{
//    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details", Justification = "<Pending>")]
//    public class DapperClient
//    {
//        private readonly IDbConnection _dbConnection;
//        public DapperClient(IDbConnection dbConnection)
//        {
//            _dbConnection = dbConnection;
//        }

//        private async Task Reconnect()
//        {
//            if (_dbConnection is MySqlConnection npgsqlConnection)
//            {
//                if (npgsqlConnection.State == ConnectionState.Closed || _dbConnection.State == ConnectionState.Broken)
//                {
//                    await npgsqlConnection.OpenAsync();
//                }
//            }
//        }

//        private string ToPostgresStoredStatement(string procedureName, DynamicParameters param, string[] resultParams)
//        {
//            string empty = string.Empty;
//            if (param != null)
//            {
//                IEnumerable<string> values = param.ParameterNames.Select((x) => "@" + x);
//                empty = "(" + string.Join(",", values) + ")";
//            }
//            else
//            {
//                empty = "()";
//            }

//            string fetchQuery = string.Empty;
//            if (resultParams?.Any() ?? false)
//            {
//                resultParams.ToList().ForEach(delegate (string x)
//                {
//                    fetchQuery = fetchQuery + " FETCH ALL IN " + x + ";";
//                });
//            }

//            return "CALL " + procedureName + empty + "; " + fetchQuery;
//        }

//        public async Task<IEnumerable<T>> QueryStoredProcPgSql<T>(
//            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
//        {
//            await Reconnect();
//            IDbTransaction transaction = tran ?? _dbConnection.BeginTransaction();
//            try
//            {
//                string query = ToPostgresStoredStatement(procName, parameters, new string[1] { resultParam });
//                GridReader multi = await _dbConnection.QueryMultipleAsync(query, parameters,
//                                                                                    commandType: CommandType.Text,
//                                                                                    transaction: transaction,
//                                                                                    commandTimeout: 300);
//                await multi.ReadAsync<object>();
//                IEnumerable<T> result = await multi.ReadAsync<T>();
//                if (tran == null)
//                {
//                    transaction.Commit();
//                }

//                return result;
//            }
//            catch (MySqlException ex)
//            {
//                if (ex.Message.Contains("terminating connection due to administrator command"))
//                {
//                    return await QueryStoredProcPgSql<T>(procName, parameters, resultParam, tran);
//                }

//                transaction?.Rollback();
//                throw ex;
//            }
//            catch
//            {
//                transaction.Rollback();
//                throw;
//            }
//        }

//        public async Task<T?> QueryFirstStoredProcPgSql<T>(
//            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
//        {
//            await Reconnect();
//            IDbTransaction transaction = tran ?? _dbConnection.BeginTransaction();
//            try
//            {
//                string query = ToPostgresStoredStatement(procName, parameters, new string[1] { resultParam });
//                GridReader multi = await _dbConnection.QueryMultipleAsync(query, parameters,
//                                                                commandType: CommandType.Text,
//                                                                transaction: transaction,
//                                                                commandTimeout: 300);
//                await multi.ReadAsync<object>();
//                IEnumerable<T> result = await multi.ReadAsync<T>();
//                if (tran == null)
//                {
//                    transaction.Commit();
//                }

//                return result.FirstOrDefault();
//            }
//            catch (MySqlException ex)
//            {
//                if (ex.Message.Contains("terminating connection due to administrator command"))
//                {
//                    return await QueryFirstStoredProcPgSql<T>(procName, parameters, resultParam, tran);
//                }

//                transaction?.Rollback();
//                throw ex;
//            }
//            catch
//            {
//                transaction.Rollback();
//                throw;
//            }
//        }

//        public async Task<GridReader> QueryMultiStoredProcPgSql(
//            string procName, DynamicParameters parameters, params string[] resultParams)
//        {
//            await Reconnect();
//            try
//            {
//                string query = ToPostgresStoredStatement(procName, parameters, resultParams);
//                GridReader result = await _dbConnection.QueryMultipleAsync(query, parameters, null, null, CommandType.Text);

//                await result.ReadAsync<object>();

//                return result;
//            }
//            catch (MySqlException ex)
//            {
//                if (ex.Message.Contains("terminating connection due to administrator command"))
//                {
//                    return await QueryMultiStoredProcPgSql(procName, parameters, resultParams);
//                }

//                throw ex;
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        public async Task<int> ExecuteStoredProcPgSql(
//            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
//        {
//            await Reconnect();
//            IDbTransaction transaction = tran ?? _dbConnection.BeginTransaction();
//            try
//            {
//                parameters.Add(resultParam, 0);

//                string query = ToPostgresStoredStatement(procName, parameters, null);

//                var gridReader = await _dbConnection.QueryMultipleAsync(query, parameters, transaction, null, CommandType.Text);
//                IEnumerable<int> result = await gridReader.ReadAsync<int>();

//                if (tran == null)
//                {
//                    transaction.Commit();
//                }

//                return result.First();
//            }
//            catch (MySqlException ex)
//            {
//                if (ex.Message.Contains("terminating connection due to administrator command"))
//                {
//                    return await ExecuteStoredProcPgSql(procName, parameters, resultParam, tran);
//                }

//                transaction?.Rollback();
//                throw ex;
//            }
//            catch
//            {
//                transaction?.Rollback();
//                throw;
//            }
//        }

//        public async Task<T> ExecuteStoredProcPgSql<T>(
//            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
//        {
//            await Reconnect();
//            IDbTransaction transaction = tran ?? _dbConnection.BeginTransaction();
//            try
//            {
//                parameters.Add(resultParam, default(T));

//                string query = ToPostgresStoredStatement(procName, parameters, null);
//                var gridReader = await _dbConnection.QueryMultipleAsync(query, parameters, transaction, null, CommandType.Text);
//                IEnumerable<T> result = await gridReader.ReadAsync<T>();

//                if (tran == null)
//                {
//                    transaction.Commit();
//                }

//                return result.First();
//            }
//            catch (MySqlException ex)
//            {
//                if (ex.Message.Contains("terminating connection due to administrator command"))
//                {
//                    return await ExecuteStoredProcPgSql<T>(procName, parameters, resultParam, tran);
//                }

//                transaction?.Rollback();
//                throw ex;
//            }
//            catch
//            {
//                transaction.Rollback();
//                throw;
//            }
//        }

//        public async Task<(IEnumerable<Result>, OutResult)> QueryStoredProcMySql<Result, OutResult>(
//            string storedProcedure, DynamicParameters parameters, string outParam, IDbTransaction? tran = null)
//        {

//            var result = await _dbConnection.QueryAsync<Result>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
//            var outResult = parameters.Get<OutResult>(outParam);

//            return (result, outResult);
//        }
        
//        // query first stored procedure with output parameter
//        public async Task<Result> QueryFirstStoredProcMySql<Result>(
//            string storedProcedure, DynamicParameters parameters, IDbTransaction? tran = null)
//        {
//            var result = await _dbConnection.QueryFirstOrDefaultAsync<Result>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

//            return result;
//        }
//    }
//}
