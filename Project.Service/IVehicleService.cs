namespace Project.Service
{
    public interface IVehicleService
    {
        Task<bool> AddMake(IVehicleMake make);
        Task<bool> AddModel(IVehicleModel model);
        Task<bool> DeleteMake(int id);
        Task<bool> DeleteModel(int id);
        Task<PaginatedList<IVehicleMake>> GetAllMakes(SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings);
        Task<PaginatedList<IVehicleModel>> GetAllModels(SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings);
        Task<PaginatedList<IVehicleModel>> GetAllModelsByMake(int makeId, SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings);
        Task<IVehicleMake> GetMakeById(int id);
        Task<IVehicleModel> GetModelById(int id);
        Task<bool> UpdateMake(IVehicleMake make);
        Task<bool> UpdateModel(IVehicleModel make);
    }
}