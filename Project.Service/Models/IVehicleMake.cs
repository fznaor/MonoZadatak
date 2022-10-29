using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleMake
    {
        int VehicleMakeId { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
    }
}
