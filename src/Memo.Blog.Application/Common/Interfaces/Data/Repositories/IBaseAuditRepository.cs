using FreeSql;

namespace Memo.Blog.Application.Common.Interfaces.Data.Repositories;

public interface IBaseAuditRepository<TEntity> : IBaseRepository<TEntity, long> where TEntity : class
{
}
