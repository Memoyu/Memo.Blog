namespace Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;

public interface IConfigRepository: IBaseDefaultRepository<Config>
{
    Task<Config> GetWithInitAsync(CancellationToken cancellationToken = default);
}
