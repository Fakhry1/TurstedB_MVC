using Microsoft.AspNetCore.Localization;
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

        public IActionResult Index()
        {
            return View();
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
