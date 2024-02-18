using Memo.Blog.Application.Articles.Commands.Create;

namespace Memo.Blog.Application.Common.Mappings;

public class ArticleRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        var tagRepo = MapContext.Current.GetService<IBaseDefaultRepository<Tag>>();

        config.ForType<CreateArticleCommand, Article>()
            .Map(d => d.Tags, s => tagRepo.Select)
    }
}
