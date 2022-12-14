using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    [Table("VehicleModels")]
    [Index(nameof(MakeId), nameof(Name), IsUnique = true)]
    public class VehicleModelEntity
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleModelId { get; set; }

        [ForeignKey("Make")]
        public int MakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public VehicleMakeEntity Make { get; set; }
    }
}
