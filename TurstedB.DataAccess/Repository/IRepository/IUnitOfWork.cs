using TurstedB.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustedB.DataAccess.Repository.IRepository;

namespace TurstedB.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
       
        ITopicRepository Topics { get; }
        ICommentHistoryRepository CommentHistory { get; }
        IStateHistoryRepositoryRepository StateHistory { get; }
        IAttachmentsRepository Attachments { get; }
        ITopicsStatesRepository TopicsStates { get; }
        IStateTransitionRepository StateTransition { get; }

        void Save();
    }
}
