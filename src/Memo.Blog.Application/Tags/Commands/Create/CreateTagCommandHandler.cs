using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Commands.Create;
public class CreateTagCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Tag> _tagResp
    ) : IRequestHandler<CreateTagCommand, Result>
{
    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Name = request.Name,
            Color = request.Color,
        };

        tag = await _tagResp.InsertAsync(tag, cancellationToken);

        var result = _mapper.Map<TagResult>(tag);

        return Result.Success(result);
    }
}
