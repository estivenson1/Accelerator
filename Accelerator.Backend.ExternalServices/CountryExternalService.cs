using Accelerator.Backend.Contracts.ExternalServices;
using Accelerator.Backend.Entities._1Referentials;
using Accelerator.Backend.ExternalServices._2Proxy;
using Accelerator.Entities.Backend.Response;
using Microsoft.Extensions.Options;

namespace Accelerator.Backend.ExternalServices;

public class CountryExternalService : ICountryExternalService
{
    /// <summary>
    /// The proxy.
    /// </summary>
    readonly CountryProxy _proxy;

    public CountryExternalService(IOptions<List<ServiceSettings>> serviceOptions)
    {
        _proxy=new CountryProxy(serviceOptions);
    }
    public Task<IList<CountryResponse>> GetCountries()
    {
        var result = _proxy.GetCountries();

        return result;
    }
}
