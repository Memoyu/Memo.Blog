namespace Memo.Blog.Application.Common.Request;

public interface IAuthorizeableRequest<T> : IRequest<T>
{
    long UserId { get; }
}
