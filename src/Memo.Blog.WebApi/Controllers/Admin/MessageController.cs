using Memo.Blog.Application.Messages.Commands.Create;
using Memo.Blog.Application.Messages.Commands.Update;
using Memo.Blog.Application.Messages.Queries.Page;

namespace Memo.Blog.WebApi.Controllers.Admin
{
    /// <summary>
    /// 通知消息
    /// </summary>
    public class MessageController(ISender mediator) : ApiAdminController
    {
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<Result> CreateAsync(CreateMessageCommand request)
        {
            return await mediator.Send(request);
        }

        /// <summary>
        /// 标为已读消息
        /// </summary>
        /// <returns></returns>
        [HttpPut("read")]
        public async Task<Result> ReadAsync(ReadMessageCommand request)
        {
            return await mediator.Send(request);
        }

        /// <summary>
        /// 获取消息分页
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<Result> PageAsync([FromQuery] PageMessageQuery request)
        {
            return await mediator.Send(request);
        }
    }
}
