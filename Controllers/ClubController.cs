using System.Text;
using System.Net.Mime;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SocilaMediaProject.Models;
using SocilaMediaProject.Data;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SocilaMediaProject.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;

        }
        [Authorize(Policy = "ClubAccess")]
        public async Task<IActionResult> Index()
        {
            // dynamic mymodel = new ExpandoObject();
            // var clubs = myDBContext.Clubs.Include(c => c.Address).ToList();
            var clubs = await _clubRepository.GetAll();
            // ViewData["clubs"] = clubs;
            return View(clubs);
        }

        public async Task<IActionResult> ClubDetails(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);

            return View(club);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ClubViewModel = new ClubViewModel { AppUserId = userId };

            return View(ClubViewModel);
        }
        [HttpPost]
        public IActionResult Create(ClubViewModel clubVM)
        {

            if (ModelState.IsValid)
            {
                var result = _clubRepository.UploadFile(clubVM.Image);
                var club = new Club
                {
                    AppUserID = clubVM.AppUserId,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    ClubCategory = clubVM.ClubCategory,
                    Image = result.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address?.Street,
                        City = clubVM.Address?.City,
                        State = clubVM.Address?.State,
                    }

                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Image", "Photo upload failed");
            }

            return View(clubVM);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            ViewBag.ClubImage = club.Image;
            if (club == null) return View("Error");

            var clubvm = new ClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                Imagepath = club.Image,
                ClubCategory = club.ClubCategory,
                Address = club.Address
            };
            return View(clubvm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                return View(clubVM);
            }

            var EdtiedClub = await _clubRepository.GetByIdAsync(id);

            if (EdtiedClub == null)
            {
                return View("Error");
            }
            else
            {
                if (!string.IsNullOrEmpty(clubVM.Image?.FileName))
                {
                    var result = _clubRepository.UploadFile(clubVM.Image);
                    EdtiedClub.Image = result.ToString();
                }

                EdtiedClub.Title = clubVM.Title;
                EdtiedClub.Description = clubVM.Description;
                EdtiedClub.ClubCategory = clubVM.ClubCategory;
                EdtiedClub.Address.City = clubVM.Address?.City;
                EdtiedClub.Address.State = clubVM.Address?.State;
                EdtiedClub.Address.Street = clubVM.Address?.Street;
                //this line creat new address with new id 
                // EdtiedClub.Address = clubVM.Address;


            }
            _clubRepository.Update(EdtiedClub);
            return RedirectToAction("Index");
        }


        // [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            _clubRepository.Delete(club);
            return RedirectToAction("Index");

        }

    }
}