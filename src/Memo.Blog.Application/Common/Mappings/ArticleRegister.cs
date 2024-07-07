using System.Linq.Expressions;
using Memo.Blog.Application.Articles.Commands.Create;
using Memo.Blog.Application.Articles.Commands.Update;
using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Common.Mappings;

public class ArticleRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        Expression<Func<CreateArticleCommand, User>> userMap = s => MapContext.Current.GetService<IBaseDefaultRepository<User>>().Select.Where(u => u.UserId == MapContext.Current.GetService<ICurrentUserProvider>().GetCurrentUser().Id).ToOne();

        config.ForType<CreateArticleCommand, Article>()
            .Map(d => d.Status, s => s.Status ?? ArticleStatus.Draft) // 默认为保存到草稿
            .Map(d => d.WordNumber, s => s.Content.Length)
            .Map(d => d.ReadingTime, s => GetReadingTime(s.Content))
            .Map(d => d.Category, s => MapContext.Current.GetService<IBaseDefaultRepository<Category>>().Select.Where(c => c.CategoryId == s.CategoryId).ToOne())
            .Map(d => d.Author, userMap);

        config.ForType<UpdateArticleCommand, Article>()
            .Map(d => d.WordNumber, s => s.Content.Length)
            .Map(d => d.ReadingTime, s => GetReadingTime(s.Content));

        config.ForType<Article, PageArticleResult>()
            .Map(d => d.Comments, s => s.ArticleComments.Count)
            .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());

        config.ForType<Article, PageArticleClientResult>()
           .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());
        
        config.ForType<Article, RankingArticleResult>()
           .Map(d => d.Comments, s => s.ArticleComments.Count);

        config.ForType<Article, ArticleResult>()
        .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());

        config.ForType<Article, ArticleDetailResult>()
       .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());
    }

    private int GetReadingTime(string content) => (int)Math.Ceiling((decimal)content.Length / 800);

}
