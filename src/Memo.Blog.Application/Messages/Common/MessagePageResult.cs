using Memo.Blog.Application.Common.Models;

namespace Memo.Blog.Application.Messages.Common
{
    public class MessagePageResult: PaginationResult<MessageResult>
    {
        public MessagePageResult(IReadOnlyList<MessageResult> items) : base(items)
        {
        }

        public int UnReads { get; set; }
    }
}
