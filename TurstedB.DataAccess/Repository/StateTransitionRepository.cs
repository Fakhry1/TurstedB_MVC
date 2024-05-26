using TrustedB.DataAccess.Repository.IRepository;
using TrustedB.DataAccess.Repository;
using TrustedB.Models;
using TrustedB.DataAccess.Data;




namespace TrustedB.DataAccess.Repository
{
    public class StateTransitionRepository : Repository<StateTransition>, IStateTransitionRepository
    {
        private ApplicationDbContext _db;
        public StateTransitionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StateTransition obj)
        {
            _db.StateTransition.Update(obj);
        }
    }
}
