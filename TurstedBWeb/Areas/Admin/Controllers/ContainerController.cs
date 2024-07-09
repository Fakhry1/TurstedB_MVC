using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrustedB.DataAccess.Data;
using TrustedB.Models;
using TrustedB.Models.ViewModels;
using TrustedB.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using TrustedBWeb.Logic;
using Container = TrustedB.Models.Container;

namespace TrustedB.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ContainerController : Controller
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        public async Task<IActionResult> Index()
        {
            var allContainer = await _containerService.GetAllContainer();
            return View(allContainer);
        }

        public async Task<IActionResult> Delete(string containerName)
        {
            await _containerService.DeleteContainer(containerName);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View(new Container());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Container container)
        {
            await _containerService.CreateContainer(container.Name);
            return RedirectToAction(nameof(Index));
        }
    }

}
