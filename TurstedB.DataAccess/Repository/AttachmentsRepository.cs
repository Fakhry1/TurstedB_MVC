using TrustedB.DataAccess.Repository.IRepository;
using TrustedB.Models;
using TrustedB.DataAccess.Data;




namespace TrustedB.DataAccess.Repository
{
    public class AttachmentsRepository : Repository<Attachments>, IAttachmentsRepository
    {
        private ApplicationDbContext _db;
        public AttachmentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Attachments obj)
        {
            _db.Attachments.Update(obj);
        }
    }
}
