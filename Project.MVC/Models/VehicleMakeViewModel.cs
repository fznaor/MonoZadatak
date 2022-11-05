namespace Project.MVC.Models
{
    public class VehicleMakeViewModel
    {
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public VehicleMakeViewModel()
        {
            VehicleMakeId = 0;
            Name = String.Empty;
            Abbreviation = String.Empty;
        }
    }
}
