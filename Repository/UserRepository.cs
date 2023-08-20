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
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext _myContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserRepository(MyDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _myContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public void Add(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _myContext.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _myContext.Users.FindAsync(id);
        }

        public void Update(AppUser appUser)
        {
            _myContext.Users.Update(appUser);
            _myContext.SaveChanges();
        }

        public string UploadFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "UsersAttachment");
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