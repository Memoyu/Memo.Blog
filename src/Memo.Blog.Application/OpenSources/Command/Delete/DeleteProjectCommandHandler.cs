namespace Memo.Blog.Application.OpenSources.Commands.Delete;

public class DeleteCategoryCommandHandler(
   IBaseDefaultRepository<OpenSource> openSourceRepo
    ) : IRequestHandler<DeleteProjectCommand, Result>
{
    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await openSourceRepo.Select.Where(p => p.ProjectId == request.ProjectId).FirstAsync(cancellationToken) ?? throw new ApplicationException("项目不存在");

        var affrows = await openSourceRepo.DeleteAsync(project, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("删除项目失败");
    }
}
