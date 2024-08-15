using Accelerator.Backend.Entities.Response;

namespace Accelerator.Backend.Contracts.ExternalServices;

public interface ICountryExternalService
{
    Task<IList<CountryResponse>> GetCountries();
}
