using Accelerator.Entities.Backend.Response;
using Accelerator.Frontend.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;


namespace Accelerator.Frontend.ExternalServices._1Referentials;


/// <summary>
/// This file will be communicate with services api rest
/// </summary>
/// <typeparam name="T">Especific object to return service</typeparam>
public class ClientWebBase<T> where T : class, new()
{
    /// <summary>
    /// The media type JSON
    /// </summary>
    protected const string MediaTypeJson = "application/JSON; charset=utf-8";
    /// <summary>
    /// Este valor se utiliza para el AppRating
    /// </summary>
    protected const string GetValidateListQuestion = "GetValidateListQuestion";
    /// <summary>
    /// _response backend services
    /// </summary>
    protected Response<T> _response;



    /// <summary>
    /// The service URL base
    /// </summary>
    private string _suffixApiManagement;

    /// <summary>
    /// The service URL base
    /// </summary>
    private string _serviceUrlBase;

    /// <summary>
    /// Gets the service URL base.
    /// </summary>
    /// <value>
    /// The service URL base.
    /// </value>
    public string ServiceUrlBase
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_serviceUrlBase))
            {
                string endpoint = ConfigurationBind.ApiUrl ?? string.Empty;
                string suffix = _suffixApiManagement ?? string.Empty;
                _serviceUrlBase = string.Format(endpoint, suffix);
            }
            return _serviceUrlBase;
        }
    }

    /// <summary>
    /// Gets or sets the service URL.
    /// </summary>
    /// <value>
    /// The service URL.
    /// </value>
    protected string ServiceUrl { get; set; }

    /// <summary>
    /// token api managment
    /// </summary>
    private string _tokenApiManagment;


    /// <summary>
    /// Gets token api managment
    /// </summary>
    /// <value>
    /// The service URL base.
    /// </value>
    //public string TokenApiManagment
    //{
    //    get
    //    {
    //        if (string.IsNullOrWhiteSpace(_tokenApiManagment))
    //        {
    //            _tokenApiManagment = SettingsAutenticationServices.TokenApiManagment(ConfigurationData);
    //        }
    //        return _tokenApiManagment;
    //    }
    //}

    /// <summary>
    /// Gets or sets the token information.
    /// </summary>
    /// <value>
    /// The token information.
    /// </value>
    //protected TokenAuth TokenInfo { get; set; }
    private readonly IConfiguration ConfigurationData;

    protected ClientWebBase(string controller, string suffixApiManagement, IConfiguration configuration)
    {
        ConfigurationData = configuration;
        _suffixApiManagement = suffixApiManagement;
        SetTokenInfo();
        //ServiceUrl = $"{ServiceUrlBase}/{controller}";
        ServiceUrl = $"{ServiceUrlBase}{controller}";
        InicializeResponse();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWebBase{T}"/> class.
    /// </summary>
    /// <param name="controller">The controller.</param>
    //protected ClientWebBase(string controller, IConfiguration configuration)
    //{
    //    ConfigurationData = configuration;
    //    SetTokenInfo();
    //    ServiceUrl = $"{ServiceUrlBase}/{controller}";
    //    _authService = new AuthenticationServiceClient(ConfigurationData);
    //    InicializeResponse();
    //}

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWebBase{T}"/> class.
    /// </summary>
    /// <param name="controller">The controller.</param>
    //protected ClientWebBase(string controller, string suffixApiManagement, IConfiguration configuration)
    //{
    //    ConfigurationData = configuration;
    //    _suffixApiManagement = suffixApiManagement;
    //    SetTokenInfo();
    //    ServiceUrl = $"{ServiceUrlBase}/{controller}";
    //    _authService = new AuthenticationServiceClient(ConfigurationData);
    //    InicializeResponse();
    //}

    /// <summary>
    /// Inicializes the response.
    /// </summary>
    private void InicializeResponse()
    {
        _response = new Response<T>
        {
            TransactionComplete = false,
            Data = new List<T>(),
            Message = new List<string>()
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWebBase{T}"/> class.
    /// </summary>
    public ClientWebBase(IConfiguration configuration)
    {
        ConfigurationData = configuration;
        SetTokenInfo();
        ServiceUrl = $"{ServiceUrlBase}{typeof(T).Name}";
        InicializeResponse();
    }

    /// <summary>
    /// Sets the token information.
    /// </summary>
    private void SetTokenInfo()
    {
        //if (Settings.UserAuthInfo != null && Settings.UserAuthInfo?.Data.Count > 0)
        //{
        //    TokenInfo = Settings.UserAuthInfo.Data.FirstOrDefault()?.TokenInfo;
        //}
    }

    /// <summary>
    /// Gets the HTTP client.
    /// </summary>
    /// <returns></returns>
    protected HttpClient GetHttpClient()
    {
        //RefreshToken();
        HttpClient httpClient = new HttpClient();
        //if (!string.IsNullOrWhiteSpace(TokenInfo?.AccessToken))
        //{
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenInfo.TokenType, TokenInfo.AccessToken);
        //}
        //httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", TokenApiManagment);
        return httpClient;
    }


    /// <summary>
    /// Refreshes the token.
    /// </summary>
    //private void RefreshToken()
    //{
    //    SetTokenInfo();
    //    if (TokenInfo?.Expiration > DateTime.Now)
    //    {
    //        return;
    //    }
    //    if (TokenInfo != null)
    //    {
    //        Task.Run(async () =>
    //        {
    //            var InfoAuth = MemoryCacheDictionary<RequestAuthenticate>.Instance.GetValue("GetTokenInfo");
    //            if (InfoAuth?.DocumentId != null)
    //            {
    //                await _authService.Authenticate(InfoAuth).ConfigureAwait(false);
    //            }
    //            else
    //            {
    //                await _authService.AuthenticateOidc(MemoryCacheDictionary<string>.Instance.GetValue("GetTokenInfoOidc")).ConfigureAwait(false);
    //            }
    //        }).GetAwaiter().GetResult();
    //    }
    //    SetTokenInfo();
    //}

    /// <summary>
    /// Posts the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<string> Post(T request, bool isString = false)
    {
        HttpClient client = GetHttpClient();
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = null;
        response = await client.PostAsync(ServiceUrl, content).ConfigureAwait(false);
        string responseString = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        return responseString;
    }

    /// <summary>
    /// Posts the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<Response<T>> Post(object request, string action = "", int mins = 0)
    {
        try
        {
            HttpClient client = GetHttpClient();
            Response<T> result = new Response<T>();
            string json = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();


            string key = string.Format("{0}{1}{2}", ServiceUrl.ToString(CultureInfo.CurrentCulture), action, json);
            //string resultCache = ManageCache(key);

            //if (string.IsNullOrEmpty(resultCache))
            //{
            Task.Run(async () =>
            {
                response = await client.PostAsync($"{ServiceUrl.ToString(CultureInfo.CurrentCulture)}/{action}", content);
            }).GetAwaiter().GetResult();

            string responseString = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<Response<T>>(responseString);

                //if (mins > 0)
                //{
                //    Barrel.Current.Add(key, responseString, TimeSpan.FromMinutes(mins));
                //}
            }
            else
            {
                result = new Response<T>
                {
                    TransactionComplete = false,
                    ResponseCode = (int)response.StatusCode
                };
            }
            //}
            //else
            //{
            //    result = JsonConvert.DeserializeObject<Response<T>>(resultCache);
            //}

            return result;
        }
        catch (Exception ex)
        {
            return new Response<T>
            {
                TransactionComplete = false,
                ResponseCode = (int)System.Net.HttpStatusCode.Forbidden
            };
        }
    }

    /// <summary>
    /// Posts the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<string> Post<TEntity>(TEntity request, string action, bool isString)
    {
        HttpClient client = GetHttpClient();
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = null;
        var url_ = $"{ServiceUrl.ToString(CultureInfo.CurrentCulture)}/{action}";
        Task.Run(async () =>
        {
            response = await client.PostAsync(url_, content);
        }).GetAwaiter().GetResult();

        string responseString = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        return responseString;
    }

    /// <summary>
    /// Posts the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<Response<TEntity>> Post<TEntity>(object request, string action = "") where TEntity : class, new()
    {
        try
        {
            HttpClient client = GetHttpClient();
            string json = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{ServiceUrl}/{action}", content);

            string responseString = string.Empty;
            Response<TEntity> result;
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<Response<TEntity>>(responseString);
            }
            else
            {
                result = new Response<TEntity>
                {
                    TransactionComplete = false,
                    ResponseCode = (int)response.StatusCode
                };
            }

            return await Task.FromResult(result);
        }
        catch (Exception)
        {
            return new Response<TEntity>
            {
                TransactionComplete = false,
                ResponseCode = (int)System.Net.HttpStatusCode.Forbidden
            };
        }
    }





    /// <summary>
    /// Posts the specified request.
    /// Flag is used when the response is just a Primitive Object
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<TEntity> Post<TEntity>(object request, string action = "", string flag = "") where TEntity : class, new()
    {
        try
        {
            HttpClient client = GetHttpClient();
            string json = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{ServiceUrl}/{action}", content);

            string responseString = string.Empty;
            TEntity result = default;
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<TEntity>(responseString);
            }


            return await Task.FromResult(result);
        }
        catch (Exception)
        {
            return default;
        }
    }

    /// <summary>
    /// Posts the specified action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public async Task<Response<T>> Post(string action = "")
    {
        try
        {
            HttpClient client = GetHttpClient();

            string json = JsonConvert.SerializeObject(null);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            string urlRequest = $"{ServiceUrl}/{action}";
            var response = await client.PostAsync(urlRequest, content);

            string responseString = string.Empty;

            Response<T> result = new Response<T>();
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<Response<T>>(responseString);

            }
            else
            {
                result = new Response<T>
                {
                    TransactionComplete = false,
                    ResponseCode = (int)response.StatusCode
                };
            }

            return result;
        }
        catch (Exception ex)
        {
            return new Response<T>
            {
                TransactionComplete = false,
                ResponseCode = (int)System.Net.HttpStatusCode.Forbidden
            };
        }
    }

    /// <summary>
    /// Gets the specified capacity name.
    /// </summary>
    /// <param name="capacityName">Name of the capacity.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public async Task<Response<T>> Get(string parameters, string capacityName)
    {
        try
        {
            HttpClient client = GetHttpClient();

            string urlRequest = $"{ServiceUrl}/{capacityName}";
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                urlRequest = string.Concat(urlRequest, "/", parameters);
            }

            HttpResponseMessage response = await client.GetAsync(urlRequest).ConfigureAwait(false);

            string responseString = string.Empty;
            Response<T> result = new Response<T>();

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<Response<T>>(responseString);
            }
            else
            {
                result = new Response<T>
                {
                    TransactionComplete = false,
                    ResponseCode = (int)response.StatusCode
                };
            }

            return result;
        }

        catch (Exception)
        {
            return new Response<T>
            {
                TransactionComplete = false,
                ResponseCode = (int)System.Net.HttpStatusCode.Forbidden
            };
        }
    }

    /// <summary>
    /// Get the specified parameters.
    /// </summary>
    /// <returns>A List.</returns>
    /// <param name="parameters">Parameters.</param>
    public async Task<List<T>> Get(string parameters)
    {
        HttpClient client = GetHttpClient();

        HttpResponseMessage response = await client.GetAsync($"{ServiceUrl}/{parameters}").ConfigureAwait(false);

        List<T> listItems = new List<T>();
        if (!response.IsSuccessStatusCode)
        {
            return listItems;
        }

        string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;
        listItems = JsonConvert.DeserializeObject<List<T>>(responseString);

        return listItems;
    }

    public async Task<Response<T>> GetAsync(string action = "")
    {
        try
        {
            var result = new Response<T>();
            HttpClient client = GetHttpClient();

            HttpResponseMessage response = await client.GetAsync($"{ServiceUrl}/{action}");


            string responseString = string.Empty;
            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<Response<T>>(responseString);
            }
            else
            {
                result = new Response<T>
                {
                    TransactionComplete = false,
                    ResponseCode = (int)response.StatusCode
                };
            }

            return result;
        }
        catch (Exception e)
        {
            return new Response<T>
            {
                TransactionComplete = false,
                ResponseCode = (int)System.Net.HttpStatusCode.Forbidden
            };
        }

    }


    /// <summary>
    /// Gets the specified parameters.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>Return entity filtered by parameters</returns>
    public async Task<Response<T>> Get(Dictionary<string, string> parameters, string action = "", int mins = 10)
    {
        try
        {
            var result = new Response<T>();
            HttpClient client = GetHttpClient();
            StringBuilder parametersConcated = new StringBuilder();

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }


            foreach (KeyValuePair<string, string> pair in parameters)
            {
                parametersConcated.AppendFormat(CultureInfo.CurrentCulture, "{0}={1}&", pair.Key, pair.Value);
            }

            var key = string.Format("{0}{1}{2}", ServiceUrl.ToString(CultureInfo.CurrentCulture), action, parametersConcated.ToString().TrimEnd('&'));
            //var resultCache = ManageCache(key);
            //if (string.IsNullOrEmpty(resultCache) || action.Equals(GetValidateListQuestion))
            //{
            HttpResponseMessage response = await client.GetAsync($"{ServiceUrl}/{action}?{parametersConcated.ToString().TrimEnd('&')}").ConfigureAwait(false);


            string responseString = string.Empty;
            if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<Response<T>>(responseString);
                //if (mins > 0)
                //{
                //    Barrel.Current.Add(key, responseString, TimeSpan.FromMinutes(mins));
                //}
            }
            else
            {
                result = new Response<T>
                {
                    TransactionComplete = false,
                    ResponseCode = (int)response.StatusCode
                };
            }
            //}
            //else
            //{
            //    result = JsonConvert.DeserializeObject<Response<T>>(resultCache);
            //}

            return result;
        }
        catch (Exception e)
        {
            return new Response<T>
            {
                TransactionComplete = false,
                ResponseCode = (int)System.Net.HttpStatusCode.Forbidden
            };
        }

    }

    /// <summary>
    /// Gets with one parameter.
    /// </summary>
    /// <returns>The one parameter.</returns>
    /// <param name="parameter">Parameter.</param>
    public async Task<bool> GetOneParameter(string parameter)
    {
        HttpClient client = GetHttpClient();
        StringBuilder parametersConcated = new StringBuilder();
        HttpResponseMessage response = await client.GetAsync($"{ServiceUrl}/{parameter}").ConfigureAwait(false);

        bool responseBool;

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        responseBool = bool.Parse(responseString);

        return responseBool;
    }

    /// <summary>
    /// Method to return chain of characters
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns>Chain of characters</returns>
    public async Task<string> GetString(string parameter)
    {
        HttpClient client = GetHttpClient();
        string responseString = string.Empty;
        HttpResponseMessage response = await client.GetAsync($"{ServiceUrl}/{parameter}").ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        return responseString;
    }

    //public string ManageCache(string key)
    //{
    //    var json = string.Empty;
    //    if (!Barrel.Current.IsExpired(key))
    //    {
    //        json = Barrel.Current.Get<string>(key);
    //    }
    //    return json;
    //}

}