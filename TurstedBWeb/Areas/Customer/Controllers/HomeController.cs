using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using TrustedB.DataAccess.Data;
using TrustedB.Models;
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

        public HomeController( IUnitOfWork unitOfWork, ApplicationDbContext appcontext)
        {
           
            _unitOfWork = unitOfWork;
            _appcontext = appcontext;
          
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Guidance(int pg = 1)
        {
            const int pageSize = 1;
            if (pg < 1) { pg = 1; }

            int recsCount = _unitOfWork.Topics.GetAll().Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            //GetAllPagination
            var data = _unitOfWork.Topics.GetAllPagination(recSkip, pager.PageSize, includeProperties: "ApplicationUser,TopicsStates,Category").ToList();
            this.ViewBag.Pager = pager;

            return View(data);
            
        }
            //Guidance

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
