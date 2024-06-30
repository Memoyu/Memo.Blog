using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Domain.Events.Messages;

namespace Memo.Blog.Application.Common.Mappings;

public class MessageRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateMessageEvent, Message>()
             .Map(d => d.MessageId, s => SnowFlakeUtil.NextId())
             .Map(d => d.UserId, s => s.UserId)
             .Map(d => d.MessageType, s => s.MessageType)
             .Map(d => d.Content, s => s.Content);

        config.ForType<CreateMessageEvent, MessageNotificationEvent>()
             .Map(d => d.Type, s => s.MessageType)
             .Map(d => d.Content, s => GetMessageContentFormat(s.UserId, s.MessageType, s.Content));

        config.ForType<Message, MessageResult>()
             .Map(d => d.MessageId, s => s.MessageId)
             .Map(d => d.MessageType, s => s.MessageType)
             .Map(d => d.Content, s => GetMessageContentFormat(s.UserId, s.MessageType, s.Content))
             .Map(d => d.CreateTime, s => s.CreateTime);
    }

    private string GetMessageTitle(MessageType type) => type switch
    {
        MessageType.User => "收到用户发来消息",
        MessageType.Comment => "收到新评论",
        MessageType.Like => "收到新点赞",
        _ => throw new NotImplementedException("未定义消息类型标题"),
    };

    private string GetMessageContentFormat(long userId, MessageType type, string content)
    {
        var formatContent = string.Empty;
        switch (type)
        {
            case MessageType.User:
                var userMessage = content.ToDesJson<UserMessageContent>() ?? throw new Exception("消息格式错误");
                var user = MapContext.Current.GetService<IBaseDefaultRepository<User>>().Select.Where(c => c.UserId == userId).First();
                formatContent = new UserMessageResult { Content = userMessage.Content, UserNickname = user.Nickname, UserAvatar = user.Avatar }.ToJson();
                break;

            case MessageType.Comment:
                var commentMessage = content.ToDesJson<CommentMessageContent>() ?? throw new Exception("消息格式错误");
                var commentVisitor = GetVisitorInfo(userId);
                formatContent = new CommentMessageResult
                {
                    Content = commentMessage.Content,
                    BelongId = commentMessage.BelongId,
                    CommentType = commentMessage.CommentType,
                    VisitorNickname = commentVisitor.Nickname,
                    VisitorAvatar = commentVisitor.Avatar,
                    Title = GetBelongTitle(commentMessage.CommentType, commentMessage.BelongId)
                }.ToJson();
                break;

            case MessageType.Like:
                var likeMessage = content.ToDesJson<LikeMessageContent>() ?? throw new Exception("消息格式错误");
                var likeVisitor = GetVisitorInfo(userId);
                formatContent = new LikeMessageResult
                {
                    BelongId = likeMessage.BelongId,
                    LikeType = likeMessage.LikeType,
                    VisitorNickname = likeVisitor.Nickname,
                    VisitorAvatar = likeVisitor.Avatar,
                    Title = GetBelongTitle(likeMessage.LikeType, likeMessage.BelongId)
                }.ToJson();
                break;
        }

        return formatContent;
    }

    private (string Nickname, string Avatar) GetVisitorInfo(long visitorId)
    {
        var visitor = MapContext.Current.GetService<IBaseDefaultRepository<Visitor>>().Select.Where(c => c.VisitorId == visitorId).First();

        return (string.IsNullOrWhiteSpace(visitor?.Nickname) ? "未知" : visitor!.Nickname,
                string.IsNullOrWhiteSpace(visitor?.Avatar) ? string.Empty : visitor!.Avatar);
    }

    private string GetBelongTitle(BelongType type, long belongId)
    {
        var typeName = type.GetDescription();

        var title = type switch
        {
            BelongType.Article => MapContext.Current.GetService<IBaseDefaultRepository<Article>>().Select.Where(c => c.ArticleId == belongId).First(a => a.Title),
            BelongType.Moment => typeName,
            BelongType.About => typeName,
            _ => throw new NotImplementedException("未定义消息所属类型消息内容的转换"),
        };

        return title;
    }
}
