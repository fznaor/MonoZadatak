using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private IVehicleContext _context;
        private IMapper _mapper;

        public VehicleMakeService(IVehicleContext context, IMapper mapper)
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

                data = SortMakes(data, sortSettings);

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

        public IQueryable<VehicleMakeEntity> SortMakes(IQueryable<VehicleMakeEntity> data, SortSettings settings)
        {
            switch (settings.SortBy)
            {
                case "Name" :
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
                default:
                    return data;
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
                await _context.SaveChangesAsync();
                return true;
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
    }
}
