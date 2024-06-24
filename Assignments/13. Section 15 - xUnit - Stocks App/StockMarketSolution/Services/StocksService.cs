using Entity;
using ServiceContract;
using ServiceContract.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StocksService : IStocksService
    {
        List<BuyOrder> _buyOrders;
        List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }

        /// <summary>
        /// Inserts a new buy order into the database table called 'BuyOrders'.
        /// </summary>
        /// <param name="buyOrderRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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

            BuyOrderResponse buyOrderResponse = buyOrderRequest.ToBuyOrderResponse();
            buyOrderResponse.BuyOrderID = Guid.NewGuid();

            BuyOrder buyOrder = buyOrderResponse.ToBuyOrder();

            _buyOrders.Add(buyOrder);

            return Task.FromResult(buyOrderResponse);
        }

        /// <summary>
        /// Inserts a new sell order into the database table called 'SellOrders'.
        /// </summary>
        /// <param name="sellOrderRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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

            SellOrderResponse sellOrderResponse = sellOrderRequest.ToSellOrderResponse();
            sellOrderResponse.SellOrderID = Guid.NewGuid();

            SellOrder sellOrder = sellOrderResponse.ToSellOrder();

            _sellOrders.Add(sellOrder);

            return Task.FromResult(sellOrderResponse);
        }

        /// <summary>
        /// Returns the existing list of buy orders retrieved from database table called 'BuyOrders'.
        /// </summary>
        /// <returns></returns>
        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> buyOrderResponses = _buyOrders.Select(buyOrder => buyOrder.ToBuyOrderResponse()).ToList();
            return Task.FromResult(buyOrderResponses);
        }

        /// <summary>
        /// Returns the existing list of sell orders retrieved from database table called 'SellOrders'.
        /// </summary>
        /// <returns></returns>
        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> sellOrderResponses = _sellOrders.Select(sellOrder => sellOrder.ToSellOrderResponse()).ToList();
            return Task.FromResult(sellOrderResponses);
        }
    }
}
