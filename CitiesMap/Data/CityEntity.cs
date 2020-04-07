using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesMap.Data
{
    [Table("City")]
    public class CityEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [InverseProperty("From")]
        public virtual ICollection<RoadEntity> RoadFrom { get; set; }

        [InverseProperty("To")]
        public virtual ICollection<RoadEntity> To { get; set; }
    }
}
