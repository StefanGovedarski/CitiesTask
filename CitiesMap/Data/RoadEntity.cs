using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesMap.Data
{
    [Table("Road")]
    public class RoadEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Distance { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public virtual CityEntity From { get; set; }

        public virtual CityEntity To { get; set; }
    }
}
