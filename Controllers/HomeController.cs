using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocilaMediaProject.Data;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;
using SocilaMediaProject.ViewModels;

namespace SocilaMediaProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILocationService _locationService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IClubRepository _clubRepository;


    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILocationService locationService)
    {
        _logger = logger;
        _userManager = userManager;
        _locationService = locationService;
        _signInManager = signInManager;
        _clubRepository = clubRepository;
    }

    public IActionResult Register()
    {
        var response = new HomeUserCreateViewModel();
        return View(response);
    }

    public async Task<IActionResult> Index()
    {
        var ipInfo = new IPInfo();
        var homeViewModel = new HomeViewModel();
        try
        {
            string url = "https://ipinfo.io?token=63d5ada815b74c";
            var info = new WebClient().DownloadString(url);
            ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
            RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRI1.EnglishName;
            homeViewModel.City = ipInfo.City;
            homeViewModel.State = ipInfo.Region;
            if (homeViewModel.City != null)
            {
                homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
            }
            return View(homeViewModel);
        }
        catch (Exception)
        {
            homeViewModel.Clubs = null;
        }

        return View(homeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(HomeViewModel homeViewModel)
    {
        var createuser = homeViewModel.Register;

        if (!ModelState.IsValid) return View(homeViewModel);

        var user = await _userManager.FindByEmailAsync(createuser.Email);
        if (user != null)
        {
            ModelState.AddModelError("", "This email address is already in use");
            return View(homeViewModel);
        }
        var userlocation = _locationService.GetCityByZipCode(createuser.ZipCode ?? 0);

        if (userlocation == null)
        {
            ModelState.AddModelError("Register.ZipCode", "Could not find zip code!");
            return View(homeViewModel);
        }

        var newUser = new AppUser
        {
            UserName = createuser.UserName,
            Email = createuser.Email,
            Address = new Address()
            {
                State = userlocation.StateCode,
                City = userlocation.CityName,
                ZipCode = createuser.ZipCode ?? 0,
            }
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, createuser.Password);

        if (newUserResponse.Succeeded)
        {
            await _signInManager.SignInAsync(newUser, isPersistent: false);
            await _userManager.AddToRoleAsync(newUser, UserRole.User);
        }
        return RedirectToAction("Index", "Club");

    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
