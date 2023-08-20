using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Club>> GetAllUserClubs();
        Task<List<Race>> GetAllUserRace();

        Task<AppUser> GetUserById(string id);
    }
}