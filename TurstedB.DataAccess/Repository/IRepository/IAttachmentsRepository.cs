using TrustedB.Models;
using TrustedB.DataAccess.Repository.IRepository;

namespace TrustedB.DataAccess.Repository.IRepository
{
    public interface IAttachmentsRepository : IRepository<Attachments>
    {
               void Update(Attachments obj);
    }
}
