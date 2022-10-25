using Project.Service;

class Test
{

    static public void Main(String[] args)
    {
        IVehicleContext context = new VehicleContext();

        foreach(VehicleMake make in context.Makes.ToList())
        {
            Console.WriteLine(make.Name);
        }

        // Select all Volkswagens
        List<VehicleModel> models = context.Models
                                    .Where(m => m.MakeId == context.Makes.Where(m => m.Name == "Volkswagen")
                                    .Select(m => m.VehicleMakeId).FirstOrDefault())
                                    .ToList();

        foreach (VehicleModel model in models)
        {
            Console.WriteLine(model.Name);
        }

    }
}