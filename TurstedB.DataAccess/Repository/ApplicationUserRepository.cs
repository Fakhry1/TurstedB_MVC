using TrustedB.DataAccess.Repository.IRepository;
using TrustedB.Models;
using TrustedB.DataAccess.Data;




namespace TrustedB.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUser.Update(obj);
        }
    }
}
