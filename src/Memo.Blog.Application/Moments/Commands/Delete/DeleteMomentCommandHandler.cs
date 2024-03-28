namespace Memo.Blog.Application.Moments.Commands.Delete;

public class DeleteMomentCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<DeleteMomentCommand, Result>
{
    public Task<Result> Handle(DeleteMomentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
