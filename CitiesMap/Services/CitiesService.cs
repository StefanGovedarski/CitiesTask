using CitiesMap.Data;
using CitiesMap.DTOs;
using CitiesMap.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesMap.Services
{
    public class CitiesService : ICitiesService
    {
        private CitiesDbContext _context;

        public CitiesService(CitiesDbContext context)
        {
            _context = context;
        }

        public void EditCity(int id, string name)
        {
            var city = _context.Cities.FirstOrDefault(x => x.Id == id);

            if (city != null)
            {
                city.Name = name;

                _context.Cities.Update(city);
                _context.SaveChanges();
            }
        }

        public void EditRoad(int distance, string cityFrom, string cityTo)
        {
            var road = _context.Roads.FirstOrDefault(x => x.From.Name ==cityFrom && x.To.Name == cityTo);

            if (road != null)
            {
                road.Distance = distance;
                road.UpdateDate = DateTime.Now;

                _context.Roads.Update(road);
                _context.SaveChanges();
            }
        }

        public List<City> GetCities()
        {
            var cities = new List<City>();

            foreach (var cityEnt in _context.Cities)
            {
                var city = new City()
                {
                    Id = cityEnt.Id,
                    Name = cityEnt.Name
                };

                cities.Add(city);
            }

            return cities;
        }

        public LogisticCenter LogisticCenter()
        {
            var logisticCetner = _context.LogisticCenter.FirstOrDefault();
            var newCenter = new LogisticCenter();

            if (logisticCetner != null)
            {
                // check timestamps on last road update and last logistic center generation so we know if we should do all the logic
                if (_context.Roads.OrderByDescending(x => x.UpdateDate).First().UpdateDate < logisticCetner.UpdatedDate)
                {
                    newCenter = new LogisticCenter { Id = logisticCetner.Id, Name = logisticCetner.Name };
                    return newCenter;
                }
                else
                {
                    newCenter = GenerateLogisticCenter();
                    if (newCenter == null)
                    {
                        // could make custom exception
                        throw new ArgumentException("Something went wrong when generating a logistic center.");
                    }
                    else
                    {
                        logisticCetner.Name = newCenter.Name;
                        _context.LogisticCenter.Update(logisticCetner);
                        _context.SaveChangesAsync();
                    }


                    return newCenter;
                }
            }
            else
            {
                newCenter = GenerateLogisticCenter();
                if (newCenter == null)
                {
                    // could make custom exception
                    throw new ArgumentException("Something went wrong when generating a logistic center.");
                }
                var logisticCenterEntity = new LogisticCenterEntity()
                {
                    Name = newCenter.Name,
                    UpdatedDate = DateTime.Now
                    
                };
                _context.LogisticCenter.Add(logisticCenterEntity);
                _context.SaveChanges();

                return newCenter;
            }
        }

        public void SaveCity(string name)
        {
            var cityEntity = new CityEntity() { Name = name};
            _context.Add(cityEntity);
            _context.SaveChanges();
        }

        public void SaveRoad(int distance, string cityFrom, string cityTo)
        {
            var cityFromEntity = _context.Cities.FirstOrDefault(x => x.Name == cityFrom);
            var cityToEntity = _context.Cities.FirstOrDefault(x => x.Name == cityTo);
            var roadEntity = new RoadEntity() {
                Distance = distance , 
                UpdateDate = DateTime.Now,
                From = cityFromEntity,
                To = cityToEntity
            };

            _context.Roads.Add(roadEntity);
            _context.SaveChanges();
        }

        private LogisticCenter GenerateLogisticCenter()
        {
            var logisticCenterName = new LogisticCenter();
            if(_context.Roads.Any())
            {
                var cities = _context.Cities.Include(x => x.RoadFrom).Include(x => x.To);

                // collection to store how many times a city has been marked as "furthest" from another city in the map
                Dictionary<CityEntity, int> furthestCityIds = new Dictionary<CityEntity, int>();

                //find furthest city for each city we have in db that has roads
                foreach (var city in cities)
                {
                    if(city.RoadFrom.Count == 0 )
                    {
                        continue;
                    }
                    int totalDistance = 0;

                    List<CityEntity> visited = new List<CityEntity>();
                    visited.Add(city);

                    foreach (var road in city.RoadFrom)
                    {
                        if(visited.Contains(road.To) == false)
                        {
                            // using depth first search and keeping track of the distance to the bottom(a.k.a furthest) most city in the search.
                            DFS(road.To, road.Distance, furthestCityIds, totalDistance, visited);
                        }
                    }
                }

                // Find the most common furthest city
                KeyValuePair<CityEntity, int> max = new KeyValuePair<CityEntity, int>(new CityEntity(), 0);
                foreach (var city in  furthestCityIds)
                {
                    if(city.Value > max.Value)
                    {
                        max = city;
                    }
                }

                // we found our furthest city in max. Now lets find the logistic center.
                // this will be the city that has the shortest road to max.
                logisticCenterName.Name = cities.First(x => x.Name == max.Key.Name).To.OrderBy(r => r.Distance).First().From.Name;

            }

            return logisticCenterName;
        }

        private void DFS(CityEntity city, int distance, Dictionary<CityEntity, int> furthestCityIds, int maxDist, List<CityEntity> visited)
        {
            visited.Add(city);

            if (city.RoadFrom.Count == 0 || !city.RoadFrom.Any(r => !visited.Contains(r.To)))
            {
                if (distance > maxDist)
                {
                    maxDist = distance;

                    if (furthestCityIds.ContainsKey(city))
                    {
                        furthestCityIds[city]++;
                    }
                    else
                    {
                        furthestCityIds.Add(city, 1);
                    }
                }
            }
            else
            {
                foreach (var road in city.RoadFrom)
                {
                    if(!visited.Contains(road.To))
                    {
                        distance += road.Distance;
                        DFS(road.To, distance, furthestCityIds, maxDist, visited);
                    }
                }
            }
        }

        public List<Road> GetRoads()
        {
            var roads = new List<Road>();

            foreach (var roadEnty in _context.Roads)
            {
                Road road = new Road()
                {
                    Id = roadEnty.Id,
                    Distance = roadEnty.Distance,
                    CityFrom = roadEnty.From.Name,
                    CityTo = roadEnty.To.Name

                };

                roads.Add(road);
            }


            return roads;
        }

        public LogisticCenter GetLogisticCenter()
        {
            var logCenterName = new LogisticCenter() {Id = 1 , Name = "Not Generated" };

            var center = _context.LogisticCenter.FirstOrDefault();
            if (center != null)
            {
                logCenterName.Name = center.Name;
                logCenterName.Id = center.Id;
            }

            return logCenterName;
        }
    }
}
