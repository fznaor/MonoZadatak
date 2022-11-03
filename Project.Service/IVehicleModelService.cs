namespace Project.Service
{
    public interface IVehicleModelService
    {
        Task<bool> AddModel(IVehicleModel model);
        Task<bool> DeleteModel(int id);
        Task<PaginatedList<IVehicleModel>> GetAllModels(SortSettings sortSettings, VehicleModelFilter filter, PaginationSettings paginationSettings);
        Task<IVehicleModel> GetModelById(int id);
        Task<bool> UpdateModel(IVehicleModel make);
    }
}