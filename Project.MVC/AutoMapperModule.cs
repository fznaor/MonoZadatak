using Autofac;
using AutoMapper;
using Project.MVC.Models;
using Project.Service;
using Project.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MVC
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
                cfg.CreateMap<VehicleMakeViewModel, IVehicleMake>().As<VehicleMake>();
                cfg.CreateMap<IVehicleMake, VehicleMakeViewModel>();
                cfg.CreateMap<VehicleMakeViewModel, VehicleMake>();
                cfg.CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListConverter<,>));

            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);


            }).As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(PaginatedListConverter<,>)).AsSelf();
        }
    }
}
