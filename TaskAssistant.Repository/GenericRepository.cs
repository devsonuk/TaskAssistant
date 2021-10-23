using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using TaskAssistant.Domain.Entities;
using TaskAssistant.Repository.Interfaces;


namespace TaskAssistant.Repository
{

    /// <summary>
    /// Contains methods to access database for Entity operations
    /// </summary>
    /// <typeparam name="TEntity">Entity inherited from BaseEntity</typeparam>
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private bool _disposedValue;
        private string _connectionString;
        protected string ConnectionString { get => _connectionString; set => _connectionString = value; }


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// Constructor which initialises Connection String based on ConnectionResolver
        /// </summary>
        /// <param name="connectionResolver">Implementation of IConnectionResolver</param>
        protected GenericRepository()
        {
            PageSort = new List<ISort>
            {
                Predicates.Sort<TEntity>(entity => entity.Id, false)
            };
            PageOffset = 0;
            PageLimit = 1000;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// Calls Dispose with false
        /// </summary>
        ~GenericRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        private IList<ISort> PageSort { get; set; }

        private int PageOffset { get; set; }

        private int PageLimit { get; set; }

        /// <inheritdoc cref="IGenericRepository{TEntity}.SetNewConnectionString(string)"/>
        public void SetNewConnectionString(string newConnectionString)
        {
            ConnectionString = newConnectionString;
        }

        #region Add Methods

        /// <inheritdoc cref="Add"/>
        public long Add(TEntity entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Insert(entity);
            }
        }

        #endregion Add Methods

        #region Delete methods

        /// <inheritdoc/>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var entity = connection.Get<TEntity>(id);
                connection.Delete(entity);
            }
        }

        #endregion Delete methods

        #region Update methods

        /// <inheritdoc cref="IGenericRepository{TEntity}.Update"/>
        public bool Update(TEntity entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Update(entity);
            }
        }

        #endregion Update methods

        #region Get methods

        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageOffset(int)"/>
        public void SetPageOffset(int pageOffset)
        {
            PageOffset = pageOffset;
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageLimit(int)"/>
        public void SetPageLimit(int pageLimit)
        {
            PageLimit = pageLimit;
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageSort(IList{ISort})"/>
        public void SetPageSort(IList<ISort> sortData)
        {
            PageSort = sortData;
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageSort(IList{ISort})"/>
        public void SetPagingParameters(int pageOffset, int pageLimit, IList<ISort> sortData)
        {
            PageOffset = pageOffset;
            PageLimit = pageLimit;
            PageSort = sortData;
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPagingParameters(int,int)"/>
        public void SetPagingParameters(int pageOffset, int pageLimit)
        {
            PageOffset = pageOffset;
            PageLimit = pageLimit;
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.Get(int)"/>
        public TEntity Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Get<TEntity>(id);
            }
        }

        /// <summary>
        /// Returns the entities matched by entityIds
        /// </summary>
        /// <param name="entityIds">The Ids of the entities</param>
        /// <returns>
        /// Entities
        /// </returns>
        public IEnumerable<TEntity> Get(IEnumerable<int> entityIds)
        {
            var predicateGroup = new PredicateGroup
            {
                Operator = GroupOperator.Or,
                Predicates = entityIds.Select(e => { return Predicates.Field<TEntity>(dp => dp.Id, Operator.Eq, e); }).ToList<IPredicate>()
            };

            return GetByPredicate(predicateGroup);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.Get(long)"/>
        public TEntity Get(long id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Get<TEntity>(id);
            }
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetByPredicate(IFieldPredicate)"/>
        public IEnumerable<TEntity> GetByPredicate(IFieldPredicate predicate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetPage<TEntity>(predicate, PageSort, PageOffset, PageLimit).ToList();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetByPredicate(IPredicateGroup predicateGroup)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetPage<TEntity>(predicateGroup, PageSort, PageOffset, PageLimit).ToList();
            }
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAll"/>
        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetPage<TEntity>(null, PageSort, PageOffset, PageLimit).ToList();
            }
        }

        #endregion Get methods

        #region Count Methods

        /// <inheritdoc cref="IGenericRepository{TEntity}.Count(IFieldPredicate)"/>
        public long Count(IFieldPredicate predicate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Count<TEntity>(predicate);
            }
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.CountByPredicateGroup(IPredicateGroup)"/>
        public long CountByPredicateGroup(IPredicateGroup predicateGroup)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Count<TEntity>(predicateGroup);
            }
        }

        #endregion Count Methods

        #region Async methods

        /// <inheritdoc cref="IGenericRepository{TEntity}.AddAsync(TEntity)"/>
        public virtual async Task<long> AddAsync(TEntity entity)
        {
            return await AddAsync(entity, CancellationToken.None).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.AddAsync(TEntity,CancellationToken)"/>
        public virtual async Task<long> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Add(entity), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.DeleteAsync(int)"/>
        public virtual async Task DeleteAsync(int id)
        {
            await DeleteAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.DeleteAsync(int,CancellationToken)"/>
        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await Task.Run(() => Delete(id), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.UpdateAsync(TEntity)"/>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => Update(entity), CancellationToken.None).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.UpdateAsync(TEntity,CancellationToken)"/>
        public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Update(entity), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAsync(int)"/>
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await GetAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAsync(int,CancellationToken)"/>
        public virtual async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Get(id), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync()"/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAllAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync(CancellationToken)"/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => GetAll(), cancellationToken).ConfigureAwait(false);
        }

        #endregion Async methods

        #region Dapper SQL Methods

        /// <summary>
        /// Executes a Sql Query with optional parameters and returns a Single Entity Result
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>Single Entity/>/></returns>
        public TEntity ExecuteSingle(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// Executes a Sql Query with optional parameters and returns a Single Entity Result or Default Value for the entity
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>a Single Entity Result or Default Value for the entity</returns>
        public TEntity ExecuteSingleOrDefault(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingleOrDefault<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// Executes a SQL Query with optional Parameters and Returns a Collection of Entities
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <param name="commandType">SQL Command Type</param>
        /// <param name="commandTimeOut">SQL Command Timeout</param>
        /// <returns>IEnumerable of Entity </returns>
        public IEnumerable<TEntity> Execute(string query, object parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<TEntity>(query, parameters, commandType: commandType, commandTimeout: commandTimeOut);
            }
        }

        /// <summary>
        /// Executes a SQL Query with optional Parameters and Returns a entity
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <param name="commandType">SQL Command Type</param>
        /// <param name="commandTimeOut">SQL Command Timeout</param>
        /// <returns>IEnumerable of Entity </returns>
        public T ExecuteSingle<T>(string query, object parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<T>(query, parameters, commandType: commandType, commandTimeout: commandTimeOut);
            }
        }

        #endregion Dapper SQL Methods

        #region IDisposable Support

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// An override of <see cref="IDisposable.Dispose"/> for the <see cref="GenericRepository{TEntity}"/> class
        /// </summary>
        /// <param name="disposing">Boolean flag to indicate whether to dispose or not</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // not sure what to dispose here
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.

        // This code added to correctly implement the disposable pattern.
        #endregion IDisposable Support
    }
}
