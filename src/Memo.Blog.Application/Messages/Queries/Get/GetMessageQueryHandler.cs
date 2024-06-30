using Memo.Blog.Application.Messages.Common;

namespace Memo.Blog.Application.Messages.Queries.Get;

public class GetMessageQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<GetMessageQuery, Result>
{
    public async Task<Result> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
       throw new NotImplementedException();
    }
}
