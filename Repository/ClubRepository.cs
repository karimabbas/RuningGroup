using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocilaMediaProject.Data;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public ClubRepository(MyDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public void Add(Club club)
        {
            _context.Add(club);
            _context.SaveChanges();
        }

        public void Delete(Club club)
        {
            if (club.Image != null)
            {
                string file = Path.Combine(_environment.WebRootPath, "ClubAttachment", club.Image);
                File.Delete(file);
            }
            _context.Clubs.Remove(club);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.Include(c => c.Address).ToListAsync();

        }

        public async Task<IEnumerable<Club>> GetAllByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).Distinct().ToListAsync();

        }

        public void Update(Club club)
        {

            _context.Clubs.Update(club);
            _context.SaveChanges();

        }

        public string UploadFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "ClubAttachment");
                var filename = Path.GetFileName(formFile.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var filestream = new FileStream(filePath, FileMode.Create);
                formFile.CopyTo(filestream);
                return filename;
            }
            else
            {
                return " ";
            }
        }

    }
}