using Accelerator.Backend.Entities._1Referentials;
using Accelerator.Backend.Entities.Response;

namespace Accelerator.Backend.Contracts.Business;

public interface ICountryBL
{
    Task<Response<CountryResponse>> GetCountries();
}
