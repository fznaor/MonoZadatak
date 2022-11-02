using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();
        }

        private IMapper AutoMapper(Ninject.Activation.IContext context)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VehicleMakeEntity, VehicleMake>().ReverseMap();
                cfg.CreateMap<VehicleModelEntity, VehicleModel>().ReverseMap();
                cfg.CreateMap<IVehicleMake, VehicleMake>().ReverseMap();
                cfg.CreateMap<IVehicleModel, VehicleModel>().ReverseMap();
            });

            config.AssertConfigurationIsValid();

            return new Mapper(config);
        }
    }
}
