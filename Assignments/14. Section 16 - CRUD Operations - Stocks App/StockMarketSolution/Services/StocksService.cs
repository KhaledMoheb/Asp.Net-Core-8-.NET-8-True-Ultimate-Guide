using Entity;
using ServiceContract;
using ServiceContract.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// Service class for managing buy and sell orders.
    /// </summary>
    public class StocksService : IStocksService
    {
        private List<BuyOrder> _buyOrders;
        private List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }

        /// <summary>
        /// Inserts a new buy order into the list of buy orders.
        /// </summary>
        /// <param name="buyOrderRequest">The buy order request to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the buy order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when buyOrderRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown when buyOrderRequest validation fails.</exception>
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }

            // Validate the instance
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(buyOrderRequest, null, null);
            bool isValid = Validator.TryValidateObject(buyOrderRequest, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults[0].ErrorMessage);
            }

            // Convert to response and generate ID
            BuyOrderResponse buyOrderResponse = buyOrderRequest.ToBuyOrderResponse();
            buyOrderResponse.BuyOrderID = Guid.NewGuid();

            // Convert to entity and add to list
            BuyOrder buyOrder = buyOrderResponse.ToBuyOrder();
            _buyOrders.Add(buyOrder);

            return Task.FromResult(buyOrderResponse);
        }

        /// <summary>
        /// Inserts a new sell order into the list of sell orders.
        /// </summary>
        /// <param name="sellOrderRequest">The sell order request to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sell order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sellOrderRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown when sellOrderRequest validation fails.</exception>
        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            // Validate the instance
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(sellOrderRequest, null, null);
            bool isValid = Validator.TryValidateObject(sellOrderRequest, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults[0].ErrorMessage);
            }

            // Convert to response and generate ID
            SellOrderResponse sellOrderResponse = sellOrderRequest.ToSellOrderResponse();
            sellOrderResponse.SellOrderID = Guid.NewGuid();

            // Convert to entity and add to list
            SellOrder sellOrder = sellOrderResponse.ToSellOrder();
            _sellOrders.Add(sellOrder);

            return Task.FromResult(sellOrderResponse);
        }

        /// <summary>
        /// Retrieves the list of buy orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of buy order responses.</returns>
        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> buyOrderResponses = _buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
            return Task.FromResult(buyOrderResponses);
        }

        /// <summary>
        /// Retrieves the list of sell orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of sell order responses.</returns>
        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> sellOrderResponses = _sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
            return Task.FromResult(sellOrderResponses);
        }
    }
}
