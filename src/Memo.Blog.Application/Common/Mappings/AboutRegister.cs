using Memo.Blog.Application.Abouts.Commands.Update;
using Memo.Blog.Application.Abouts.Common;

namespace Memo.Blog.Application.Common.Mappings;

public class AboutRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<About, AboutResult>()
           .Map(d => d.Tags, s => s.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries));

        config.ForType<UpdateAboutCommand, About>()
          .Map(d => d.Tags, s => string.Join("," , s.Tags));
    }
}
