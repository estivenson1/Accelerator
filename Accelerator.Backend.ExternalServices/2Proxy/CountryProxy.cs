using Accelerator.Backend.Entities._1Referentials;
using Accelerator.Backend.Entities.Response;
using Accelerator.Backend.ExternalServices._1Referentials;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.Backend.ExternalServices._2Proxy
{
    public class CountryProxy : ClientWebBase<CountryResponse>
    {
        public CountryProxy(IOptions<List<ServiceSettings>> serviceOptions) : base(serviceOptions, "Restcountries", "all")
        {
        }  
        //public CountryProxy(IOptions<List<ServiceSettings>> serviceOptions, string serviceName, string controllerName) : base(serviceOptions, serviceName, controllerName)
        //{
        //}

        public async Task<IList<CountryResponse>> Get()
        {
            var countryList = new List<CountryResponse>();

            var myHttpClient = HttpClient.Value;
            var response = await myHttpClient.GetAsync(Url.ToString());
            if (response is null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var stringContent = await response.Content.ReadAsStringAsync();
            JArray countries = JArray.Parse(stringContent);

            foreach (var country in countries)
            {
                var countryInfo = new CountryResponse
                {
                    Name = country["name"]["common"].ToString(),
                    Capital = country["capital"]?.FirstOrDefault()?.ToString(),
                    Region = country["region"].ToString(),
                    Subregion = country["subregion"]?.ToString() ?? "NA",
                    Population = country["population"].ToString(),
                    Language = country["languages"]?.First?.ToString(),
                    Currency = country["currencies"]?.First?.ToString(),
                    IsoCode = country["cca2"].ToString(),
                    Flag = country["flags"]["png"].ToString()
                };

                countryList.Add(countryInfo);
            }


            return countryList;
        }
    }
}
