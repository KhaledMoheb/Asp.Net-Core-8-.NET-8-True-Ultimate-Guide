using Stocks.Core.Exceptions;
using Stocks.Core.RepositoryContracts;
using Stocks.Core.ServiceContracts.FinnhubService;

namespace Stocks.Core.Services.FinnhubService
{
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubCompanyProfileService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        /// <summary>
        /// Retrieves company profile information from Finnhub API.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol for which to fetch the company profile.</param>
        /// <returns>A dictionary containing the response from Finnhub API.</returns>
        /// <exception cref="FinnhubException">Thrown when no response is received from Finnhub server or when there is an error in the response.</exception>
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            try
            {
                // invoke _finnhubRepository
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);

                return responseDictionary;
            }
            catch (Exception ex)
            {
                throw new FinnhubException($"Error in {nameof(GetCompanyProfile)}", ex);
            }
        }
    }
}
