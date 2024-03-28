namespace Memo.Blog.Application.Moments.Commands.Update;

public class UpdateMomentCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<UpdateMomentCommand, Result>
{
    public Task<Result> Handle(UpdateMomentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
