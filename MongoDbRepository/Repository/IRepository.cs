using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbRepository.Repository
{
    public interface IRepository<T, in TKey> where T : IEntity<TKey>
    {
        Task<List<T>> GetAll();

        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);

        Task<T> GetById(TKey id);

        Task Add(T entity);

        Task Update(TKey id, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null);

        Task Replace(T entity, FindOneAndReplaceOptions<T> options = null);

        Task Delete(TKey id);
    }

    public interface IRepository<T> : IRepository<T, ObjectId> where T : Entity
    {
    }
}
