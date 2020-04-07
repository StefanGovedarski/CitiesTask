using CitiesMap.Data;
using CitiesMap.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesMap.Services.Abstract
{
    public interface ICitiesService
    {
        List<City> GetCities();

        List<Road> GetRoads();

        LogisticCenter LogisticCenter();

        LogisticCenter GetLogisticCenter();

        void SaveCity(string name);

        void EditCity(int id, string name);

        void SaveRoad(int distance, string cityFrom, string cityTo);

        void EditRoad(int distance, string cityFrom, string cityTo);
    }
}
