using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models;
using Project.Service;

namespace Project.MVC.Controllers
{
    public class MakesController : Controller
    {
        private readonly IVehicleMakeService _service;
        private IMapper _mapper;

        public MakesController(IVehicleMakeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Makes
        public async Task<IActionResult> Index(string searchString,
                                               string currentFilter,
                                               string sortBy = "Name", 
                                               string sortOrder = "asc", 
                                               int pageNumber = 1,
                                               int pageSize = 10)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["SortBy"] = sortBy;
            ViewData["SortOrder"] = sortOrder;

            var sortSettings = new SortSettings(sortBy, sortOrder == "asc");

            var list = await _service.GetAllMakes(sortSettings, searchString, new PaginationSettings(pageNumber, pageSize));
            var mappedList = _mapper.Map<PaginatedList<VehicleMakeViewModel>>(list);

            return View(mappedList);
        }

        // GET: Makes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            VehicleMakeViewModel make;
            try
            {
                make = _mapper.Map<VehicleMakeViewModel>(await _service.GetMakeById(id));
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            return View(make);
        }

        // GET: Makes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleMakeId,Name,Abbreviation")] VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddMake(_mapper.Map<IVehicleMake>(make));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            return View(make);
        }

        // GET: Makes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            VehicleMakeViewModel make;
            try
            {
                make = _mapper.Map<VehicleMakeViewModel>(await _service.GetMakeById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return View(make);
        }

        // POST: Makes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleMakeId,Name,Abbreviation")] VehicleMakeViewModel make)
        {
            if (id != make.VehicleMakeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool hasSucceeded = await _service.UpdateMake(_mapper.Map<IVehicleMake>(make));
                    if (!hasSucceeded)
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }

        // GET: Makes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            VehicleMakeViewModel make;
            try
            {
                make = _mapper.Map<VehicleMakeViewModel>(await _service.GetMakeById(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return View(make);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool hasSucceeded = await _service.DeleteMake(id);
                if (!hasSucceeded)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return RedirectToAction(nameof(Index));  
        }
    }
}
