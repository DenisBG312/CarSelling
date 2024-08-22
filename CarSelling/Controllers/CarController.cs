using CarSelling.Models;
using CarSellingWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Controllers
{
    public class CarController : Controller
    {
        private readonly CarSellingDbContext _context;
        public CarController(CarSellingDbContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.Include(c => c.Brand).ToListAsync();

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
                    Price = viewModel.Price
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
    }
}
