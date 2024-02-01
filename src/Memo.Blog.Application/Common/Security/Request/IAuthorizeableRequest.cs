namespace Memo.Blog.Application.Common.Security.Request;

public interface IAuthorizeableRequest<T> : IRequest<T>
{
    long UserId { get; }
}
