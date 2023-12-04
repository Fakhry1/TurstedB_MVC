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
        IStateHistoryRepositoryRepository StateHistory { get; }

        void Save();
    }
}
