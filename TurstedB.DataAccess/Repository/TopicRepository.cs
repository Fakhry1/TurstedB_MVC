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
    public class TopicRepository : Repository<Topics>, ITopicRepository
    {
        private ApplicationDbContext _db;
        public TopicRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Topics obj)
        {
            _db.Topics.Update(obj);
        }
    }
}
