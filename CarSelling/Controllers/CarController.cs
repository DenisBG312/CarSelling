using CarSelling.Models;
using CarSellingWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace CarSelling.Controllers
{
    public class CarController : Controller
    {
        private readonly CarSellingDbContext _context;
        public CarController(CarSellingDbContext context) 
        {
            _context = context;
        }

        public  IActionResult Index(int? page)
        {
            // Fetch the list of cars and map it to CarIndexViewModel
            var cars = _context.Cars
                .Select(car => new CarIndexViewModel
                {
                    Id = car.Id,
                    BrandName = car.Brand.Name,
                    Model = car.Model,
                    Price = car.Price,
                    ImgUrl = car.ImgUrl
                }).ToPagedList(page ?? 1, 5);  // Assuming PagedList library for pagination

            return View(cars);
        }

        // GET: Car/Create
        public async Task<IActionResult> Create()
        {
            // Fetch brands from the database
            var brands = await _context.Brands.ToListAsync();

            // Populate the ViewModel
            var viewModel = new CarCreateViewModel
            {
                Brands = new SelectList(brands, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the ViewModel to the Car entity
                var car = new Car
                {
                    BrandId = viewModel.BrandId,
                    Model = viewModel.Model,
                    Mileage = viewModel.Mileage,
                    ImgUrl = viewModel.ImgUrl,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    CarCreationDate = viewModel.CarCreationDate,
                    CreatedAt = viewModel.CreatedAt,
                };

                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If validation fails, re-populate the brands list and return the view
            var brands = await _context.Brands.ToListAsync();
            viewModel.Brands = new SelectList(brands, "Id", "Name");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var car = _context.Cars.Include(c => c.Brand).FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var carViewModel = new CarDetailsViewModel()
            {
                Id = car.Id,
                BrandName = car.Brand.Name,
                Model = car.Model,
                Mileage = car.Mileage,
                Price = car.Price,
                CarCreationDate = car.CarCreationDate,
                CreatedAt = car.CreatedAt,
                Description = car.Description,
                ImgUrl = car.ImgUrl
            };
            return View(carViewModel);
        }
    }
}
