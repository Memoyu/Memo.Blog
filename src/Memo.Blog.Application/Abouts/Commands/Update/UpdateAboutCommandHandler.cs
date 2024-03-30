namespace Memo.Blog.Application.Abouts.Commands.Update;

public class UpdateAboutCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<About> aboutRepo
    ) : IRequestHandler<UpdateAboutCommand, Result>
{
    public async Task<Result> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = mapper.Map<About>(request);
        var entity = await aboutRepo.Select.FirstAsync(cancellationToken);
        if (entity is null)
        {
            about = await aboutRepo.InsertAsync(about, cancellationToken);
            return about.Id == 0 ? Result.Failure("新增关于信息失败") : Result.Success(about.Id);
        }

        about.Id = entity.Id;
        var affrows = await aboutRepo.UpdateAsync(about, cancellationToken);
        return affrows > 0 ? Result.Success() : Result.Failure("更新关于信息失败");
    }
}
