using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    [Table("VehicleMakes")]
    public class VehicleMake
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleMakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public ICollection<VehicleModel> Models { get; set; }
    }
}
