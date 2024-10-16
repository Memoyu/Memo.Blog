﻿using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Categories.Commands.Update;

public class UpdateCategoryCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.Select.Where(c => c.CategoryId == request.CategoryId).FirstAsync(cancellationToken)
            ?? throw new ApplicationException("分类不存在");

        var exist = await categoryRepo.Select.AnyAsync(c => c.CategoryId != request.CategoryId && request.Name == c.Name, cancellationToken);
        if (exist) throw new ApplicationException("分类名已存在");

        category.Name = request.Name;
        category.AddDomainEvent(new UpdatedArticleCategoryEvent(category));

        var affrows = await categoryRepo.UpdateAsync(category, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新分类失败");
    }
}
