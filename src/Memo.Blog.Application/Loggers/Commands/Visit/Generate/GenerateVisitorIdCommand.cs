namespace Memo.Blog.Application.Loggers.Commands.Visit.Generate;

public record GenerateVisitorIdCommand(string Os, string Browser) : IRequest<Result>;
