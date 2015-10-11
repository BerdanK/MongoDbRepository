using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbRepository.Repository
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : IEntity<TKey>
    {
        private readonly IMongoCollection<T> _collection;

        protected BaseRepository()
            : this(Utils.GetConnectionString())
        {
        }
        private BaseRepository(string connectionString)
        {
            //_collection = new MongoClient(connectionString).GetDatabase(Utils.GetDatabaseName()).GetCollection<T>(typeof(T).Name);
            _collection =
                new MongoClient(connectionString).GetDatabase(Utils.GetDatabaseName())
                    .GetCollection<T>(Utils.GetCollectionName<T>());
        }
        protected BaseRepository(MongoClientSettings settings)
        {
            _collection =
                new MongoClient(settings).GetDatabase(Utils.GetDatabaseName())
                    .GetCollection<T>(Utils.GetCollectionName<T>());
        }

        protected IMongoCollection<T> Collection
        {
            get { return _collection; }
        }

        public async Task<TView> Get<TView>(Expression<Func<T, bool>> search, ProjectionDefinition<T, TView> projection = null, SortDefinition<T> sort = null)
        {
            return await _collection.Find(search).Project(projection).Sort(sort).FirstAsync();
        }

        public async Task<List<TView>> GetAll<TView>(Expression<Func<T, bool>> search, ProjectionDefinition<T, TView> projection = null, SortDefinition<T> sort = null)
        {
            var query = _collection.Find(search).Project(projection).Sort(sort);
            return await query.ToListAsync();
        }

        public async Task<List<TView>> GetAll<TView>(FilterDefinition<T> search, ProjectionDefinition<T, TView> projection = null, SortDefinition<T> sort = null)
        {
            var query = _collection.Find(search).Project(projection).Sort(sort);
            return await query.ToListAsync();
        }

        public async Task<List<TView>> GetAll<TView>(int offset, int limit, Expression<Func<T, bool>> search, ProjectionDefinition<T, TView> projection = null, SortDefinition<T> sort = null)
        {
            return await _collection.Find(search).Project(projection).Sort(sort).Skip(offset).Limit(limit).ToListAsync();
        }

        public async Task<List<TField>> Distinct<TField>(string fieldName, FilterDefinition<T> filter)
        {
            return await _collection.DistinctAsync<TField>(fieldName, filter).Result.ToListAsync();
        }

        public IAggregateFluent<T> GetAggregate()
        {
            //var pipeline = new PipelineStagePipelineDefinition<T, T>(stages);
            //return await _collection.AggregateAsync(pipeline, options, cancellationToken);
            return _collection.Aggregate();
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(TKey id, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null)
        {
            await _collection.FindOneAndUpdateAsync(Builders<T>.Filter.Eq(x => x.Id, id), update, options);
        }

        public async Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null)
        {
            await _collection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<T> Replace(T entity, FindOneAndReplaceOptions<T> options = null)
        {
            return await _collection.FindOneAndReplaceAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity, options);
        }

        public async Task Delete(TKey id)
        {
            await _collection.FindOneAndDeleteAsync(Builders<T>.Filter.Eq(x => x.Id, id));
        }

        //TODO: Batch operation???
    }
    public class BaseRepository<T> : BaseRepository<T, string>, IRepository<T> where T : Entity
    {

    }
}
