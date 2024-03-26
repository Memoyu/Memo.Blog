using FreeSql;
using System.Linq.Expressions;
using Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;
using Memo.Blog.Domain.Common;
using MediatR;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Infrastructure.Persistence.Repositories;

public class BaseDefaultRepository<TEntity> : DefaultRepository<TEntity, long>, IBaseDefaultRepository<TEntity> where TEntity : BaseAuditEntity
{
    /// <summary>
    ///  当前登录人信息
    /// </summary>
    protected readonly CurrentUser CurrentUser;

    private readonly IPublisher _publisher;

    public BaseDefaultRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUserProvider currentUserProvider, IPublisher publisher) : base(unitOfWorkManager?.Orm, unitOfWorkManager)
    {
        CurrentUser = currentUserProvider.GetCurrentUser();
        _publisher = publisher;
    }

    protected async Task BeforeInsertAsync(TEntity entity)
    {
        if (entity is BaseAuditEntity createAudit)
        {
            createAudit.CreateTime = DateTime.Now;
            createAudit.CreateUserId = CurrentUser.Id;
        }

        await BeforeUpdateAsync(entity);
    }

    protected async Task BeforeUpdateAsync(TEntity entity)
    {
        if (entity is BaseAuditEntity updateAudit)
        {
            updateAudit.UpdateTime = DateTime.Now;
            updateAudit.UpdateUserId = CurrentUser.Id;
        }

        await PublishDomainEventsAsync(entity);
    }

    protected async Task BeforeDeleteAsync(TEntity entity)
    {
        if (entity is BaseAuditEntity deleteAudit)
        {
            deleteAudit.IsDeleted = true;
            deleteAudit.DeleteUserId = CurrentUser.Id;
            deleteAudit.DeleteTime = DateTime.Now;
        }

        await PublishDomainEventsAsync(entity);
    }

    #region 领域事件

    private async Task PublishDomainEventsAsync(TEntity entity)
    {
        if (entity is not BaseEntity domainEntity) return;

        var domainEvents = domainEntity.GetDomainEvents().ToList();

        domainEntity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent);
    }

    #endregion

    #region Insert

    public override TEntity Insert(TEntity entity)
    {
        BeforeInsertAsync(entity).GetAwaiter().GetResult();
        return base.Insert(entity);
    }

    public override async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await BeforeInsertAsync(entity);
        return await base.InsertAsync(entity, cancellationToken);
    }

    public override List<TEntity> Insert(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            BeforeInsertAsync(entity).GetAwaiter().GetResult();
        }

        return base.Insert(entities);
    }

    public override async Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (TEntity entity in entities)
        {
            await BeforeInsertAsync(entity);
        }

        return await base.InsertAsync(entities, cancellationToken);
    }

    #endregion

    #region Update

    private readonly Expression<Func<TEntity, object>> _updateIgnoreExp = e => new { e.CreateUserId, e.CreateTime };

    public override int Update(TEntity entity)
    {
        BeforeUpdateAsync(entity).GetAwaiter().GetResult();
        return Orm.Update<TEntity>().SetSource(entity).IgnoreColumns(_updateIgnoreExp).ExecuteAffrows();
    }

    public override async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await BeforeUpdateAsync(entity);
        return await Orm.Update<TEntity>().SetSource(entity).IgnoreColumns(_updateIgnoreExp).ExecuteAffrowsAsync(cancellationToken);
    }

    public override int Update(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            BeforeUpdateAsync(entity).GetAwaiter().GetResult();
        }

        return Orm.Update<TEntity>().SetSource(entities).IgnoreColumns(_updateIgnoreExp).ExecuteAffrows();
    }

    public override async Task<int> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await BeforeUpdateAsync(entity);
        }

        return await Orm.Update<TEntity>().SetSource(entities).IgnoreColumns(_updateIgnoreExp).ExecuteAffrowsAsync(cancellationToken);
    }

    #endregion

    #region Delete

    public override int Delete(long id)
    {
        TEntity entity = Get(id);
        BeforeDeleteAsync(entity).GetAwaiter().GetResult();
        return base.Update(entity);
    }

    public override int Delete(TEntity entity)
    {
        base.Attach(entity);
        BeforeDeleteAsync(entity).GetAwaiter().GetResult();
        return base.Update(entity);
    }

    public override int Delete(IEnumerable<TEntity> entities)
    {
        base.Attach(entities);
        foreach (TEntity entity in entities)
        {
            BeforeDeleteAsync(entity).GetAwaiter().GetResult();
        }
        return base.Update(entities);
    }

    public override async Task<int> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        TEntity entity = await GetAsync(id, cancellationToken);
        await BeforeDeleteAsync(entity);
        return await base.UpdateAsync(entity, cancellationToken);
    }

    public override async Task<int> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Attach(entities);
        foreach (TEntity entity in entities)
        {
            await BeforeDeleteAsync(entity);
        }

        return await base.UpdateAsync(entities, cancellationToken);
    }

    public override async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        base.Attach(entity);
        await BeforeDeleteAsync(entity);
        return await base.UpdateAsync(entity, cancellationToken);
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
            BeforeDeleteAsync(entity).GetAwaiter().GetResult();
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
            await BeforeDeleteAsync(entity);
        }

        return await base.UpdateAsync(items, cancellationToken);
    }

    #endregion

    #region InsertOrUpdate
    public override TEntity InsertOrUpdate(TEntity entity)
    {
        BeforeInsertAsync(entity).GetAwaiter().GetResult();
        return base.InsertOrUpdate(entity);
    }

    public override async Task<TEntity> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await BeforeInsertAsync(entity);
        return await base.InsertOrUpdateAsync(entity, cancellationToken);
    }

    #endregion
}
