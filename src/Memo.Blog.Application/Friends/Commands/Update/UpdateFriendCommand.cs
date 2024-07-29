namespace Memo.Blog.Application.Friends.Commands.Update;

[Authorize(Permissions = ApiPermission.Friend.Update)]
public record UpdateFriendCommand(
    long FriendId, 
    string Nickname,
    string Description,
    string Site,
    string? Avatar,
    bool Showable) : IAuthorizeableRequest<Result>;

public class UpdateFriendCommandValidator : AbstractValidator<UpdateFriendCommand>
{
    public UpdateFriendCommandValidator()
    {
        RuleFor(x => x.FriendId)
            .Must(x => x > 0)
            .WithMessage("友链Id必须大于0");

        RuleFor(x => x.Nickname)
            .NotEmpty().WithMessage("友链昵称不能为空");

        RuleFor(x => x.Nickname)
                 .MinimumLength(1)
                 .MaximumLength(20)
                 .WithMessage("友链昵称长度在1-20个字符之间");

        RuleFor(x => x.Description)
          .NotEmpty().WithMessage("友链描述不能为空");

        RuleFor(x => x.Description)
            .MinimumLength(1)
            .MaximumLength(100)
            .WithMessage("友链描述长度在1-100个字符之间");

        RuleFor(x => x.Site)
             .NotEmpty()
             .Must(x => Uri.TryCreate(x, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
             .WithMessage("站点必须为http或https链接");
    }
}

