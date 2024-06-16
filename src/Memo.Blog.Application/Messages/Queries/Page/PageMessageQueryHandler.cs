namespace Memo.Blog.Application.Messages.Queries.Page;

public class PageMessageQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<PageMessageQuery, Result>
{
    public async Task<Result> Handle(PageMessageQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

