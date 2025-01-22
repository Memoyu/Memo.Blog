using Memo.Blog.Application.Common.Interfaces.Services.Mail;
using Memo.Blog.Application.Common.Models.Mail;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Memo.Blog.Infrastructure_.Mail;

public class MailServiceTest
{
    private readonly IMailService _mailService;

    public MailServiceTest()
    {
        var settings = new Dictionary<string, string>
        {
            ["Authorization:Mail:Enable"] = "true",
            ["Authorization:Mail:DisplayName"] = "Memo博客系统通知",
            ["Authorization:Mail:Email"] = "memo.blog@aliyun.com",
            ["Authorization:Mail:Password"] = "memo.blog.6076",
            ["Authorization:Mail:Host"] = "smtp.mxhichina.com",
            ["Authorization:Mail:Port"] = "465",
            ["Authorization:Mail:EnableSsl"] = "true",
        };

        var services = new ServiceCollection();
        services.AddLogging();
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(settings!).Build();

        services.Configure<AuthorizationSettings>(configuration.GetSection("Authorization"));

        services.AddSingleton<IMailService, MailService>();
        var sp = services.BuildServiceProvider();

        _mailService = sp.GetRequiredService<IMailService>();
    }


    [Fact]
    public async Task Send_Should_Success()
    {
        _mailService.Send(new MailMsg { Tos = ["mmy6076@outlook.com"], Subject = "测试邮件", Body="dddddd" });
    }
}
