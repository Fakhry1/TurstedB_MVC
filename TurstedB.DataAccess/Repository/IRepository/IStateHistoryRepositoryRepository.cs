using TrustedB.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrustedB.DataAccess.Repository.IRepository
{
    public interface IStateHistoryRepositoryRepository : IRepository<StateHistory>
    {
               void Update(StateHistory obj);
    }
}
