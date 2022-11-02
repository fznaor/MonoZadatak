using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleService : IVehicleService
    {
        private IVehicleContext _context;
        private IMapper _mapper;

        public VehicleService(IVehicleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<IVehicleMake>> GetAllMakes(SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings)
        {
            try
            {
                var data = from m in _context.Makes select m;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    data = data.Where(m => m.Name.ToUpper().Contains(searchTerm.ToUpper()));
                }

                if (sortSettings.SortBy == "Name" && sortSettings.SortAscending)
                {
                    data = data.OrderBy(m => m.Name);
                }
                else if(sortSettings.SortBy == "Name" && !sortSettings.SortAscending)
                {
                    data = data.OrderByDescending(m => m.Name);
                }
                else if (sortSettings.SortBy == "Abbreviation" && sortSettings.SortAscending)
                {
                    data = data.OrderBy(m => m.Abbreviation);
                }
                else if (sortSettings.SortBy == "Abbreviation" && !sortSettings.SortAscending)
                {
                    data = data.OrderByDescending(m => m.Abbreviation);
                }

                var count = await data.CountAsync();
                var items = await data.Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize).Take(paginationSettings.PageSize).ToListAsync();
                var interfaceTypeItems = new List<IVehicleMake>(_mapper.Map<List<VehicleMake>>(items));

                return new PaginatedList<IVehicleMake>(interfaceTypeItems, count, paginationSettings.PageNumber, paginationSettings.PageSize);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginatedList<IVehicleModel>> GetAllModels(SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings)
        {
            try
            {
                var data = from m in _context.Models select m;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    data = data.Where(m => m.Name.ToUpper().Contains(searchTerm.ToUpper()));
                }

                if (sortSettings.SortBy == "Name" && sortSettings.SortAscending)
                {
                    data = data.OrderBy(m => m.Name);
                }
                else if (sortSettings.SortBy == "Name" && !sortSettings.SortAscending)
                {
                    data = data.OrderByDescending(m => m.Name);
                }
                else if (sortSettings.SortBy == "Abbreviation" && sortSettings.SortAscending)
                {
                    data = data.OrderBy(m => m.Abbreviation);
                }
                else if (sortSettings.SortBy == "Abbreviation" && !sortSettings.SortAscending)
                {
                    data = data.OrderByDescending(m => m.Abbreviation);
                }

                var count = await data.CountAsync();
                var items = await data.Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize).Take(paginationSettings.PageSize).ToListAsync();
                var interfaceTypeItems = new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(items));

                return new PaginatedList<IVehicleModel>(interfaceTypeItems, count, paginationSettings.PageNumber, paginationSettings.PageSize);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginatedList<IVehicleModel>> GetAllModelsByMake(int makeId, SortSettings sortSettings, string searchTerm, PaginationSettings paginationSettings)
        {
            try
            {
                var data = _context.Models
                            .Where(m => m.Make.VehicleMakeId == makeId);

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    data = data.Where(m => m.Name.ToUpper().Contains(searchTerm.ToUpper()));
                }

                if (sortSettings.SortBy == "Name" && sortSettings.SortAscending)
                {
                    data = data.OrderBy(m => m.Name);
                }
                else if (sortSettings.SortBy == "Name" && !sortSettings.SortAscending)
                {
                    data = data.OrderByDescending(m => m.Name);
                }
                else if (sortSettings.SortBy == "Abbreviation" && sortSettings.SortAscending)
                {
                    data = data.OrderBy(m => m.Abbreviation);
                }
                else if (sortSettings.SortBy == "Abbreviation" && !sortSettings.SortAscending)
                {
                    data = data.OrderByDescending(m => m.Abbreviation);
                }

                var count = await data.CountAsync();
                var items = await data.Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize).Take(paginationSettings.PageSize).ToListAsync();
                var interfaceTypeItems = new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(items));

                return new PaginatedList<IVehicleModel>(interfaceTypeItems, count, paginationSettings.PageNumber, paginationSettings.PageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IVehicleMake> GetMakeById(int id)
        {
            var make = await _context.Makes.FindAsync(id);
            if (make == null)
            {
                throw new KeyNotFoundException("No make with given id found");
            }
            else return _mapper.Map<VehicleMake>(make);
        }

        public async Task<IVehicleModel> GetModelById(int id)
        {
            var model = await _context.Makes.FindAsync(id);
            if (model == null)
            {
                throw new KeyNotFoundException("No model with given id found");
            }
            else return _mapper.Map<VehicleModel>(model);
        }

        public async Task<bool> AddMake(IVehicleMake make)
        {
            try
            {
                _context.Makes.Add(_mapper.Map<VehicleMakeEntity>(make));
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddModel(IVehicleModel model)
        {
            try
            {
                _context.Models.Add(_mapper.Map<VehicleModelEntity>(model));
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateMake(IVehicleMake make)
        {
            try
            {
                var toUpdate = await _context.Makes.FindAsync(make.VehicleMakeId);
                if (toUpdate == null)
                {
                    return false;
                }
                _mapper.Map(make, toUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateModel(IVehicleModel model)
        {
            try
            {
                var toUpdate = await _context.Models.FindAsync(model.VehicleModelId);
                if (toUpdate == null)
                {
                    return false;
                }
                _mapper.Map(model, toUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteMake(int id)
        {
            try
            {
                var toDelete = await _context.Makes.FindAsync(id);
                if (toDelete == null)
                {
                    return false;
                }
                _context.Makes.Remove(toDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteModel(int id)
        {
            try
            {
                var toDelete = await _context.Models.FindAsync(id);
                if (toDelete == null)
                {
                    return false;
                }
                _context.Models.Remove(toDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
