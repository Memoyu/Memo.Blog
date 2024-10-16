﻿namespace Memo.Blog.Application.OpenSources.Queries.Get;

[Authorize(Permissions = ApiPermission.OpenSource.Get)]
public record GetProjectQuery(long ProjectId) : IAuthorizeableRequest<Result>;

public class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
{
    public GetProjectQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("项目Id不能为空");
    }
}

public record GetProjectClientQuery(long ProjectId) : IRequest<Result>;

public class GetProjectClientQueryValidator : AbstractValidator<GetProjectClientQuery>
{
    public GetProjectClientQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("项目Id不能为空");
    }
}
