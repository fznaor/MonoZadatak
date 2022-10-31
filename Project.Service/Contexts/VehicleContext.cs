using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleContext : DbContext, IVehicleContext
    {
        public DbSet<VehicleMakeEntity> Makes { get; set; }
        public DbSet<VehicleModelEntity> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=vehicles;Username=postgres;Password=password");

        async Task<int> IVehicleContext.SaveChangesAsync()
        {
            return await SaveChangesAsync();
        }
        
    }
}
