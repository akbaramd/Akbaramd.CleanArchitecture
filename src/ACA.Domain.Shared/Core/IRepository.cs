using System.Linq.Expressions;

namespace ACA.Domain.Shared.Core;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
    where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
{
    Task<TEntity> GetOneAsync(TKey id, CancellationToken cancellationToken = default);
    TEntity GetOne(TKey id);

    TEntity Delete(TKey id, bool autoSave = false);
    Task<TEntity> DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Queryable();

    TEntity GetOne(Expression<Func<TEntity, bool>> expression);


    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    List<TEntity> Get(Expression<Func<TEntity, bool>> expression);


    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    bool Exist(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);


    TEntity Insert(TEntity entity, bool autoSave = false);
    Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity, bool autoSave = false);
    Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

    TEntity Delete(TEntity entity, bool autoSave = false);
    Task<TEntity> DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

    TEntity Delete(Expression<Func<TEntity, bool>> expression, bool autoSave = false);

    Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> expression, bool autoSave = false,
        CancellationToken cancellationToken = default);
}