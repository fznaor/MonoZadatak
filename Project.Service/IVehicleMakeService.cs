namespace Project.Service
{
    public interface IVehicleMakeService
    {
        Task<bool> AddMake(IVehicleMake make);
        Task<bool> DeleteMake(int id);
        Task<PaginatedList<IVehicleMake>> GetAllMakes(SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings);
        Task<IVehicleMake> GetMakeById(int id);
        Task<bool> UpdateMake(IVehicleMake make);
    }
}