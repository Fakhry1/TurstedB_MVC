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

        //private readonly string _storageAccount = "trustedbstorage";
        //private readonly string _accessKey = "Op0/NUvACwpy38UFNbywYhFT+z4AMCl9ODGbUuB4udg/Q6TmOeDeYbh3vBLGqkk2KXdbK5qQXTJO+AStWCRJFA==";
        private readonly BlobServiceClient _blobClient;

        public Handel(IUnitOfWork unitOfWork, BlobServiceClient blobClient)
        {
         _unitOfWork = unitOfWork;
            _blobClient = blobClient;
            //var credential = new StorageSharedKeyCredential(_storageAccount, _accessKey);
            //var blobUri = $"https://{_storageAccount}.blob.core.windows.net";
            //_blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);


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

        //________________________________storage_______________________

        public  async Task<List<string>> ListBlobContainersAsync()
        {
            List<string> containerNameList = new();
            var containers = _blobClient.GetBlobContainersAsync();
            await foreach (var container in containers) 
            {
                containerNameList.Add(container.Name);

            }

            return containerNameList;
        }


        public async Task<List<string>> ListBlobAsync(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            var blobString = new List<string>();
            await foreach (var item in blobs)
            {
                blobString.Add(item.Name);
            }

            return blobString;
        }


        public async Task<string> GetBlob(string name, string containreName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containreName);

            var blobClient = blobContainerClient.GetBlobClient(name);

            return blobClient.Uri.AbsoluteUri;
        }

            public async Task<bool> UploadFilesAsync(string name,IFormFile file, string containreName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containreName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

            if (result != null)
            {
                return true;
            }
            return false;

        }

    }
}
