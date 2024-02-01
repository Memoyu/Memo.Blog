using FreeSql;

namespace Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;

public interface IBaseAuditRepository<TEntity> : IBaseRepository<TEntity, long> where TEntity : class
{
}
