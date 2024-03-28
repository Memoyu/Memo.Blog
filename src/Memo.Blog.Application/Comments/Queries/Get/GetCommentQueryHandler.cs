using Memo.Blog.Application.Comments.Common;

namespace Memo.Blog.Application.Comments.Queries.Get;
public class GetCommentQueryHandler(IMapper mapper, IBaseDefaultRepository<Comment> commentRepo) : IRequestHandler<GetCommentQuery, Result>
{
    public async Task<Result> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await commentRepo.Select.Where(f => f.CommentId == request.CommentId).FirstAsync(cancellationToken);

        return comment is null ? throw new ApplicationException("评论不存在") : (Result)Result.Success(mapper.Map<CommentResult>(comment));
    }
}
