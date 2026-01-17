using Memo.Blog.Application.Notes.Common;

namespace Memo.Blog.Application.Friends.Queries.Get;

public class GetNoteQueryHandler(IMapper mapper, IBaseDefaultRepository<Note> noteRepo) : IRequestHandler<GetNoteQuery, Result>
{
    public async Task<Result> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        var friend = await noteRepo.Select
            .Include(n => n.Catalog)
            .Include(n => n.Author)
            .Where(f => f.NoteId == request.NoteId).FirstAsync(cancellationToken);
        if (friend is null) throw new ApplicationException("笔记不存在");

        return Result.Success(mapper.Map<NoteResult>(friend));
    }
}
