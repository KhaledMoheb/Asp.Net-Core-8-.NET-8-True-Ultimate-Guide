using Entitiy.Model;
using Entity.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StockMarketSolution.Controllers;
using StockMarketSolution.Models.ViewModels;

namespace StockMarketSolution.Filters.ActionFilters
{
    /// <summary>
    /// Action filter for processing order creation requests in TradeController.
    /// </summary>
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        public CreateOrderActionFilter()
        {
        }

        /// <summary>
        /// Executes asynchronously before and after the action method is called.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        /// <param name="next">The delegate to execute the next action filter or the action itself.</param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Check if the controller is TradeController
            if (context.Controller is not TradeController tradeController)
            {
                await next();
                return;
            }

            // Retrieve OrderRequest from action arguments
            Order order = null;
            if (context.ActionArguments.TryGetValue("sellOrder", out object? sellOrder))
            {
                order = context.ActionArguments["sellOrder"] as Order;
            }
            if (context.ActionArguments.TryGetValue("buyOrder", out object? buyOrder))
            {
                order = context.ActionArguments["buyOrder"] as Order;
            }


            if (order == null)
            {
                await next();
                return;
            }

            // Set the date and time of the order
            order.DateAndTimeOfOrder = DateTime.Now;

            // Clear existing model state and validate the model
            tradeController.ModelState.Clear();
            if (tradeController.TryValidateModel(order))
            {
                await next();
                return;
            }

            // If model validation fails, set errors in ViewBag and proceed to next action
            tradeController.ViewBag.Errors = tradeController.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Create a new StockTrade object from the validated order request
            StockTrade stockTrade = new StockTrade()
            {
                StockName = order.StockName,
                Quantity = order.Quantity,
                StockSymbol = order.StockSymbol
            };

            // Short-Circuits or skips the subsequent action filters & action method
            // Set the result to redirect to the Index action of TradeController with the stockTrade object
            context.Result = tradeController.View(nameof(TradeController.Index), stockTrade);
        }
    }
}
