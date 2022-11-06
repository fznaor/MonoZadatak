using Project.Service;

namespace Project.MVC.Models
{
    public class VehicleModelIndexViewModel
    {
        public PaginatedList<IVehicleModel>? Models { get; set; }
        public int SelectedMakeId { get; set; }
        public List<IVehicleMake>? Makes { get; set; }

        public VehicleModelIndexViewModel(PaginatedList<IVehicleModel>? models, List<IVehicleMake>? makes)
        {
            Models = models;
            SelectedMakeId = 0;
            Makes = makes;
        }
    }
}
