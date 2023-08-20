using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Repository;
using SocilaMediaProject.ViewModels;

namespace SocilaMediaProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardRepository _dashboardRepository;


        public DashboardController(ILogger<DashboardController> logger, IDashboardRepository dashboardRepository)
        {
            _logger = logger;
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Userclubs = await _dashboardRepository.GetAllUserClubs();
            var UserRaces = await _dashboardRepository.GetAllUserRace();

            DashboardViewModel DashVM = new()
            {
                Clubs = Userclubs,
                Races = UserRaces
            };
            return View(DashVM);
        }












        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}