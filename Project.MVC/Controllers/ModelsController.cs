using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.Models;
using Project.Service;

namespace Project.MVC.Controllers
{
    public class ModelsController : Controller
    {
        private readonly IVehicleMakeService _makeService;
        private readonly IVehicleModelService _modelService;
        private IMapper _mapper;

        public ModelsController(IVehicleMakeService makeService, IVehicleModelService modelService, IMapper mapper)
        {
            _makeService = makeService;
            _modelService = modelService;
            _mapper = mapper;
        }

        // GET: Models
        public async Task<IActionResult> Index(string nameSearchString,
                                               string nameFilter,
                                               int? selectedMakeId,
                                               string sortBy = "Name",
                                               string sortOrder = "asc",
                                               int pageNumber = 1,
                                               int pageSize = 10)
        {
            if (nameSearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                nameSearchString = nameFilter;
            }

            ViewData["NameFilter"] = nameSearchString;
            ViewData["SortBy"] = sortBy;
            ViewData["SortOrder"] = sortOrder;

            var sortSettings = new SortSettings(sortBy, sortOrder == "asc");

            var models = await _modelService.GetAllModels(sortSettings, new VehicleModelFilter(selectedMakeId, nameSearchString), new PaginationSettings(pageNumber, pageSize));
            var makes = await _makeService.GetAllMakes();

            return View(new VehicleModelIndexViewModel(models, makes));
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int id)
        {
            IVehicleModel model;
            try
            {
                model = await _modelService.GetModelById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<VehicleModelViewModel>(model);
            viewModel.Makes = await _makeService.GetAllMakes();
            return View(viewModel);
        }

        // GET: Models/Create
        public async Task<IActionResult> Create()
        {
            return View(new VehicleModelViewModel { Makes = await _makeService.GetAllMakes()});
        }

        // POST: Models/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Abbreviation, MakeId")] VehicleModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _modelService.AddModel(_mapper.Map<IVehicleModel>(model));
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            model.Makes = await _makeService.GetAllMakes();
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            IVehicleModel model;
            try
            {
                model = await _modelService.GetModelById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<VehicleModelViewModel>(model);
            viewModel.Makes = await _makeService.GetAllMakes();
            return View(viewModel);
        }

        // POST: Models/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleModelId, Name, Abbreviation, MakeId")] VehicleModelViewModel model)
        {
            if (id != model.VehicleModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool hasSucceeded = await _modelService.UpdateModel(_mapper.Map<IVehicleModel>(model));
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
            return View(model);
        }
        
        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            IVehicleModel model;
            try
            {
                model = await _modelService.GetModelById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<VehicleModelViewModel>(model);
            viewModel.Makes = await _makeService.GetAllMakes();
            return View(viewModel);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool hasSucceeded = await _modelService.DeleteModel(id);
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
