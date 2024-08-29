using System.Globalization;
using CarSelling.Models;
using CarSelling.Services;
using CarSellingWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using X.PagedList.Extensions;

namespace CarSelling.Controllers
{
    public class CarController : Controller
    {
        private readonly CarSellingDbContext _context;
        private readonly PayPalService _payPalService;

        public CarController(CarSellingDbContext context, PayPalService payPalService)
        {
            _context = context;
            _payPalService = payPalService;
        }

        [HttpPost]
        public async Task<IActionResult> BuyNow(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                return NotFound();
            }

            var amount = car.Price ?? 0;

            // Generate return and cancel URLs
            var returnUrl = Url.Action("PaymentSuccess", "Car", new { carId = carId }, Request.Scheme);
            var cancelUrl = Url.Action("PaymentCancel", "Car", new { carId = carId }, Request.Scheme);

            // Create an order with PayPal
            var orderId = await _payPalService.CreateOrder(amount, returnUrl, cancelUrl);

            // Retrieve the order details from PayPal to get the approval URL
            var orderDetails = await _payPalService.GetOrderDetails(orderId);
            var approvalUrl = orderDetails.Links
                .FirstOrDefault(link => link.Rel == "approve")?.Href;

            if (string.IsNullOrEmpty(approvalUrl))
            {
                // Handle error: Approval URL not found
                return BadRequest("Unable to retrieve PayPal approval URL.");
            }

            // Redirect user to PayPal checkout page
            return Redirect(approvalUrl);
        }

        public async Task<IActionResult> PaymentSuccess(int carId, string token)
        {
            // Verify the payment with PayPal
            var orderDetails = await _payPalService.GetOrderDetails(token);
            if (orderDetails.Status == "APPROVED")
            {
                // Update your order status and perform any other required actions
                ViewBag.Message = "Payment successful! Thank you for your purchase.";
                return View();

            }
            else
            {
                ViewBag.Message = "Payment was not completed. Please try again.";
                return View("PaymentCancel");
            }
        }

        public IActionResult PaymentCancel(int carId)
        {
            ViewBag.Message = "Payment was canceled. Please try again.";
            return View("Index");
        }

        public IActionResult Index(int? page)
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
                }).ToPagedList(page ?? 1, 6); // Assuming PagedList library for pagination

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
                // Validate CarCreationDate
                if (viewModel.CarCreationDate == null || IsInvalidDate(viewModel.CarCreationDate))
                {
                    ModelState.AddModelError("CarCreationDate", "The Car Creation Date is not valid. Please provide a valid date.");
                }

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
                        EngineType = viewModel.EngineType,
                        NumberOfDoors = viewModel.NumberOfDoors,
                        Location = viewModel.Location,
                        SafetyFeatures = viewModel.SafetyFeatures,
                        ComfortFeatures = viewModel.ComfortFeatures,
                        Color = viewModel.Color
                    };

                    _context.Cars.Add(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            // If we got this far, something failed, redisplay the form
            return View(viewModel);
        }

        private bool IsInvalidDate(DateTime date)
        {
            return date == DateTime.MinValue || date > DateTime.Now;
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
                ImgUrl = car.ImgUrl,
                EngineType = car.EngineType.ToString(),
                Location = car.Location,
                SafetyFeatures = car.SafetyFeatures,
                ComfortFeatures = car.ComfortFeatures,
                Color = car.Color.ToString(),
                NumberOfDoors = car.NumberOfDoors

            };
            return View(carViewModel);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var carToDelete = _context.Cars
                                        .Include(c => c.Brand)
                                        .FirstOrDefault(c => c.Id == id);
            if (carToDelete == null)
            {
                return NotFound();
            }

            return View(carToDelete); // Pass the car object to the view
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                return NotFound();
            }

            var amount = car.Price ?? 0;


            var returnUrl = Url.Action("PaymentSuccess", "Car", new { carId }, Request.Scheme);
            var cancelUrl = Url.Action("PaymentCancel", "Car", new { carId }, Request.Scheme);

            var orderId = await _payPalService.CreateOrder(amount, returnUrl, cancelUrl);

            return Json(new { id = orderId });
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var carToDelete = _context.Cars.Find(id);
            if (carToDelete == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(carToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to the list view or another relevant action
        }
    }
}
