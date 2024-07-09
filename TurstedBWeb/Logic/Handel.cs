using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using TrustedB.Models;
using TrustedBWeb.Areas.Admin.Controllers;
using TurstedB.DataAccess.Repository.IRepository;

namespace TrustedBWeb.Logic
{
    public class Handel
    {
        private readonly IUnitOfWork _unitOfWork;

        
        public Handel(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;

        }
        public bool StateTransition(int? oldState, int? newState)
        {
            var transistionList = _unitOfWork.StateTransition.GetAll().ToList();
            var result = false;
            foreach (var state in transistionList)
            {
                if (state.Statefrom == oldState && state.Stateto == newState)
                {
                    result = true;
                    break;
                }

            }
            return result;

        }

        public string StateName(int? newState)
        {
            var stateName = _unitOfWork.TopicsStates.Get(u => u.stateId == newState).ArabicName;
            return stateName;
        }

        public string GetContainerName(int? SubCategory)
        {
            string ContainerName = "";
           if(SubCategory == 1) { ContainerName = "guidance"; }
           if(SubCategory == 2) { ContainerName = "image-murals"; }
           if (SubCategory == 3) { ContainerName = "image-moualid";}
           if (SubCategory == 4) { ContainerName = "video-holiya-events"; }
           if (SubCategory == 5) { ContainerName = "video-moualid-events"; }
           if (SubCategory == 6) { ContainerName = "audio-qaseid-hadarat"; }
           if (SubCategory == 7) { ContainerName = "audio-lessons"; }


            return ContainerName;
        }

    }
}
