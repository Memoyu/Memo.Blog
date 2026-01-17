namespace Memo.Blog.Application.Friends.Queries.Get;

[Authorize(Permissions = ApiPermission.Note.Get)]
public record GetNoteQuery(long NoteId) : IAuthorizeableRequest<Result>;


public class GetNoteQueryValidator : AbstractValidator<GetNoteQuery>
{
    public GetNoteQueryValidator()
    {
        RuleFor(x => x.NoteId)
            .NotEmpty()
            .WithMessage("笔记Id不能为空");
    }
}

