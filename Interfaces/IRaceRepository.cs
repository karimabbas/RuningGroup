using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Interfaces
{
    public interface IRaceRepository
    {
        IEnumerable<Race> GetAll();

        Task<Race> GetByIdAsync(int id);

        Task<IEnumerable<Race>> GetAllByCity(string city);

        string UploadFile(IFormFile formFile);

        void Add(Race race);

        void Update(Race race);

        void Delete(Race race);
    }
}