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
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        private ApplicationDbContext _db;
        public SubCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SubCategory obj)
        {
            _db.SubCategory.Update(obj);
        }
    }
}
