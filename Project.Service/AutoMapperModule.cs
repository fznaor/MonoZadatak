using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleMakeEntity, VehicleMake>().ReverseMap();
                cfg.CreateMap<VehicleModelEntity, VehicleModel>().ReverseMap();
                cfg.CreateMap<IVehicleMake, VehicleMake>().ReverseMap();
                cfg.CreateMap<IVehicleModel, VehicleModel>().ReverseMap();

            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);


            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
