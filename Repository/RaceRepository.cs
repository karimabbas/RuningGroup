using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocilaMediaProject.Data;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public RaceRepository(MyDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public void Add(Race race)
        {
            _context.Add(race);
            _context.SaveChanges();
        }

        public void Delete(Race race)
        {
            if (race.Image != null)
            {
                string file = Path.Combine(_environment.WebRootPath, "RaceAttachment", race.Image);
                File.Delete(file);
            }
            _context.Races.Remove(race);
            _context.SaveChanges();
        }

        public IEnumerable<Race> GetAll()
        {
            return _context.Races.Include(c => c.Address).ToList();

        }

        public async Task<IEnumerable<Race>> GetAllByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();

        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(c => c.Address).FirstOrDefaultAsync(c => c.ID == id);

        }

        public void Update(Race race)
        {
            _context.Races.Update(race);
            _context.SaveChanges();
        }

        public string UploadFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "RaceAttachment");
                var filename = Path.GetFileName(formFile.FileName);
                var filePath = Path.Combine(uploads, filename);
                using var filestream = new FileStream(filePath, FileMode.Create);
                formFile.CopyTo(filestream);
                return filename;
            }
            else
            {
                return "no photo";
            }
        }
    }
}