using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).ToOneAsync(cancellationToken);
        if (category == null) throw new ApplicationException("分类不存在");

        category.AddDomainEvent(new ArticleUpdateCategoryEvent(category.CategoryId, InitConst.InitCategoryId));

        var rows = await categoryRepo.DeleteAsync(category, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("删除分类失败");
    }
}
