using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesMap.DTOs
{
    public class Road
    {
        public int Id { get; set; }

        public int Distance { get; set; }

        public string CityFrom { get; set; }

        public string CityTo { get; set; }
    }
}
