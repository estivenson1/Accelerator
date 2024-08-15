using Accelerator.Backend.Business._1Referentials;
using Accelerator.Backend.Contracts.Business;
using Accelerator.Backend.Contracts.ExternalServices;
using Accelerator.Backend.Utils.ResponseMessages;
using Accelerator.Entities.Backend.Response;

namespace Accelerator.Backend.Business
{
    public class CountryBL : BusinessBase<CountryResponse>, ICountryBL
    {
        /// <summary>
        /// The service.
        /// </summary>
        readonly ICountryExternalService _externalService;

        public CountryBL(ICountryExternalService externalService)
        {
            _externalService= externalService;
        }


        public async Task<Response<CountryResponse>> GetCountries()
        {
            try
            {
             var countries= await  _externalService.GetCountries();
                ResponseBusiness.Data = new List<CountryResponse>(countries);


                return ResponseSuccess();
            }
            catch (Exception)
            {
                return ResponseFail<CountryResponse>(ServiceResponseCode.ServiceExternalError);
            }
        }


    }
}
