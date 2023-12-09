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

        private readonly UserManager<IdentityUser> _userManager;

       private readonly IWebHostEnvironment _hostEnvironment;
        public string fileName = "";
        public string slash = @"\";

        //public var topicList;
        public TopicController(ILogger<TopicController> logger, IWebHostEnvironment hostEnvironment, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var topicList = new List<Topics>();
           
             
            topicList = _unitOfWork.Topics.GetAll().ToList();
           

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
                topicVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == id);
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
                
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                //add new topic
                if (topicVM.topic.TopicId == Guid.Empty)
                {
                   
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension = Path.GetExtension(files[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        topicVM.topic.TopicFile = @"\Files\Topics\" + fileName + extension;

                    }
                         topicVM.topic.CreationDate = DateTime.UtcNow.AddMinutes(180).ToString(); 
                         _unitOfWork.Topics.Add(topicVM.topic);
                         var Shistory = new StateHistory();
                         Shistory.State = topicVM.topic.State;
                         Shistory.TopicId = topicVM.topic.TopicId;
                         Shistory.ApplicationUserId = userLoginId;
                         Shistory.StateSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                         _unitOfWork.StateHistory.Add(Shistory);
                   
                }
                else //Eidt new topic

                {
                    var Tpath = _unitOfWork.Topics.Get(u => u.TopicId == topicVM.topic.TopicId);
                    if (files.Count > 0)
                    {

                        //var Tid = _unitOfWork.Topics.Get(u => u.TopicId == topicVM.topic.TopicId);

                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, Tpath.TopicFile.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        topicVM.topic.TopicFile = @"Files\Topics\" + fileName + extension_new;
                    }
                    else
                    {
                        topicVM.topic.TopicFile = Tpath.TopicFile;
                    }

                    topicVM.topic.CreationDate = Tpath.CreationDate;

                    _unitOfWork.Topics.Update(topicVM.topic);

                    var Shistory = new StateHistory();
                    Shistory.State = topicVM.topic.State;
                    Shistory.TopicId = topicVM.topic.TopicId;
                    //userID
                    Shistory.ApplicationUserId = userLoginId;
                    Shistory.StateSetDate = DateTime.UtcNow.AddMinutes(180).ToString();
                    _unitOfWork.StateHistory.Add(Shistory);
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");

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
            var fileName = _unitOfWork.Topics.Get(u => u.TopicId == id);
            //var slash = '\';
            //Build the File Path.
            string path = Path.Combine(webRootPath)  + slash + fileName.TopicFile;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName.TopicFile);
        }


        public IActionResult Index1()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}