using Memo.Blog.Application.Categories.Common;

namespace Memo.Blog.Application.Categories.Commands.Create;
public class CreateCategoryCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Category> categoryResp
    ) : IRequestHandler<CreateCategoryCommand, Result>
{
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
        };

        category = await categoryResp.InsertAsync(category, cancellationToken);

        var result = _mapper.Map<CategoryResult>(category);

        return Result.Success(result);
    }
}
