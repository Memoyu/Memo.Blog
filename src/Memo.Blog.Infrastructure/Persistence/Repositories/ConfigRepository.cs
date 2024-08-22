using FreeSql;
using MediatR;
using Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Infrastructure.Persistence.Repositories;
public class ConfigRepository : BaseDefaultRepository<Config>, IConfigRepository
{
    public ConfigRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUserProvider currentUserProvider, IPublisher publisher) : base(unitOfWorkManager, currentUserProvider, publisher)
    {
    }

    public async Task<Config> GetWithInitAsync(CancellationToken cancellationToken = default)
    {
        var config = await Select.FirstAsync(cancellationToken);

        config ??= await InsertAsync(new Config(), cancellationToken);

        return config.Id == 0 ? throw new ApplicationException("新增系统配置失败") : config;
    }
}
