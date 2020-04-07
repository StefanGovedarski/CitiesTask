using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesMap.DTOs;
using CitiesMap.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesMap.Controllers
{
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private ICitiesService _service;

        public CitiesController( ICitiesService service)
        {
            _service = service;
        }

        // POST: Cities/PostCity
        [Route("PostCity")]
        [HttpPost]
        public void PostCity([FromBody] string value)
        {
            _service.SaveCity(value);
        }

        // POST: Cities/PostRoad
        [HttpPost]
        [Route("PostRoad")]
        public void PostRoad([FromBody] Road road)
        {
            _service.SaveRoad(road.Distance, road.CityFrom, road.CityTo);
        }

        [HttpPut]
        [Route("PutCity")]
        public void PutCity([FromBody] City city)
        {
            _service.EditCity(city.Id, city.Name);
        }

        
        [HttpPut]
        [Route("PutRoad")]
        public void PutRoad([FromBody] Road road)
        {
            _service.EditRoad(road.Distance, road.CityFrom, road.CityTo);
        }

        [Route("LogisticCenter")]
        [HttpGet]
        public LogisticCenter LogisticCenter()
        {
           return _service.LogisticCenter();  
        }

        [Route("GetCities")]
        [HttpGet]
        public IEnumerable<City> GetCities()
        {
            return _service.GetCities();
        }

        [Route("GetRoads")]
        [HttpGet]
        public IEnumerable<Road> GetRoads()
        {
            return _service.GetRoads();
        }

        [Route("GetCenter")]
        [HttpGet]
        public LogisticCenter GetLogisticCenter()
        {
            return _service.GetLogisticCenter();
        }
    }
}
