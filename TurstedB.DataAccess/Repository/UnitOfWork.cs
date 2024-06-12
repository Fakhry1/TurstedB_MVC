
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

        public IStateHistoryRepositoryRepository StateHistory { get; private set; }

        public ICommentHistoryRepository CommentHistory { get; private set; }

        public IAttachmentsRepository Attachments { get; private set; }

        public ITopicsStatesRepository TopicsStates { get; private set; }

        public IStateTransitionRepository StateTransition { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public ISubCategoryRepository SubCategory { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
           
            Topics = new   TopicRepository(_db);
            StateHistory = new StateHistoryRepository(_db);
            CommentHistory = new CommentHistoryRepository(_db);
            Attachments = new AttachmentsRepository(_db);
            TopicsStates = new TopicsStatesRepository(_db);
            StateTransition = new StateTransitionRepository(_db);
            Category = new CategoryRepository(_db);
            SubCategory = new SubCategoryRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
