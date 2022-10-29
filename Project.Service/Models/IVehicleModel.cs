using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleModel
    {
        int VehicleModelId { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
    }
}
