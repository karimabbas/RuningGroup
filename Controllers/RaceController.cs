using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocilaMediaProject.Data;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;
using SocilaMediaProject.ViewModels;

namespace SocilaMediaProject.Controllers
{
    [Authorize]
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public IActionResult Index()
        {
            var races = _raceRepository.GetAll();

            return View(races);
        }

        public async Task<IActionResult> RaceDetails(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);

            return View(race);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var RaceVm = new RaceViewModel()
            {
                AppUserID = userId
            };
            return View(RaceVm);
        }

        [HttpPost]
        public IActionResult Create(RaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = _raceRepository.UploadFile(raceVM.Formfile);

                var race = new Race
                {
                    AppUserId = raceVM.AppUserID,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    RaceCategory = raceVM.RaceCategory,
                    Image = result.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address?.Street,
                        City = raceVM.Address?.City,
                        State = raceVM.Address?.State
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            ViewBag.RaceImage = race.Image;

            if (race == null) return View("Error");

            var racevm = new RaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                RaceCategory = race.RaceCategory,
                Address = race.Address,
                Image = race.Image
            };
            return View(racevm);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, RaceViewModel racevm)
        {

            if (!ModelState.IsValid)
            {
                return View(racevm);
            }

            var EditedRace = await _raceRepository.GetByIdAsync(id);

            if (EditedRace == null)
            {
                return RedirectToAction("Error");
            }
            else
            {
                if (!string.IsNullOrEmpty(racevm.Formfile?.FileName))
                {
                    var result = _raceRepository.UploadFile(racevm.Formfile);
                    EditedRace.Image = result.ToString();
                }

                EditedRace.Title = racevm.Title;
                EditedRace.Description = racevm.Description;
                EditedRace.RaceCategory = racevm.RaceCategory;
                EditedRace.Address.City = racevm.Address?.City;
                EditedRace.Address.State = racevm.Address?.State;
                EditedRace.Address.Street = racevm.Address?.Street;

            }
            _raceRepository.Update(EditedRace);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var club = await _raceRepository.GetByIdAsync(id);
            _raceRepository.Delete(club);
            return RedirectToAction("Index");

        }


        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}