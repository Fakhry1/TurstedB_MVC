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

            if (SubCategory == 8) { ContainerName = "reading-speetches"; }
            if (SubCategory == 9) { ContainerName = "image-holiya"; }
            if (SubCategory == 10) { ContainerName = "image-public-events"; }
            if (SubCategory == 11) { ContainerName = "audio-speeches"; }
            if (SubCategory == 12) { ContainerName = "audio-khoutap"; }
            if (SubCategory == 13) { ContainerName = "video-public-events"; }
            if (SubCategory == 14) { ContainerName = "video-selections"; }
            if (SubCategory == 15) { ContainerName = "reading-khoutap"; }
            if (SubCategory == 16) { ContainerName = "reading-books"; }


            return ContainerName;
        }


        public bool StateTransition(int? oldState, int? newState, List<string> Roles)
        {
            var transistionList = _unitOfWork.StateTransition.GetAll().ToList();


            var result = false;
            var resultRole = false;
            foreach (var state in transistionList)
            {

                List<string> rolesFromTstate;// = new List<string>();
                if (state.Statefrom == oldState && state.Stateto == newState)
                {
                    if (state.RoleName != null)
                    {
                        foreach (var includeProp in state.RoleName
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            foreach (var role in Roles)
                            {
                                if (role == includeProp)
                                {
                                    resultRole = true;
                                    break;
                                }
                            }
                            //rolesFromTstate = rolesFromTstate.Add(includeProp);

                        }
                    }
                }
                if (state.Statefrom == oldState && state.Stateto == newState && resultRole)
                {
                    result = true;
                    break;
                }

            }
            return result;

        }

    }
}
