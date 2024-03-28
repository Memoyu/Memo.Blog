
namespace Memo.Blog.Application.Moments.Commands.Create;

public class CreateMomentCommandHandler(
    IMapper mapper
    ) : IRequestHandler<CreateMomentCommand, Result>
{
    public Task<Result> Handle(CreateMomentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
