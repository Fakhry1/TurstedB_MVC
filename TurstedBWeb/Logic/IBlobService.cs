using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using TrustedB.Models;
using TrustedBWeb.Areas.Admin.Controllers;
using TurstedB.DataAccess.Repository.IRepository;

namespace TrustedBWeb.Logic
{
    public interface IBlobService
    {
        Task<string> GetBlob(string name, string containerName);
        Task DownloadBlob(string name, string containerName, string localPath);

        Task<List<string>> GetAllBlobs(string containerName);
       // Task<List<Blob>> GetAllBlobsWithUri(string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName, TrustedB.Models.Blob blob);
        Task<bool> DeleteBlob(string name, string containerName);
    }
}