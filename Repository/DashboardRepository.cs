using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SocilaMediaProject.Data;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Repository
{
    public class DashboardRepository : IDashboardRepository
    {

        private readonly MyDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardRepository(MyDBContext myDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = myDBContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUserID = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var userClubs = _context.Clubs.Where(c => c.AppUser.Id == currentUserID);

            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRace()
        {
            var currentUserID = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var usrRaces = _context.Races.Where(r => r.AppUserId == currentUserID);

            return usrRaces.ToList();

        }

        public Task<AppUser> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

    }
}