using TrustedB.DataAccess.Repository.IRepository;
using TrustedB.Models;
using TrustedB.DataAccess.Data;

namespace TrustedB.DataAccess.Repository
{
    public class CommentHistoryRepository : Repository<CommentHistory>, ICommentHistoryRepository
    {
        private ApplicationDbContext _db;
        public CommentHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CommentHistory obj)
        {
            _db.CommentHistory.Update(obj);
        }
    }
}
