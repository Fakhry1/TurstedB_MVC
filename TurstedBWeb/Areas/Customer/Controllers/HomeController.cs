using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using TrustedB.DataAccess.Data;
using TrustedB.Models;
using TrustedB.Models.ViewModels;
using TrustedBWeb.Areas.Admin.Controllers;
using TrustedBWeb.Logic;
using TurstedB.DataAccess.Repository.IRepository;
using TurstedBWeb.Models;

namespace TrustedBWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _appcontext;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        public string fileName = "";
        public string slash = @"\";

        public HomeController( IUnitOfWork unitOfWork, ApplicationDbContext appcontext, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
           
            _unitOfWork = unitOfWork;
            _appcontext = appcontext;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        //Guidance - check deploy
        public IActionResult Guidance(int? SubCategoryID, int pg = 1 )
        {

            const int pageSize = 3;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 1)).Count();
            var pager = new Pager((int)SubCategoryID, recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            var NewsList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.SubCategoryID == SubCategoryID)).ToList();

            //var TopicList = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).ToList();
            this.ViewBag.Pager = pager;


            return View(NewsList);

            
        }

        public async Task<IActionResult> GuidanceDetails(Guid? Id)
        {
            //Handel handel = new Handel(_unitOfWork);
           // var containername = 
            RequestDetailsVM requestDetailsVM = new RequestDetailsVM();
            requestDetailsVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == Id, includeProperties: "Category");
            requestDetailsVM.AttachmentsList = _unitOfWork.Attachments.GetAll(filter: o => (o.TopicId == Id)).ToList();
            //requestDetailsVM.StorageName = await handel.ListBlobContainersAsync();
            return View(requestDetailsVM);
        }


        //public IActionResult DownloadFile(Guid? id)
        //{
        //    string webRootPath = _hostEnvironment.WebRootPath;
        //    var fileName = _unitOfWork.Attachments.Get(u => u.FileId == id);

        //    string path = Path.Combine(webRootPath) + slash + fileName.FilePath;

        //    //Read the File data into Byte Array.
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    //Send the File to Download.
        //    return File(bytes, "application/octet-stream", fileName.FilePath);
        //}

        public async Task<IActionResult> DownloadAudioAsync(string? path)
        {
            //string webRootPath = _hostEnvironment.WebRootPath;
            //string filePath = Path.Combine(webRootPath) + slash + path;
            //byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            //return File(bytes, "application/octet-stream", "Image.png");

            //var fileName = _unitOfWork.Attachments.Get(u => u.FileId == id);

            List<string> storageData = new List<string>();
            foreach (var includeProp in path
                    .Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                storageData.Add(includeProp);
            }

            var blobName = storageData[3];
            var containerName = storageData[2];

            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = _configuration.GetValue<string>("BlobConnection");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                blockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }
            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);


        }

        //______________________________Audio____________________________________

        public IActionResult AllAudio(int? SubCategoryID, int pg = 1)
        {
            const int pageSize = 4;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o => (o.SubCategoryID == SubCategoryID)).Count(); 
            var pager = new Pager((int)SubCategoryID, recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            var AudioList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.SubCategoryID == SubCategoryID)).ToList();

            //var TopicList = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).ToList();
            this.ViewBag.Pager = pager;


            return View(AudioList);
            

        }

        //______________________________Audio____________________________________

        public IActionResult AllImages(int? SubCategoryID, int pg = 1)
        {

            const int pageSize = 4;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o =>(o.CategoryID == 1)).Count();
            var pager = new Pager((int)SubCategoryID,recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            var ImageList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.SubCategoryID == SubCategoryID)).ToList();
           
           
            this.ViewBag.Pager = pager;
          

            return View(ImageList);

        }

        //___________________________________Videos___________________________

        public IActionResult AllVideos(int? SubCategoryID, int pg = 1)
        {

            const int pageSize = 3;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == SubCategoryID)).Count();
            var pager = new Pager((int)SubCategoryID, recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            var ImageList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.SubCategoryID == SubCategoryID)).ToList();

            //var TopicList = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).ToList();
            this.ViewBag.Pager = pager;


            return View(ImageList);

        }

        //_______________________________storage____________________

        //public async Task<IActionResult> checkStorageAsync()
        //{
        //    //TestVM testVM = new TestVM();
        //    BlobService handel = new Handel(_unitOfWork);
        //    var containername = await handel.ListBlobContainersAsync();

        //    return View(containername);
        //}



        #region Localization
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
    }
}
