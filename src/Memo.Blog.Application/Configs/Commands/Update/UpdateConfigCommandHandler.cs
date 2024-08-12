namespace Memo.Blog.Application.Configs.Commands.Update;

public class UpdateConfigCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Config> configRepo
    ) : IRequestHandler<UpdateConfigCommand, Result>
{
    public async Task<Result> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        var update = mapper.Map<Config>(request);
        var config = await configRepo.Select.FirstAsync(cancellationToken);
        if (config is null)
        {
            update = await configRepo.InsertAsync(update, cancellationToken);
            return update.Id == 0 ? Result.Failure("新增系统配置失败") : Result.Success(update.Id);
        }

        update.Id = config.Id;
        var affrows = await configRepo.UpdateAsync(update, cancellationToken);
        return affrows > 0 ? Result.Success() : Result.Failure("更新系统配置失败");
    }
}
