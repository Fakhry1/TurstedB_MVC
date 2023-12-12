using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using TrustedB.Models;
using TrustedBWeb.Areas.Admin.Controllers;
using TurstedB.DataAccess.Repository.IRepository;
using TurstedBWeb.Models;

namespace TrustedBWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IWebHostEnvironment _hostEnvironment;
        public string fileName = "";

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadVideo(int? id)
        {
            return View();
        }

            //[DisableRequestSizeLimit]
        [HttpPost]
        public IActionResult UploadVideo()
        {
            
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var filetype = HttpContext.Request.Headers["type"].FirstOrDefault();
                var description = HttpContext.Request.Headers["session"].FirstOrDefault();
                

                if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Files\Topics");
                        var extension = Path.GetExtension(files[0].FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                      var fullpath = @"Files\Topics\" + fileName + extension;

                    }
            

             return Json(new { status = true, message = files[0].FileName });
            //return View();
        }
           






        }
    }
