namespace Project.Service
{
    public interface IVehicleService
    {
        Task<bool> AddMake(IVehicleMake make);
        Task<bool> AddModel(IVehicleModel model);
        Task<bool> DeleteMake(int id);
        Task<bool> DeleteModel(int id);
        Task<List<IVehicleMake>> GetAllMakes();
        Task<List<IVehicleModel>> GetAllModels();
        Task<List<IVehicleModel>> GetAllModelsByMake(int makeId);
        Task<bool> UpdateMake(IVehicleMake make);
        Task<bool> UpdateModel(IVehicleModel make);
    }
}