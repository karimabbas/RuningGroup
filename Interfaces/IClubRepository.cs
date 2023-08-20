using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();

        Task<Club> GetByIdAsync(int id);

        Task<IEnumerable<Club>> GetAllByCity(string city);

        string UploadFile(IFormFile formFile);

        void Add(Club club);

        void Update(Club club);

        void Delete(Club club);

        Task<IEnumerable<IdentityRole>> GetAllRoles();

        Task<IEnumerable<Club>> GetClubByCity(string city);


    }
}