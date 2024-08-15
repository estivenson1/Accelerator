
using Accelerator.Entities.Backend.Response;
using Accelerator.Frontend.Contracts.ExternalServices;
using Accelerator.Frontend.ExternalServices._1Referentials;
using Accelerator.Frontend.Utils;
using Microsoft.Extensions.Configuration;

namespace Accelerator.Frontend.ExternalServices;

public class CountryExternalService : ClientWebBase<CountryResponse>, ICountryExternalService
{
    public CountryExternalService(IConfiguration configuration) : base("https://acceleratorbackendapplication.azurewebsites.net/api/Country", ConfigurationBind.SuffixCSCountry, configuration)
    {
    }

    public Task<Response<CountryResponse>> GetCountries()
    {
        var resp = GetAsync("GetCountries");
        return resp;
    }
}
