using Accelerator.Entities.Backend.Response;


namespace Accelerator.Backend.Contracts.Business;

public interface ICountryBL
{
    Task<Response<CountryResponse>> GetCountries();
}
