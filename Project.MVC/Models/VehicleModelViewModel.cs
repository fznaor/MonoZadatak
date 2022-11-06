using Project.Service;

namespace Project.MVC.Models
{
    public class VehicleModelViewModel
    {
        public int VehicleModelId { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public VehicleMakeEntity? Make { get; set; }

        public IEnumerable<IVehicleMake>? Makes { get; set; }
    }
}
