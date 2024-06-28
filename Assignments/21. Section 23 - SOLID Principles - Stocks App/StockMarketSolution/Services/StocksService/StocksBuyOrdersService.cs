using Entity.Model;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksService;
using Services.DbContexts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.StocksService
{
    public class StocksBuyOrdersService : IBuyOrdersService
    {
        private readonly IStocksRepository _stocksRepository;
        public StocksBuyOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        /// <summary>
        /// Inserts a new buy order into the list of buy orders.
        /// </summary>
        /// <param name="buyOrder">The buy order request to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the buy order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when buyOrderRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown when buyOrderRequest validation fails.</exception>
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrder? buyOrder)
        {
            if (buyOrder == null)
            {
                throw new ArgumentNullException(nameof(buyOrder));
            }

            //Model validation
            ValidationHelper.ModelValidation(buyOrder);

            buyOrder.BuyOrderID = Guid.NewGuid();

            // Create new buy order throught _stocksRepository
            BuyOrder buyOrderFromRepository = await _stocksRepository.CreateBuyOrder(buyOrder);

            // Convert the created buy order to buy order response
            BuyOrderResponse buyOrderResponse = buyOrderFromRepository.ToBuyOrderResponse();

            //convert the BuyOrder object into BuyOrderResponse
            return buyOrderResponse;
        }

        /// <summary>
        /// Retrieves the list of buy orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of buy order responses.</returns>
        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();
            List<BuyOrderResponse> buyOrderResponses = buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
            return buyOrderResponses;
        }
    }
}
