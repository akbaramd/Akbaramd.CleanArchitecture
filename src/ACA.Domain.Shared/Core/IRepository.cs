using System.Linq.Expressions;

namespace ACA.Domain.Shared.Core;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
    where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
{
    Task<TEntity> GetOneAsync(TKey id, CancellationToken cancellationToken = default);
    TEntity GetOne(TKey id);

    void Delete(TKey id, bool autoSave = true);
    Task DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Queryable();

    TEntity GetOne(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);
    TEntity? FindOne(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression,
      CancellationToken cancellationToken = default);
    List<TEntity> Get(Expression<Func<TEntity, bool>> expression);


    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    bool Exist(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);


    TEntity Insert(TEntity entity, bool autoSave = true);
    Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity, bool autoSave = true);
    Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);

    void Delete(TEntity entity, bool autoSave = true);
    Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);

    void Delete(Expression<Func<TEntity, bool>> expression, bool autoSave = true);

    Task DeleteAsync(Expression<Func<TEntity, bool>> expression, bool autoSave = true,
        CancellationToken cancellationToken = default);
}