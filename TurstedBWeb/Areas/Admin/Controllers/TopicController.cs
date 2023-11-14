using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using TrustedB.Models;
using TurstedB.DataAccess.Repository.IRepository;
using TurstedBWeb.Models;

namespace TrustedBWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicController : Controller
    {
        private readonly ILogger<TopicController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnvironment;
        public string fileName = "";

        public List<Topics> topicList;
        public TopicController(ILogger<TopicController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (topicList == null)
            {
                topicList = new List<Topics>(); 
            } 
            else
            { 

            topicList = _unitOfWork.Topics.GetAll().ToList();
           } 
            return View(topicList);
        }



        public IActionResult Upsert(Guid? id)
        {
           Topics topic = new Topics();
            if (id == null)
            {
                //new 
                return View(topic);
            }else
            {
                //eidt
                topic = _unitOfWork.Topics.Get(u => u.TopicId == id);
                return View(topic);
            }
           
        }


        public IActionResult Upsert(Topics topic)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                //add new topic
                if (topic.TopicId == Guid.Empty)
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
                        topic.TopicFile = @"Files\Topics\" + fileName + extension;

                    }

                    _unitOfWork.Topics.Add(topic);
                }
                else //Eidt new topic

                {
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, topic.TopicFile.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        topic.TopicFile = @"Files\Topics\" + fileName + extension_new;
                    }

                    _unitOfWork.Topics.Update(topic);
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");

            }
            else
            {
                return View(topic);
            }

         }



            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}