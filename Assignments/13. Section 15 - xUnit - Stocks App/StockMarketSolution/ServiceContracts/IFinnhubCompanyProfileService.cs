namespace ServiceContract
{
    public interface IFinnhubCompanyProfileService : IFinnhubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}
