using Accelerator.Backend.Contracts.Business;
using Accelerator.Backend.Entities._1Referentials;
using Accelerator.Backend.Entities.Response;
using Microsoft.AspNetCore.Mvc;

namespace Accelerator.Backend.Application.CountryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : Controller
{
    /// <summary>
    /// The business
    /// </summary>
    readonly ICountryBL _business;

    public CountryController(ICountryBL business)
    {
        _business=business;
    }


    [HttpGet]
    [Route("GetCountries")]
    [Produces(typeof(Response<CountryResponse>))]
    public async Task<IActionResult> GetCountries()
    {
        try
        {

            var response = await _business.GetCountries();

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
