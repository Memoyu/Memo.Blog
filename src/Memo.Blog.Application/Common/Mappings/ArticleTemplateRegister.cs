using Memo.Blog.Application.ArticleTemplates.Commands.Create;
using Memo.Blog.Application.ArticleTemplates.Commands.Update;

namespace Memo.Blog.Application.Common.Mappings;

public class ArticleTemplateRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateArticleTemplateCommand, ArticleTemplate>()
           .Map(d => d.Content, s => s.Content ?? string.Empty);
        
        config.ForType<UpdateArticleTemplateCommand, ArticleTemplate>()
           .Map(d => d.Content, s => s.Content ?? string.Empty);
    }
}
