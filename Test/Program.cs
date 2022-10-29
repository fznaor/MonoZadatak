using AutoMapper;
using Ninject;
using Ninject.Modules;
using Project.Service;
using System.Reflection;

class Test
{

    static async Task Main(String[] args)
    {
        var kernel = new StandardKernel(new INinjectModule[] { new DIModule(), new AutoMapperModule() });
        var service = kernel.Get<IVehicleService>();

        List<IVehicleMake> makes = await service.GetAllMakes();
        foreach(IVehicleMake m in makes)
        {
            Console.WriteLine(m.Name);
        }

        Console.WriteLine();
        List<IVehicleModel> makesVW = await service.GetAllModelsByMake(1);
        foreach(IVehicleModel m in makesVW)
        {
            Console.WriteLine(m.Name);
        }

        await service.AddMake(new VehicleMake { Name = "Lamborghini", Abbreviation = "LAM" });
        await service.AddModel(new VehicleModel { MakeId = 10, Name = "Gallardo", Abbreviation = "GAL" });
    }
}