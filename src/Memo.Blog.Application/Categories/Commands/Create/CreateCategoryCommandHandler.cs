namespace Memo.Blog.Application.Categories.Commands.Create;

public class CreateCategoryCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Category> categoryResp
    ) : IRequestHandler<CreateCategoryCommand, Result>
{
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var exist = await categoryResp.Select.AnyAsync(c => request.Name == c.Name, cancellationToken);
        if (exist) throw new ApplicationException("分类已存在");

        var category = new Category
        {
            Name = request.Name,
        };
        category = await categoryResp.InsertAsync(category, cancellationToken);

        return category == null || category.Id == 0 ? throw new ApplicationException("保存分类失败") : Result.Success(category.CategoryId);
    }
}
