namespace Memo.Blog.Application.Abouts.Commands.Update;

public class UpdateAboutCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<About> aboutRepo
    ) : IRequestHandler<UpdateAboutCommand, Result>
{
    public async Task<Result> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var update = mapper.Map<About>(request);
        var about = await aboutRepo.Select.FirstAsync(cancellationToken);
        if (about is null)
        {
            update = await aboutRepo.InsertAsync(update, cancellationToken);
            return update.Id == 0 ? Result.Failure("新增关于信息失败") : Result.Success(update.Id);
        }

        update.Id = about.Id;
        var affrows = await aboutRepo.UpdateAsync(update, cancellationToken);
        return affrows > 0 ? Result.Success() : Result.Failure("更新关于信息失败");
    }
}
