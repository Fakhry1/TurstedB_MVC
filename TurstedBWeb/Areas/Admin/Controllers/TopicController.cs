using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.Diagnostics;
using System.Security.Claims;
using TrustedB.DataAccess.Repository;
using TrustedB.Models;
using TrustedB.Models.ViewModels;
using TrustedB.Utility;
using TrustedBWeb.Logic;
using TurstedB.DataAccess.Repository.IRepository;
using TurstedBWeb.Models;

namespace TrustedBWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ILogger<TopicController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _hostEnvironment;
        public string fileName = "";
        public string slash = @"\";

        //public var topicList;
        public TopicController(ILogger<TopicController> logger, IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var topicList = new List<Topics>();


            topicList = _unitOfWork.Topics.GetAll(includeProperties: "ApplicationUser,TopicsStates,Category").ToList();


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
        public IActionResult Upsert(TopicVM topicVM)
        {
            Handel handel = new Handel(_unitOfWork);
            if (ModelState.IsValid)
            {

                var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
                   
                    
                    bool transition = handel.StateTransition(oldTopic.stateId, topicVM.topic.stateId);

                 

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

        public IActionResult DownloadFile(Guid? id)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var fileName = _unitOfWork.Attachments.Get(u => u.FileId == id);

            string path = Path.Combine(webRootPath) + slash + fileName.FilePath;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName.FilePath);
        }


        //______________________________Attachments_________________

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Attachment(Guid? id, TopicVM topicVM)
        {
            //if (id == null)
            //{
            //    TempData[SD.Error] = "Please Add Topic firstly";
            //    return RedirectToAction(nameof(Upsert));
            //}
            topicVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == id,includeProperties: "ApplicationUser,TopicsStates");

            if (ModelState.IsValid)
            {
                var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                //add new Request
                if (topicVM.topic != null && topicVM.attachments != null)
                {

                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension = Path.GetExtension(files[0].FileName);

                        if (topicVM.attachments.FileType != extension)
                        {
                            TempData[SD.Error] = "Please Add"+ topicVM.attachments.FileType + "file";
                              return RedirectToAction("Upsert", new { id = topicVM.topic.TopicId });
                        }

                            var attchmentPath = Path.Combine(uploads, fileName + extension);

                        using (var fileStreams = new FileStream(attchmentPath, FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                            fileStreams.Close();
                            var data = System.IO.File.ReadAllBytes(attchmentPath);

                        }


                        topicVM.attachments.FilePath = @"\Files\Topics\" + fileName + extension;

                    }

                    topicVM.attachments.AttachmentSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    topicVM.attachments.ApplicationUserId = userLoginId;
                    topicVM.attachments.TopicId = topicVM.topic.TopicId;
                    topicVM.attachments.stateId = topicVM.topic.stateId;
                    _unitOfWork.Attachments.Add(topicVM.attachments);

                    if (topicVM.attachments.MainFile =="true") 
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