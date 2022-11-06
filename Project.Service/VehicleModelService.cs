using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private IVehicleContext _context;
        private IMapper _mapper;

        public VehicleModelService(IVehicleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<IVehicleModel>> GetAllModels(SortSettings sortSettings, VehicleModelFilter filter, PaginationSettings paginationSettings)
        {
            try
            {
                var data = from m in _context.Models select m;

                data = FilterModels(data, filter);
                data = SortModels(data, sortSettings);

                var count = await data.CountAsync();
                var items = await data.Include(m => m.Make).Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize).Take(paginationSettings.PageSize).ToListAsync();
                var interfaceTypeItems = new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(items));

                return new PaginatedList<IVehicleModel>(interfaceTypeItems, count, paginationSettings.PageNumber, paginationSettings.PageSize);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<VehicleModelEntity> FilterModels(IQueryable<VehicleModelEntity> data, VehicleModelFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.NameFilter))
            {
                data = data.Where(m => m.Name.ToUpper().Contains(filter.NameFilter.ToUpper()));
            }
            if (filter.MakeIdFilter != null & filter.MakeIdFilter > 0)
            {
                data = data.Where(m => m.MakeId == filter.MakeIdFilter);
            }
            return data;
        }

        public IQueryable<VehicleModelEntity> SortModels(IQueryable<VehicleModelEntity> data, SortSettings settings)
        {
            switch (settings.SortBy)
            {
                case "Name":
                    if (settings.SortAscending)
                    {
                        return data.OrderBy(m => m.Name);
                    }
                    else
                    {
                        return data.OrderByDescending(m => m.Name);
                    }
                case "Abbreviation":
                    if (settings.SortAscending)
                    {
                        return data.OrderBy(m => m.Abbreviation);
                    }
                    else
                    {
                        return data.OrderByDescending(m => m.Abbreviation);
                    }
                case "Make":
                    if (settings.SortAscending)
                    {
                        return data.OrderBy(m => m.Make.Name);
                    }
                    else
                    {
                        return data.OrderByDescending(m => m.Make.Name);
                    }
                default:
                    return data;
            }
        }

        public async Task<IVehicleModel> GetModelById(int id)
        {
            var model = await _context.Models.Include(m => m.Make).FirstOrDefaultAsync(m => m.VehicleModelId == id);
            if (model == null)
            {
                throw new KeyNotFoundException("No model with given id found");
            }
            else return _mapper.Map<VehicleModel>(model);
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
