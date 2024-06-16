namespace Memo.Blog.Application.Messages.Commands.Update;

public class ReadMessageCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<ReadMessageCommand, Result>
{
    public async Task<Result> Handle(ReadMessageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
