using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using JoePizzaApp.Data;
using JoePizzaApp.Models;

namespace PizzaPlanet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pizzas = await _context.Pizzas.ToListAsync(); // Get all pizzas
            var pizzaQuantities = new Dictionary<int, int>();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userCart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (userCart != null)
                {
                    foreach (var cartItem in userCart.CartItems)
                    {
                        pizzaQuantities[cartItem.PizzaId] = cartItem.Quantity;
                    }
                }
            }
            /*else
            {
                return View("LoginError");
            }*/

            var viewModel = new PizzaViewModel
            {
                Pizzas = pizzas,
                PizzaQuantities = pizzaQuantities
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Cart()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the user's cart and load related cart items
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Pizza)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int pizzaId, int quantity)
        {
            try
            {
                // Get the current user's ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Find the user's cart in the database
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                // Check if the cart doesn't exist for the user, create one
                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    _context.Carts.Add(cart);
                }

                // Check if the pizza is already in the cart
                var existingCartItem = cart.CartItems.FirstOrDefault(item => item.PizzaId == pizzaId);

                if (existingCartItem != null)
                {
                    // Update the quantity
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    // Create a new cart item and add it to the cart
                    var cartItem = new CartItem
                    {
                        PizzaId = pizzaId,
                        Quantity = quantity
                    };
                    cart.CartItems.Add(cartItem);
                }

                // Save changes to persist the updated cart
                await _context.SaveChangesAsync();

                return RedirectToAction("Cart");
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, and return an appropriate view or message
                return View("LoginError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                // Find the cart item by its ID
                var cartItem = await _context.CartItems.FindAsync(cartItemId);

                if (cartItem != null)
                {
                    // Remove the cart item from the context and save changes
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Cart");
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, and return an appropriate view or message
                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult OrderConfirmed()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve cart items for the current user
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Pizza)
                .FirstOrDefault(c => c.UserId == userId);

            // Create a new order record and save it to the database
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now, // You can set the order date here
                                          // Add other order-related properties if needed
            };

            // Create a list to store order details
            var orderDetails = new List<OrderDetail>();

            foreach (var cartItem in cart.CartItems)
            {
                // Create an order detail for each cart item
                var orderDetail = new OrderDetail
                {
                    PizzaId = cartItem.PizzaId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Pizza.Price,
                    // Calculate the total price for this order detail
                    TotalPrice = cartItem.Quantity * cartItem.Pizza.Price
                };

                orderDetails.Add(orderDetail);
            }

            // Set the order's details
            order.OrderDetails = orderDetails;

            // Calculate the total order price
            order.TotalPrice = orderDetails.Sum(od => od.TotalPrice);

            // Save the order to the database
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear the cart items since the order is confirmed
            cart.CartItems.Clear();
            _context.SaveChanges();

            // Pass the order details to the view
            return View(order);
        }



    }
}