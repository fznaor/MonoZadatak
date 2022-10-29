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
            var data = await _context.Makes.ToListAsync();
            return new List<IVehicleMake>(_mapper.Map<List<VehicleMake>>(data));
        }

        public async Task<List<IVehicleModel>> GetAllModels()
        {
            var data = await _context.Models.ToListAsync();
            return new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(data));
        }

        public async Task<List<IVehicleModel>> GetAllModelsByMake(int makeId)
        {
            var data = await _context.Models
                        .Where(m => m.Make.VehicleMakeId == makeId)
                        .ToListAsync();
            return new List<IVehicleModel>(_mapper.Map<List<VehicleModel>>(data));
        }

        public async Task<int> AddMake(IVehicleMake make)
        {
            _context.Makes.Add(_mapper.Map<VehicleMakeEntity>(make));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddModel(IVehicleModel model)
        {
            _context.Models.Add(_mapper.Map<VehicleModelEntity>(model));
            return await _context.SaveChangesAsync();
        }
    }
}
