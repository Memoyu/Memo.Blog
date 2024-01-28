using FreeSql;
using System.Linq.Expressions;
using Memo.Blog.Application.Common.Interfaces;
using Memo.Blog.Application.Common.Interfaces.Data.Repositories;
using Memo.Blog.Domain.Common;

namespace Memo.Blog.Infrastructure.Data.Repositories;

public class BaseAuditRepository<TEntity> : DefaultRepository<TEntity, long>, IBaseAuditRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    ///  当前登录人信息
    /// </summary>
    protected readonly ICurrentUser CurrentUser;

    public BaseAuditRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
    {
        CurrentUser = currentUser;
    }

    protected void BeforeInsert(TEntity entity)
    {
        if (entity is BaseAuditEntity createAudit)
        {
            createAudit.CreateTime = DateTime.Now;
            createAudit.CreateUserId = CurrentUser.UserId.HasValue ? CurrentUser.UserId.Value : 0;
        }

        BeforeUpdate(entity);
    }

    protected void BeforeUpdate(TEntity entity)
    {
        if (entity is BaseAuditEntity updateAudit)
        {
            updateAudit.UpdateTime = DateTime.Now;
            updateAudit.UpdateUserId = CurrentUser.UserId;
        }
    }

    protected void BeforeDelete(TEntity entity)
    {
        if (entity is BaseAuditEntity deleteAudit)
        {
            deleteAudit.IsDeleted = true;
            deleteAudit.DeleteUserId = CurrentUser.UserId;
            deleteAudit.DeleteTime = DateTime.Now;
        }
    }

    #region Insert

    public override TEntity Insert(TEntity entity)
    {
        BeforeInsert(entity);
        return base.Insert(entity);
    }

    public override Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        BeforeInsert(entity);
        return base.InsertAsync(entity, cancellationToken);
    }

    public override List<TEntity> Insert(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            BeforeInsert(entity);
        }

        return base.Insert(entities);
    }

    public override Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (TEntity entity in entities)
        {
            BeforeInsert(entity);
        }

        return base.InsertAsync(entities, cancellationToken);
    }

    #endregion

    #region Update

    public override int Update(TEntity entity)
    {
        BeforeUpdate(entity);
        return base.Update(entity);
    }

    public override Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        BeforeUpdate(entity);
        return base.UpdateAsync(entity, cancellationToken);
    }

    public override int Update(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            BeforeUpdate(entity);
        }

        return base.Update(entities);
    }

    public override Task<int> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            BeforeUpdate(entity);
        }

        return base.UpdateAsync(entities, cancellationToken);
    }

    #endregion

    #region Delete

    public override int Delete(long id)
    {
        TEntity entity = Get(id);
        BeforeDelete(entity);
        return base.Update(entity);
    }

    public override int Delete(TEntity entity)
    {
        base.Attach(entity);
        BeforeDelete(entity);
        return base.Update(entity);
    }

    public override int Delete(IEnumerable<TEntity> entities)
    {
        base.Attach(entities);
        foreach (TEntity entity in entities)
        {
            BeforeDelete(entity);
        }
        return base.Update(entities);
    }

    public override async Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        TEntity entity = await GetAsync(id, cancellationToken);
        BeforeDelete(entity);
        return await base.UpdateAsync(entity, cancellationToken);
    }

    public override Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Attach(entities);
        foreach (TEntity entity in entities)
        {
            BeforeDelete(entity);
        }

        return base.UpdateAsync(entities, cancellationToken);
    }

    public override Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        base.Attach(entity);
        BeforeDelete(entity);
        return base.UpdateAsync(entity, cancellationToken);
    }

    public override int Delete(Expression<Func<TEntity, bool>> predicate)
    {
        List<TEntity> items = base.Select.Where(predicate).ToList();
        if (items.Count == 0)
        {
            return 0;
        }

        foreach (var entity in items)
        {
            BeforeDelete(entity);
        }

        return base.Update(items);
    }

    public override async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        List<TEntity> items = await base.Select.Where(predicate).ToListAsync(cancellationToken);
        if (items.Count == 0)
        {
            return 0;
        }

        foreach (var entity in items)
        {
            BeforeDelete(entity);
        }

        return await base.UpdateAsync(items, cancellationToken);
    }

    #endregion

    #region InsertOrUpdate
    public override TEntity InsertOrUpdate(TEntity entity)
    {
        BeforeInsert(entity);
        return base.InsertOrUpdate(entity);
    }

    public override async Task<TEntity> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        BeforeInsert(entity);
        return await base.InsertOrUpdateAsync(entity, cancellationToken);
    }

    #endregion
}
