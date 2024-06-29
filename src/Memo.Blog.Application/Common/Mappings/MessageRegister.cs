using Memo.Blog.Application.Messages.Commands.Create;
using Memo.Blog.Domain.Events.Messages;

namespace Memo.Blog.Application.Common.Mappings;

public class MessageRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateMessageCommand, CreateMessageEvent>()
            .Map(d => d.ToId, s => s.ToId)
            .Map(d => d.Content, s => s.ToId);

        config.ForType<CreateMessageEvent, Message>();

        config.ForType<Message, MessageNotificationEvent>();
    }
}
