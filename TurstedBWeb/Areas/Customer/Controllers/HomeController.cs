using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using TrustedB.DataAccess.Data;
using TrustedB.Models;
using TrustedB.Models.ViewModels;
using TrustedBWeb.Areas.Admin.Controllers;
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
        public string fileName = "";
        public string slash = @"\";

        public HomeController( IUnitOfWork unitOfWork, ApplicationDbContext appcontext, IWebHostEnvironment hostEnvironment)
        {
           
            _unitOfWork = unitOfWork;
            _appcontext = appcontext;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        //Guidance
        public IActionResult Guidance(Guid? Id,int pg = 1 )
        {
            RequestDetailsVM requestDetailsVM = new RequestDetailsVM();
            
            const int pageSize = 3;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 1)).Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

             //GetAllPagination
            requestDetailsVM.TopicList = _unitOfWork.Topics.GetAllPagination(recSkip, pager.PageSize, filter: o => (o.CategoryID == 1), includeProperties: "ApplicationUser,TopicsStates,Category").ToList();
          
            this.ViewBag.Pager = pager;

            if ( Id !=null )
            {
                requestDetailsVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == Id, includeProperties: "Category");
                requestDetailsVM.AttachmentsList = _unitOfWork.Attachments.GetAll(filter: o => (o.TopicId == Id)).ToList();
                ViewBag.FileList = requestDetailsVM.AttachmentsList;
            }

            return View(requestDetailsVM);
            
        }

        public IActionResult GuidanceDetails(Guid? Id)
        {
            RequestDetailsVM requestDetailsVM = new RequestDetailsVM();
            requestDetailsVM.topic = _unitOfWork.Topics.Get(u => u.TopicId == Id, includeProperties: "Category");
            requestDetailsVM.AttachmentsList = _unitOfWork.Attachments.GetAll(filter: o => (o.TopicId == Id)).ToList();
            //return PartialView("_GuidanceDetails", requestDetailsVM.topic);

            //var AttachmentsList = _unitOfWork.Attachments.GetAll(filter: o => (o.TopicId == Id)).ToList();
            return View(requestDetailsVM);
        }


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

        public IActionResult DownloadAudio(string? path)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            //var fileName = _unitOfWork.Attachments.Get(u => u.FileId == id);

            string filePath = Path.Combine(webRootPath) + slash + path;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", "Image.png");
        }

        //______________________________Audio____________________________________

        public IActionResult AllAudio(int pg = 1)
        {
     
            AudioVM audioVM = new AudioVM();

            const int pageSize = 4;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).Count() + _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 5)).Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            audioVM.AudioQList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.CategoryID == 4)).ToList();
            audioVM.AudioLesonList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.CategoryID == 5)).ToList();

            //var TopicList = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).ToList();
            this.ViewBag.Pager = pager;

            return View(audioVM);

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
           
            //var TopicList = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).ToList();
            this.ViewBag.Pager = pager;
          

            return View(ImageList);

        }

        //___________________________________Videos___________________________

        public IActionResult AllVideos(int? SubCategoryID, int pg = 1)
        {

            const int pageSize = 8;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 3)).Count();
            var pager = new Pager((int)SubCategoryID, recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            var ImageList = _unitOfWork.Topics.GetAllPaginationAudio(recSkip, pager.PageSize, filter: o => (o.SubCategoryID == SubCategoryID)&&(o.CategoryID==3)).ToList();

            //var TopicList = _unitOfWork.Topics.GetAll(filter: o => (o.CategoryID == 4)).ToList();
            this.ViewBag.Pager = pager;


            return View(ImageList);

        }



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
