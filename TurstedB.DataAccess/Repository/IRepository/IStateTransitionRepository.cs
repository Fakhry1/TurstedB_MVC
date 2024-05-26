using TrustedB.Models;
using TrustedB.DataAccess.Repository.IRepository;

namespace TrustedB.DataAccess.Repository.IRepository
{
    public interface IStateTransitionRepository : IRepository<StateTransition>
    {
               void Update(StateTransition obj);
    }
}
