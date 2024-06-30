namespace Memo.Blog.Application.Messages.Common
{
    public class MessageResult
    {
        public MessageType MessageType { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }
    }
}
