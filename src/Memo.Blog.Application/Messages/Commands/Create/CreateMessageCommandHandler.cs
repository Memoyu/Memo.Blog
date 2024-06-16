namespace Memo.Blog.Application.Messages.Commands.Create;

public class CreateMessageCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<CreateMessageCommand, Result>
{
    public async Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
