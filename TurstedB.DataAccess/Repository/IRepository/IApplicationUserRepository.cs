using TrustedB.Models;
using TrustedB.DataAccess.Repository.IRepository;

namespace TrustedB.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
               void Update(ApplicationUser obj);
    }
}
