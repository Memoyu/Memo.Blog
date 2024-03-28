namespace Memo.Blog.Application.Tags.Commands.Create;

public class CreateTagCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Tag> tagRepo
    ) : IRequestHandler<CreateTagCommand, Result>
{
    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {

        var exist = await tagRepo.Select.AnyAsync(c => request.Name == c.Name, cancellationToken);
        if (exist) throw new ApplicationException("标签已存在");

        var tag = new Tag
        {
            Name = request.Name,
            Color = request.Color,
        };
        tag = await tagRepo.InsertAsync(tag, cancellationToken);

        return tag == null || tag.Id == 0 ? throw new ApplicationException("保存标签失败") : Result.Success(tag.TagId);
    }
}
