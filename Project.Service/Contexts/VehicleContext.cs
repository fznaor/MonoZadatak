using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleContext : DbContext, IVehicleContext
    {
        public DbSet<VehicleMake> Makes { get; set; }
        public DbSet<VehicleModel> Models { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=vehicles;Username=postgres;Password=password");
    }
}
