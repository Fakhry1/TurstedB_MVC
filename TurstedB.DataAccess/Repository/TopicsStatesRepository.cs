using TrustedB.DataAccess.Repository.IRepository;

using TrustedB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustedB.DataAccess.Data;



namespace TrustedB.DataAccess.Repository
{
    public class TopicsStatesRepository : Repository<TopicsStates>, ITopicsStatesRepository
    {
        private ApplicationDbContext _db;
        public TopicsStatesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TopicsStates obj)
        {
            _db.TopicsStates.Update(obj);
        }
    }
}
