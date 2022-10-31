using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public interface IVehicleContext
    {
        DbSet<VehicleMakeEntity> Makes { get; set; }
        DbSet<VehicleModelEntity> Models { get; set; }

        Task<int> SaveChangesAsync();
    }
}
