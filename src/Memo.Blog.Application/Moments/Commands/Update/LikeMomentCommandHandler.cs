using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Events.Messages;

namespace Memo.Blog.Application.Moments.Commands.Update;

public class LikeMomentCommandHandler(
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Moment> momentRepo,
    IBaseDefaultRepository<MomentLike> monentLikeRepo
    ) : IRequestHandler<LikeMomentCommand, Result>
{
    public async Task<Result> Handle(LikeMomentCommand request, CancellationToken cancellationToken)
    {
        var visitorId = currentUserProvider.GetCurrentVisitor();
        if (visitorId <= 0) throw new ApplicationException("点赞动态失败");

        var moment = await momentRepo.Select.Where(t => t.MomentId == request.MomentId).FirstAsync(cancellationToken)
            ?? throw new ApplicationException("动态不存在");

        // 已经点过赞
        var hasLike = await monentLikeRepo.Select.AnyAsync(al => al.VisitorId == visitorId && al.MomentId == moment.MomentId, cancellationToken);
        if (hasLike) return Result.Success();

        var momentLike = new MomentLike { MomentId = moment.MomentId, VisitorId = visitorId };

        // 动态点赞事件
        momentLike.AddDomainEvent(new CreateMessageEvent
        {
            UserId = visitorId,
            ToUsers = [moment.CreateUserId],
            ToRoles = [InitConst.InitAdminRoleId, InitConst.InitVisitorRoleId], // 按理说，只应该给管理员发以及作者，此处加上游客，演示用
            MessageType = MessageType.Like,
            Content = new LikeMessageContent
            {
                LikeType = BelongType.Moment,
                BelongId = moment.MomentId,
            }.ToJson()
        });

        momentLike = await monentLikeRepo.InsertAsync(momentLike, cancellationToken)
            ?? throw new ApplicationException("点赞动态失败");

        return momentLike.Id > 0 ? Result.Success() : throw new ApplicationException("点赞动态失败");
    }
}

