using Accelerator.Entities.Backend.Response;

namespace Accelerator.Frontend.Contracts.Business;

public interface ICountryBL
{
    Task<Response<CountryResponse>> GetCountries();
}
