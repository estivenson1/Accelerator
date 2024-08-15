using Accelerator.Entities.Backend.Response;

namespace Accelerator.Frontend.Contracts.ExternalServices;

public interface ICountryExternalService
{
    Task<Response<CountryResponse>> GetCountries();
}
