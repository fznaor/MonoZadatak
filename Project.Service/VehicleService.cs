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

        public async Task<List<IVehicleMake>> GetAllMakes()
        {
            try
            {
                var data = await _context.Makes.ToListAsync();
                return new List<IVehicleMake>(_mapper.Map<List<VehicleMake>>(data));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<IVehicleModel>> GetAllModels()
        {
            try
            {
                var data = await _context.Models.ToListAsync();
                return new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(data));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<IVehicleModel>> GetAllModelsByMake(int makeId)
        {
            try
            {
                var data = await _context.Models
                            .Where(m => m.Make.VehicleMakeId == makeId)
                            .ToListAsync();
                return new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(data));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
