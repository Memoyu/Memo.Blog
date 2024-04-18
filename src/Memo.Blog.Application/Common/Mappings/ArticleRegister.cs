﻿using System.Linq.Expressions;
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
            .Map(d => d.WordNumber, s => s.Content.Length)
            .Map(d => d.ReadingTime, s => s.Content.Length / 800)
            .Map(d => d.Category, s => MapContext.Current.GetService<IBaseDefaultRepository<Category>>().Select.Where(c => c.CategoryId == s.CategoryId).ToOne())
            .Map(d => d.Author, userMap);

        config.ForType<UpdateArticleCommand, Article>()
            .Map(d => d.WordNumber, s => s.Content.Length)
            .Map(d => d.ReadingTime, s => s.Content.Length / 800);

        config.ForType<Article, PageArticleResult>()
            .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());

        config.ForType<Article, ClientPageArticleResult>()
           .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());

        config.ForType<Article, ArticleResult>()
        .Map(d => d.Tags, s => s.ArticleTags.Select(at => at.Tag).ToList());
    }
}
