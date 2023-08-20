using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Interfaces
{
    public interface ILocationService
    {
        Task<List<City>> GetLocationSearch(string location);

       City GetCityByZipCode(int zipcode);
    }

}