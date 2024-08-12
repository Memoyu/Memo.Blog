namespace Memo.Blog.Application.Configs.Commands.Update;

public class UpdateConfigCommandHandler(
    IBaseDefaultRepository<Category> categoryRepo
    ) : IRequestHandler<UpdateConfigCommand, Result>
{
    public async Task<Result> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
