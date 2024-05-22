
using TrustedB.Models;

namespace TrustedB.DataAccess.Repository.IRepository
{
    public interface ICommentHistoryRepository : IRepository<CommentHistory>
    {
               void Update(CommentHistory obj);
    }
}
