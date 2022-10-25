using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleContext
    {
        DbSet<VehicleMake> Makes { get; set; }
        DbSet<VehicleModel> Models { get; set; }
    }
}
