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
        Task<TView> Get<TView>(Expression<Func<T, bool>> search, ProjectionDefinition<T, TView> projection = null,
            SortDefinition<T> sort = null);

        Task<List<TView>> GetAll<TView>(Expression<Func<T, bool>> search,
            ProjectionDefinition<T, TView> projection = null, SortDefinition<T> sort = null);

        Task<List<TView>> GetAll<TView>(int offset, int limit, Expression<Func<T, bool>> search,
            ProjectionDefinition<T, TView> projection = null, SortDefinition<T> sort = null);

        Task<List<TView>> GetAll<TView>(FilterDefinition<T> search, ProjectionDefinition<T, TView> projection = null,
            SortDefinition<T> sort = null);

        //Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);

        //Task<List<T>> FindBy(FilterDefinition<T> predicate);

        Task<List<TField>> Distinct<TField>(string fieldName, FilterDefinition<T> filter);

        //Task<T> GetById(TKey id);

        IAggregateFluent<T> GetAggregate();

        Task Add(T entity);

        Task Update(TKey id, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null);

        Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options = null);

        Task<T> Replace(T entity, FindOneAndReplaceOptions<T> options = null);

        Task Delete(TKey id);
    }

    public interface IRepository<T> : IRepository<T, string> where T : Entity
    {
    }
}
