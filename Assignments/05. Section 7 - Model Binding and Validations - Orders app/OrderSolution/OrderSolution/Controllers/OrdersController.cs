using Microsoft.AspNetCore.Mvc;
using OrderSolution.Models;

namespace OrderSolution.Controllers
{
    public class OrdersController : Controller
    {
        // Route: /order
        [Route("/order")]
        public IActionResult Order(Order order)
        {
            // Check if model state is valid
            if (!ModelState.IsValid)
            {
                // If not valid, collect error messages
                string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));

                // Return bad request with error messages
                return BadRequest(errors);
            }

            // Generate a random order number
            int orderNumber = (new Random()).Next(0, 99999);
            order.OrderNo = orderNumber;

            // Prepare response with order number
            var response = new Dictionary<string, string>()
            {
                { "orderNumber", orderNumber.ToString() },
            };

            // Return JSON response with order number
            return Json(response);
        }
    }
}
