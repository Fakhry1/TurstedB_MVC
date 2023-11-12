
using TrustedB.DataAccess.Repository.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustedB.DataAccess.Data;
using TrustedB.DataAccess.Repository;

using TrustedB.Models;
using TurstedB.DataAccess.Repository.IRepository;

namespace TrustedB.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
    

        public ITopicRepository Topics { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
           
            Topics = new   TopicRepository(_db);


        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
