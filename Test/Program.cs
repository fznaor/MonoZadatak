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
        var makeService = kernel.Get<IVehicleMakeService>();
        var modelService = kernel.Get<IVehicleModelService>();

        try
        {
            PaginatedList<IVehicleMake> makes = await makeService.GetAllMakes(new SortSettings("Name", true), "", new PaginationSettings(1, 5));
            Console.WriteLine("Page {0} of {1}", makes.PageIndex, makes.TotalPages);
            foreach (IVehicleMake m in makes)
            {
                Console.WriteLine(m.Name);
            }
            Console.WriteLine();

            PaginatedList<IVehicleModel> models = await modelService.GetAllModels(new SortSettings("Name", true), new VehicleModelFilter("", ""), new PaginationSettings(1, 5));
            Console.WriteLine("Page {0} of {1}", models.PageIndex, models.TotalPages);
            foreach (IVehicleModel m in models)
            {
                Console.WriteLine(m.Name);
            }
            Console.WriteLine();

            PaginatedList<IVehicleModel> modelsVW = await modelService.GetAllModels(new SortSettings("Name", true), new VehicleModelFilter("volkswagen", ""), new PaginationSettings(1, 5));
            Console.WriteLine("Page {0} of {1}", modelsVW.PageIndex, modelsVW.TotalPages);
            foreach (IVehicleModel m in modelsVW)
            {
                Console.WriteLine(m.Name + " " + m.Make.Name);
            }

            await makeService.AddMake(new VehicleMake { Name = "Maserati", Abbreviation = "MAS" });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}