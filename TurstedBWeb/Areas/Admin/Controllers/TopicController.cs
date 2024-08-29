using Azure.Core;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TrustedB.DataAccess.Repository;
using TrustedB.Models;
using TrustedB.Models.ViewModels;
using TrustedB.Utility;
using TrustedBWeb.Logic;
using TurstedB.DataAccess.Repository.IRepository;
using TurstedBWeb.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace TrustedBWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ILogger<TopicController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobService _blobService;
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobClient;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _hostEnvironment;
        public string fileName = "";
        public string slash = @"\";

        //private readonly IBlobService _blobService;
        //public var topicList;
        public TopicController(ILogger<TopicController> logger, IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IBlobService blobService, BlobServiceClient blobClient, IConfiguration configuration)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _blobService = blobService;
            _blobClient = blobClient;
            _configuration = configuration;
        }

        public IActionResult Index(int pg = 1)
        {
            var topicList = new List<Topics>();
            const int pageSize = 10;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll().Count();
            var pager = new Pager(null, recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
           
            topicList = _unitOfWork.Topics.GetAllPagination(recSkip, pager.PageSize,null, includeProperties: "ApplicationUser,TopicsStates,Category").ToList();

            this.ViewBag.Pager = pager;

            //  topicList = _unitOfWork.Topics.GetAll(includeProperties: "ApplicationUser,TopicsStates,Category").ToList();


            return View(topicList);
        }



        public IActionResult Upsert(Guid? id)
        {
            TopicVM topicVM = new()
            {
                topic = new Topics(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ArabicName,
                    Value = i.CategoryId.ToString()
                }),
                SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(i => new SelectListItem
                {
                    Text = i.SubArabicName,
                    Value = i.SubCategoryId.ToString()
                })
            };

            if (id == null)
            {
                //new 
                return View(topicVM);
            }
            else
            {
                //eidt
                topicVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == id, includeProperties: "ApplicationUser,TopicsStates,Category");
                topicVM.StateHistoryList = _unitOfWork.StateHistory.GetAll(filter: o => (o.TopicId == topicVM.topic.TopicId), includeProperties: "ApplicationUser").ToList();
                topicVM.AttachmentsList = _unitOfWork.Attachments.GetAll(filter: o => (o.TopicId == topicVM.topic.TopicId), includeProperties: "ApplicationUser,TopicsStates").ToList();
                topicVM.CommentList = _unitOfWork.CommentHistory.GetAll(filter: o => (o.TopicId == topicVM.topic.TopicId), includeProperties: "ApplicationUser,TopicsStates").ToList();

                return View(topicVM);
            }

        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UpsertAsync(TopicVM topicVM)
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;

            var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationuser = _unitOfWork.ApplicationUser.Get(u => u.Id == userLoginId);
            topicVM.applicationUser = applicationuser;
            List<string> exsitingUserRoles = await _userManager.GetRolesAsync(applicationuser) as List<string>;


            Handel handel = new Handel(_unitOfWork);
            if (ModelState.IsValid)
            {

               // var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //add new topic
                if (topicVM.topic.TopicId == Guid.Empty)
                {

                    topicVM.topic.CreationDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    topicVM.topic.ApplicationUserId = userLoginId;
                    topicVM.topic.stateId = 1;
                    _unitOfWork.Topics.Add(topicVM.topic);

                    var Shistory = new StateHistory();
                    Shistory.State = handel.StateName(1);
                    Shistory.TopicId = topicVM.topic.TopicId;
                    Shistory.ApplicationUserId = userLoginId;
                    Shistory.StateSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    _unitOfWork.StateHistory.Add(Shistory);


                }
                else //Eidt new topic

                {
                    //var newState = topicVM.topic.stateId;
                    var oldTopic = _unitOfWork.Topics.Get(u => u.TopicId == topicVM.topic.TopicId);
                   
                    
                    bool transition = handel.StateTransition(oldTopic.stateId, topicVM.topic.stateId, exsitingUserRoles);
                    if (!transition)
                    {
                        TempData[SD.Error] = culture.Name == "ar" ? "عفوا ليس لديك الصلاحية المناسبة" : "Sorry you havn't permission";
                        return RedirectToAction("Upsert", new { id = topicVM.topic.TopicId });
                    }


                    if (oldTopic.stateId != topicVM.topic.stateId && transition)
                    {
                        //topicVM.topic = oldTopic;
                        oldTopic.stateId = topicVM.topic.stateId;
                        //var test = oldTopic;
                        _unitOfWork.Topics.Update(oldTopic);

                        var Shistory = new StateHistory();
                        Shistory.State = handel.StateName(topicVM.topic.stateId);
                        Shistory.TopicId = topicVM.topic.TopicId;
                        Shistory.ApplicationUserId = userLoginId;
                        Shistory.StateSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                        _unitOfWork.StateHistory.Add(Shistory);
                    }
                    else
                    {
                        if (topicVM.topic.Titel != null)
                            topicVM.topic.stateId = oldTopic.stateId;
                        topicVM.topic.CreationDate = oldTopic.CreationDate;
                        topicVM.topic.ApplicationUserId = oldTopic.ApplicationUserId;
                        topicVM.topic.MainFile = oldTopic.MainFile; 
                            _unitOfWork.Topics.Update(topicVM.topic);
                    }
                }

                _unitOfWork.Save();
                return RedirectToAction("Upsert", new { id = topicVM.topic.TopicId });

            }
            else
            {
                return View(topicVM);
            }

        }

        //__________________download______________________

        public async Task<IActionResult> DownloadFileAsync(Guid? id)
        {
            
            var fileName = _unitOfWork.Attachments.Get(u => u.FileId == id);
            
            List<string> storageData = new List<string>();
            foreach (var includeProp in fileName.FilePath
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


        //______________________________Attachments_________________

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Attachment(Guid? id, TopicVM topicVM)
        {
            //Handel handel = new Handel(_unitOfWork);
            topicVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == id, includeProperties: "ApplicationUser,TopicsStates");
            var ContainerName = _unitOfWork.SubCategory.Get(u => u.SubCategoryId == topicVM.topic.SubCategoryID).SubEnglishName;

            if (ModelState.IsValid)
            {
                var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (topicVM.topic != null && topicVM.attachments != null)
                {

                    if (files.Count > 0)
                    {

                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension = Path.GetExtension(files[0].FileName);


                        if (topicVM.attachments.FileType != extension)
                        {
                            TempData[SD.Error] = "Please Add" + topicVM.attachments.FileType + "file";
                            return RedirectToAction("Upsert", new { id = topicVM.topic.TopicId });
                        }
                        TrustedB.Models.Blob blob = new()
                        {
                            Title = topicVM.attachments.FileName,
                            Comment = null,
                            Uri = topicVM.attachments.Uri
                        };

                        var fullFileName = fileName + extension;
                        var result = await _blobService.UploadBlob(fullFileName, files[0], ContainerName, blob);
                        var containerPath = await _blobService.GetBlob(fullFileName, ContainerName);

                        topicVM.attachments.FilePath = containerPath;
                        
                    }

                    topicVM.attachments.AttachmentSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    topicVM.attachments.ApplicationUserId = userLoginId;
                    topicVM.attachments.TopicId = topicVM.topic.TopicId;
                    topicVM.attachments.stateId = topicVM.topic.stateId;
                    _unitOfWork.Attachments.Add(topicVM.attachments);

                    if (topicVM.attachments.MainFile == "true")
                    {
                        topicVM.topic.MainFile = topicVM.attachments.FilePath;
                        _unitOfWork.Topics.Update(topicVM.topic);
                    }
                    _unitOfWork.Save();


                }
            }

            return RedirectToAction("Upsert", new { id = topicVM.topic.TopicId });

        }

        //_____________________addComments_______________________

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult AddComments(Guid? id, TopicVM topicVM)
        {
           
            var oldTopic = _unitOfWork.Topics.Get(u => u.TopicId == id);
            var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                var commentHistory = new CommentHistory();
                commentHistory.Comment = topicVM.comments.Comment;
                commentHistory.TopicId = oldTopic.TopicId;
                commentHistory.ApplicationUserId = userLoginId;
                commentHistory.stateId = oldTopic.stateId;
                commentHistory.CommentSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                _unitOfWork.CommentHistory.Add(commentHistory);
                _unitOfWork.Save();
            }
            else
            {
                return View(topicVM);
            }
            return RedirectToAction("Upsert", new { id = oldTopic.TopicId});
        }

        [HttpGet]
        public JsonResult GetSubCategroy(int? categoryID)
        {
            var subCategroyList = _unitOfWork.SubCategory.GetAll(filter: o => (o.CategoryID == categoryID)).ToList();
            return Json(subCategroyList);
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}