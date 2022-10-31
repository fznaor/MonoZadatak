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

        try
        {
            List<IVehicleMake> makes = await service.GetAllMakes();
            foreach (IVehicleMake m in makes)
            {
                Console.WriteLine(m.Name);
            }

            Console.WriteLine();
            List<IVehicleModel> makesVW = await service.GetAllModelsByMake(1);
            foreach (IVehicleModel m in makesVW)
            {
                Console.WriteLine(m.Name);
            }

            await service.AddMake(new VehicleMake { VehicleMakeId = 1, Name = "Lamborghini", Abbreviation = "LAM" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        // await service.AddModel(new VehicleModel { MakeId = 10, Name = "Gallardo", Abbreviation = "GAL" });

        // await service.UpdateMake(new VehicleMake { VehicleMakeId = 10, Name = "Lambo", Abbreviation = "LBO" });
    }
}