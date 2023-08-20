using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        void Add(AppUser appUser);
        void Update(AppUser appUser);
        void Delete(AppUser appUser);

        string UploadFile(IFormFile formFile);

    }

}