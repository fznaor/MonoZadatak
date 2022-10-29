namespace Project.Service
{
    public interface IVehicleService
    {
        Task<int> AddMake(IVehicleMake make);
        Task<int> AddModel(IVehicleModel model);
        Task<List<IVehicleMake>> GetAllMakes();
        Task<List<IVehicleModel>> GetAllModels();
        Task<List<IVehicleModel>> GetAllModelsByMake(int makeId);
    }
}