using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVehicleContext>().To<VehicleContext>();
            this.Bind<IVehicleService>().To<VehicleService>();
        }
    }
}
