using Memo.Blog.Application.Moments.Commands.Create;
using Memo.Blog.Application.Moments.Commands.Update;
using Memo.Blog.Application.Moments.Common;

namespace Memo.Blog.Application.Common.Mappings;

public class MomentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Moment, MomentResult>()
           .Map(d => d.Tags, s => s.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries));

        config.ForType<UpdateMomentCommand, Moment > ()
           .Map(d => d.Tags, s => string.Join(',', s.Tags.Where(s => !string.IsNullOrWhiteSpace(s)).ToList()));

        config.ForType<CreateMomentCommand, Moment>()
          .Map(d => d.Tags, s => string.Join(',', s.Tags.Where(s => !string.IsNullOrWhiteSpace(s)).ToList()));
    }
}
