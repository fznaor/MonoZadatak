using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleContext>().As<IVehicleContext>();
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
            builder.RegisterType<VehicleModel>().As<IVehicleModel>();
            builder.RegisterType<VehicleMake>().As<IVehicleMake>();
        }
    }
}
