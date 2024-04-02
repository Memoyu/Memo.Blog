namespace Memo.Blog.Application.Abouts.Commands.Update;

[Authorize(Permissions = ApiPermission.About.Update)]
public record UpdateAboutCommand(
    string Title,
    string Banner,
    List<string> Tags,
    string Content,
    bool Commentable) : IAuthorizeableRequest<Result>;

public class UpdateAboutCommandValidator : AbstractValidator<UpdateAboutCommand>
{
    public UpdateAboutCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("关于信息标题不能为空");

        RuleFor(x => x.Title)
                 .MinimumLength(1)
                 .MaximumLength(50)
                 .WithMessage("关于信息标题长度在1-50个字符之间");

        RuleFor(x => x.Tags)
          .NotEmpty()
          .WithMessage("关于信息个人标签不能为空");

        RuleFor(x => x.Tags)
          .Must(x => x.Count >= 1)
          .WithMessage("关于信息个人标签个数不能小于1个");

        RuleFor(x => x.Content)
          .NotEmpty()
          .WithMessage("关于信息内容不能为空");
    }
}

