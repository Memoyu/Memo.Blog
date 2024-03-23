namespace Memo.Blog.Application.Tags.Commands.Create;

public class CreateTagCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Tag> tagResp
    ) : IRequestHandler<CreateTagCommand, Result>
{
    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {

        var exist = await tagResp.Select.AnyAsync(c => request.Name == c.Name, cancellationToken);
        if (exist) return Result.Failure("标签已存在");

        var tag = new Tag
        {
            Name = request.Name,
            Color = request.Color,
        };
        tag = await tagResp.InsertAsync(tag, cancellationToken);

        return tag == null || tag.Id == 0 ? Result.Failure("保存标签失败") : Result.Success(tag.TagId);
    }
}
