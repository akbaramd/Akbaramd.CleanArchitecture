using System.Linq.Expressions;
using ACA.Domain.Shared.Core;
using Microsoft.EntityFrameworkCore;

namespace ACA.Infrastructure.Data;

public class EfRepository<TEntity>(ACADbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
   private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

  public IQueryable<TEntity> Queryable()
  {
    return _dbSet;
  }

  public TEntity GetOne(Expression<Func<TEntity, bool>> expression)
  {
    return _dbSet.First(expression);
  }

  public Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression,
    CancellationToken cancellationToken = default)
  {
    return _dbSet.FirstAsync(expression, cancellationToken: cancellationToken);
  }

  public TEntity? FindOne(Expression<Func<TEntity, bool>> expression)
  {
    return _dbSet.FirstOrDefault(expression);
  }

  public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
  {
    return _dbSet.FirstOrDefaultAsync(expression, cancellationToken: cancellationToken);
  }

  public List<TEntity> Get(Expression<Func<TEntity, bool>> expression)
  {
    return _dbSet.Where(expression).ToList();
  }

  public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
    CancellationToken cancellationToken = default)
  {
    return _dbSet.Where(expression).ToListAsync(cancellationToken: cancellationToken);
  }

  public bool Exist(Expression<Func<TEntity, bool>> expression)
  {
    return _dbSet.Any(expression);
  }

  public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression,
    CancellationToken cancellationToken = default)
  {
    return _dbSet.AnyAsync(expression, cancellationToken);
  }

  public TEntity Insert(TEntity entity, bool autoSave = true)
  {
    var res = _dbSet.Add(entity);

    if (autoSave)
    {
      context.SaveChanges();
    }

    return res.Entity;
  }

  public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
  {
    var res = await _dbSet.AddAsync(entity, cancellationToken);

    if (autoSave)
    {
      await  context.SaveChangesAsync(cancellationToken);
    }

    return res.Entity;
  }

  public TEntity Update(TEntity entity, bool autoSave = true)
  {
    var res = _dbSet.Update(entity);

    if (autoSave)
    {
      context.SaveChanges();
    }

    return res.Entity;
  }

  public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
  {
    var res =  _dbSet.Update(entity);

    if (autoSave)
    {
      await  context.SaveChangesAsync(cancellationToken);
    }

    return res.Entity;
  }

  public void Delete(TEntity entity, bool autoSave = true)
  {
    var res = _dbSet.Remove(entity);

    if (autoSave)
    {
      context.SaveChanges();
    }
  }

  public async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
  {
    var res =  _dbSet.Remove(entity);

    if (autoSave)
    {
      await  context.SaveChangesAsync(cancellationToken);
    }
  }

  public void Delete(Expression<Func<TEntity, bool>> expression, bool autoSave = true)
  {
    var finds = Get(expression);

    foreach (var find in finds)
    {
      Delete(find);
    }
    
  }

  public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression, bool autoSave = true,
    CancellationToken cancellationToken = default)
  {
    var finds = await GetAsync(expression, cancellationToken);

    foreach (var find in finds)
    {
      await  DeleteAsync(find, cancellationToken: cancellationToken);
    }

  }
}

public class EfRepository<TEntity, TKey>(ACADbContext context) : EfRepository<TEntity>(context), IRepository<TEntity, TKey>
  where TEntity : class, IEntity<TKey>
  where TKey : IEquatable<TKey>
{
  public Task<TEntity> GetOneAsync(TKey id, CancellationToken cancellationToken = default)
  {
    return GetOneAsync(x => x.Id.Equals(id), cancellationToken);
  }

  public TEntity GetOne(TKey id)
  {
    return GetOne(x => x.Id.Equals(id));
  }

  public void Delete(TKey id, bool autoSave = true)
  {
     Delete(x => x.Id.Equals(id), autoSave);
  }

  public Task DeleteAsync(TKey id, bool autoSave = true, CancellationToken cancellationToken = default)
  {
    return  DeleteAsync(x => x.Id.Equals(id), autoSave, cancellationToken);
  }
}
