namespace Memo.Blog.Application.OpenSources.Commands.Create;

public class CreateProjectCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<CreateProjectCommand, Result>
{
    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var exist = await categoryRepo.Select.AnyAsync(c => request.Name == c.Name, cancellationToken);
        if (exist) throw new ApplicationException("同名分类已存在");

        var category = new Category
        {
            Name = request.Name,
        };
        category = await categoryRepo.InsertAsync(category, cancellationToken);

        return category == null || category.Id == 0 ? throw new ApplicationException("保存分类失败") : Result.Success(category.CategoryId);
    }
}
