using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
            PaginatedList<IVehicleMake> makes = await service.GetAllMakes(new SortSettings("Name", true), "", new PaginationSettings(1, 5));
            Console.WriteLine("Page {0} of {1}", makes.PageIndex, makes.TotalPages);
            foreach (IVehicleMake m in makes)
            {
                Console.WriteLine(m.Name);
            }
            Console.WriteLine();

            PaginatedList<IVehicleModel> models = await service.GetAllModels(new SortSettings("Name", true), "", new PaginationSettings(1, 25));
            Console.WriteLine("Page {0} of {1}", models.PageIndex, models.TotalPages);
            foreach (IVehicleModel m in models)
            {
                Console.WriteLine(m.Name);
            }
            Console.WriteLine();

            PaginatedList<IVehicleModel> modelsVW = await service.GetAllModelsByMake(1, new SortSettings("Name", true), "u", new PaginationSettings(1, 5));
            Console.WriteLine("Page {0} of {1}", modelsVW.PageIndex, modelsVW.TotalPages);
            foreach (IVehicleModel m in modelsVW)
            {
                Console.WriteLine(m.Name);
            }

            await service.AddMake(new VehicleMake { Name = "Maserati", Abbreviation = "MAS" });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}