namespace Project.Service
{
    public class VehicleModel : IVehicleModel
    {
        public int VehicleModelId { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public VehicleMakeEntity Make { get; set; }
    }
}
