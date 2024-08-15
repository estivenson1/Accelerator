
using Accelerator.Entities.Backend.Response;
using Accelerator.Frontend.Contracts.Business;
using Accelerator.Frontend.Contracts.ExternalServices;

namespace Accelerator.Frontend.Business;

public class CountryBL : ICountryBL
{
    private readonly ICountryExternalService _externalService;

    public CountryBL(ICountryExternalService externalService)
    {
        _externalService = externalService;
    }

    public Task<Response<CountryResponse>> GetCountries()
    {
       var resp= _externalService.GetCountries();
        return resp;
    }

}
