using Memo.Blog.Application.Comments.Common;

namespace Memo.Blog.Application.Comments.Queries.Get;
public class GetCommentQueryHandler(IMapper mapper, IBaseDefaultRepository<Comment> commentRepo) : IRequestHandler<GetCommentQuery, Result>
{
    public async Task<Result> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var friend = await commentRepo.Select.Where(f => f.CommentId == request.CommentId).FirstAsync(cancellationToken);
        if (friend is null) throw new ApplicationException("评论不存在");

        return Result.Success(mapper.Map<CommentResult>(friend));
    }
}
