using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.Diagnostics;
using System.Security.Claims;
using TrustedB.DataAccess.Repository;
using TrustedB.Models;
using TrustedB.Models.ViewModels;
using TurstedB.DataAccess.Repository.IRepository;
using TurstedBWeb.Models;

namespace TrustedBWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
  
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
           
             
            topicList = _unitOfWork.Topics.GetAll(includeProperties: "ApplicationUser,TopicsStates").ToList();
           

            return View(topicList);
        }



        public IActionResult Upsert(Guid? id)
        {
            TopicVM topicVM = new()
            {
                topic=new Topics()
            };
         
            if (id == null)
            {
                //new 
                return View(topicVM);
            }else
            {
                //eidt
                topicVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == id, includeProperties: "ApplicationUser,TopicsStates");
                topicVM.StateHistoryList = _unitOfWork.StateHistory.GetAll(filter: o => (o.TopicId == topicVM.topic.TopicId), includeProperties: "ApplicationUser").ToList();
                
                return View(topicVM);
            }
           
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Upsert(TopicVM topicVM)
        {
          
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
                    Shistory.State = "قيد اعداد";
                    Shistory.TopicId = topicVM.topic.TopicId;
                    Shistory.ApplicationUserId = userLoginId;
                    Shistory.StateSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    _unitOfWork.StateHistory.Add(Shistory);

                    
                }
                else //Eidt new topic

                {
                    var oldTopic = _unitOfWork.Topics.Get(u => u.TopicId == topicVM.topic.TopicId);


                    topicVM.topic.CreationDate = oldTopic.CreationDate;

                    _unitOfWork.Topics.Update(topicVM.topic);
                    if (oldTopic.stateId != topicVM.topic.stateId) { 
                    var Shistory = new StateHistory();
                    Shistory.State = topicVM.topic.TopicsStates.ArabicName;
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

        //public IActionResult DownloadFile(Guid? id)
        //{
        //    string webRootPath = _hostEnvironment.WebRootPath;
        //    var fileName = _unitOfWork.Topics.Get(u => u.TopicId == id);

        //    string path = Path.Combine(webRootPath)  + slash + fileName.TopicFile;

        //    //Read the File data into Byte Array.
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    //Send the File to Download.
        //    return File(bytes, "application/octet-stream", fileName.TopicFile);
        //}


        public IActionResult Index1()
        {

            return View();
        }

        //______________________________Attachments_________________

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Attachment(Guid? id, TopicVM topicVM)
        {
           
            if (ModelState.IsValid)
            {
                var userLoginId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                //add new Request
                if (topicVM.topic != null && topicVM.attachments !=null)
                {

                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension = Path.GetExtension(files[0].FileName);
                        var attchmentPath = Path.Combine(uploads, fileName + extension);

                        using (var fileStreams = new FileStream(attchmentPath, FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                            fileStreams.Close();
                            var data = System.IO.File.ReadAllBytes(attchmentPath);
                           
                        }


                        topicVM.attachments.FilePath = @"\Files\Attachments\" + fileName + extension;

                    }
                    
                    topicVM.attachments.AttachmentSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    topicVM.attachments.ApplicationUserId = userLoginId;
                    topicVM.attachments.TopicId = topicVM.topic.TopicId;
                    _unitOfWork.Attachments.Add(topicVM.attachments);
                    _unitOfWork.Save();


                }
            }

            return RedirectToAction("Upsert", new { id = topicVM.topic.TopicId });

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}