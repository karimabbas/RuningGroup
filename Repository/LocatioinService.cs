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
    public class LocatioinService : ILocationService
    {
        private readonly MyDBContext _myDBContext;
        public LocatioinService(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }
        public City GetCityByZipCode(int zipcode)
        {
            return  _myDBContext.Cities.FirstOrDefault(x => x.Zip == zipcode);
        }

        public async Task<List<City>> GetLocationSearch(string location)
        {
            List<City> result;
            if (location.Length > 0 && char.IsDigit(location[0]))
            {
                return await _myDBContext.Cities.Where(x => x.Zip.ToString().StartsWith(location)).Take(4).Distinct().ToListAsync();
            }
            else if (location.Length > 0)
            {
                result = await _myDBContext.Cities.Where(x => x.CityName == location).Take(10).ToListAsync();
            }
            result = await _myDBContext.Cities.Where(x => x.StateCode == location).Take(10).ToListAsync();

            return result;
        }
    }
}