using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository.Repository
{
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : IEntity<TKey>
    {
        private readonly IMongoCollection<T> _collection;

        protected BaseRepository() : this(Utils.GetConnectionString())
        {
        }
        private BaseRepository(string connectionString)
        {
            _collection = new MongoClient(connectionString).GetDatabase(Utils.GetDatabaseName()).GetCollection<T>(typeof(T).Name);
        }
        protected BaseRepository(MongoClientSettings settings)
        {
            _collection = new MongoClient(settings).GetDatabase(Utils.GetDatabaseName()).GetCollection<T>(typeof(T).Name);
        }

        protected IMongoCollection<T> Collection
        {
            get { return _collection; }
        }

        public async Task<List<T>> GetAll()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate) 
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<T> GetById(TKey id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            return await _collection.Find(filter).FirstAsync();
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(TKey id, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null)
        {
            await _collection.FindOneAndUpdateAsync(Builders<T>.Filter.Eq(x => x.Id, id), update, options);
        }

        public async Task Replace(T entity, FindOneAndReplaceOptions<T> options = null)
        {
            await _collection.FindOneAndReplaceAsync(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity, options);
        }

        public async Task Delete(TKey id)
        {
            await _collection.FindOneAndDeleteAsync(Builders<T>.Filter.Eq(x => x.Id, id));
        }

        //TODO: Batch operation???
    }
    public abstract class BaseRepository<T> : BaseRepository<T, ObjectId>, IRepository<T> where T : Entity
    {

    }
}
