using FreeSql;

namespace Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;

public interface IBaseDefaultRepository<TEntity> : IBaseRepository<TEntity, long> where TEntity : class
{
}
