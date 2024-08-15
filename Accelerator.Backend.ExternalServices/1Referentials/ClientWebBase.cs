using Accelerator.Backend.Entities._1Referentials;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Accelerator.Backend.Utils.Extensions;

namespace Accelerator.Backend.ExternalServices._1Referentials;

/// <summary>
/// Represent client web base class.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public class ClientWebBase<T> where T : class, new()
{
    /// <summary>
    /// The media type JSON
    /// </summary>
    protected const string MediaTypeJson = "Application/JSON; charset=utf-8";










    /// <summary>
    /// The logger service.
    /// </summary>
    //public readonly ILoggerService LoggerService;

    /// <summary>
    /// The time audit
    /// </summary>
    protected readonly Stopwatch _timeAudit;



    private static Lazy<HttpClient> _httpClient;

    public Lazy<HttpClient> HttpClient
    {
        get
        {
            if (_httpClient is null)
            {
                _httpClient = new Lazy<HttpClient>();
            }
            return _httpClient;
        }
    }


    public ClientWebBase(IOptions<List<ServiceSettings>> serviceOptions,
        string serviceName,
        string controllerName)
    {
        if (serviceOptions == null)
        {
            return;
        }
        _timeAudit = new Stopwatch();
        var service = serviceOptions.Value.FindAll(a => a.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        Url = new Uri($"{service.Url}/{controllerName}");
    }



    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWebBase{T}"/> class.
    /// </summary>
    /// <param name="serviceOptions">The service options.</param>
    /// <param name="serviceName">Name of the service.</param>
    /// <param name="controllerName">Name of the controller.</param>
    //public ClientWebBase(IOptions<List<ServiceSettings>> serviceOptions,
    //    string serviceName,
    //    string controllerName,
    //    ILoggerService log)
    //{
    //    if (serviceOptions == null)
    //    {
    //        return;
    //    }
    //    _timeAudit = new Stopwatch();
    //    LoggerService = log;
    //    var service = serviceOptions.Value.FindAll(a => a.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
    //    Url = new Uri($"{service.Url}/{controllerName}");
    //}

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWebBase{T}"/> class.
    /// </summary>
    /// <param name="serviceOptions"></param>
    /// <param name="serviceName"></param>
    /// <param name="controllerName"></param>
    /// <param name="optionUserSecret"></param>
    /// <param name="log"></param>
    //public ClientWebBase(IOptions<List<ServiceSettings>> serviceOptions, string serviceName, string controllerName, IOptions<UserSecretSettings> optionUserSecret, ILoggerService log)
    //{
    //    if (serviceOptions == null)
    //    {
    //        return;
    //    }

    //    var service = serviceOptions.Value.FindAll(a => a.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

    //    Url = new Uri($"{service.Url}/{controllerName}");

    //    _timeAudit = new Stopwatch();
    //}

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWebBase{T}"/> class.
    /// </summary>
    /// <param name="serviceOptions">The service options.</param>
    /// <param name="serviceName">Name of the service.</param>
    /// <param name="controllerName">Name of the controller.</param>
    /// <param name="token">The token.</param>
    //public ClientWebBase(IOptions<List<ServiceSettings>> serviceOptions, string serviceName, string controllerName,
    //    ILoggerService log, string token, IOptions<UserSecretSettings> secret)
    //{
    //    if (serviceOptions == null)
    //    {
    //        return;
    //    }
    //    _timeAudit = new Stopwatch();
    //    var service = serviceOptions.Value.FindAll(a => a.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
    //    Url = new Uri($"{service.Url}/{controllerName}");
    //    LoggerService = log;
    //    _accessToken = token;
    //    _apim = secret.Value.Apim;
    //}

    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    /// <value>
    /// The URL.
    /// </value>
    /// <author>intergrupo\lcorreag</author>
    /// <remarks>
    /// CreationDate: 06/05/2015
    /// ModifiedBy:
    /// ModifiedDate:
    /// </remarks>
    public Uri Url { get; set; }

    /// <summary>
    /// Gets the HTTP put.
    /// </summary>
    /// <value>
    /// The HTTP put.
    /// </value>
    /// <author>intergrupo\lcorreag</author>        
    /// CreationDate: 06/05/2015
    /// ModifiedBy:
    /// ModifiedDate:
    public string HttpPut => "PUT";

    /// <summary>
    /// Gets the HTTP post.
    /// </summary>
    /// <value>
    /// The HTTP post.
    /// </value>
    /// <author>intergrupo\lcorreag</author>
    /// <remarks>
    /// CreationDate: 07/05/2015
    /// ModifiedBy:
    /// ModifiedDate:
    /// </remarks>
    public string HttpPost => "POST";

    /// <summary>
    /// Gets the HTTP get.
    /// </summary>
    /// <value>
    /// The HTTP get.
    /// </value>
    /// <author>intergrupo\lcorreag</author>
    /// <remarks>
    /// CreationDate: 07/05/2015
    /// ModifiedBy:
    /// ModifiedDate:
    /// </remarks>
    public string HttpGet => "GET";

    /// <summary>
    /// The instancefor httpClient
    /// </summary>
    private static readonly Lazy<HttpClient> _client = new Lazy<HttpClient>();





    /// <summary>
    /// Gets the request.
    /// </summary>
    /// <returns>Returns the status object.</returns>
    public HttpRequestMessage GetWebMessageWithDefaultHeadders(HttpRequestMessage request)
    {
        if (request.Content != null)
        {
            request.Content.Headers.ContentEncoding.Add("gzip");
        }

        request.Headers.Add("Accept-Type", MediaTypeJson);

        //if (_SubscriptionKey != null)
        //{
        //    request.Headers.Add("SKey", _SubscriptionKey);
        //}
        //if (_accessToken != null)
        //{
        //    request.Headers.Add("Authorization", string.Concat(TypeAuthorization, _accessToken));
        //}
        //if (_apim != null)
        //{
        //    request.Headers.Add("Ocp-Apim-Subscription-Key", _apim);
        //}

        return request;
    }

    /// <summary>
    /// Gets the list.
    /// </summary>
    /// <returns>Returns the status object.</returns>
    public IList<T> GetList()
    {
        var entidades = new List<T>();
        var elapsed = string.Empty;
        string responseString = string.Empty;

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidades = JsonConvert.DeserializeObject<List<T>>(responseString);
            }

            //Log(string.Empty, responseString, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, string.Empty, responseString, elapsed);
        }
        return entidades;
    }

    /// <summary>
    /// Gets the list.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns the status object.</returns>
    public IList<T> GetList(string id)
    {
        var entidades = new List<T>();
        var result = string.Empty;
        var elapsed = string.Empty;

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, $"{Url}/{id}"))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidades = JsonConvert.DeserializeObject<List<T>>(result);
            }

         
        }
        catch (Exception ex)
        {
            string message = ex.Message;
        }
        return entidades;
    }

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns>Returns the status object.</returns>
    public T Get()
    {
        var entidad = new T();
        var result = string.Empty;
        var elapsed = string.Empty;

        try
        {

            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidad = JsonConvert.DeserializeObject<T>(result);
            }

            
        }
        catch (Exception ex)
        {
            string message = ex.Message;
        }
        return entidad;
    }

    /// <summary>
    /// Gets the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public T Get(string id)
    {
        var entidad = new T();
        var result = string.Empty;
        var elapsed = string.Empty;

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, $"{Url}/{id}"))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidad = JsonConvert.DeserializeObject<T>(result);
            }

           
        }
        catch (Exception ex)
        {
            string message = ex.Message;
        }
        return entidad;
    }

    /// <summary>
    /// Gets the list.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public T Get(IDictionary<string, string> parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var entidad = new T();
        var result = string.Empty;
        var elapsed = string.Empty;
        var concatParameters = parameters.ConcatDictionary();

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, $"{Url}?{concatParameters}"))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();
                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidad = JsonConvert.DeserializeObject<T>(result,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                        });
            }

            //Log(concatParameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, concatParameters, result, elapsed);
        }
        return entidad;
    }

    /// <summary>
    /// Gets the data string.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">parameters</exception>
    public string GetDataString(IDictionary<string, string> parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var data = string.Empty;
        var elapsed = string.Empty;
        var concaParameters = parameters.ConcatDictionary();

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, $"{Url}?{concaParameters}"))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
            }

            //Log(concaParameters, data, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, concaParameters, data, elapsed);
        }
        return data;
    }

    /// <summary>
    /// Gets the list.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public IList<T> GetList(IDictionary<string, string> parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var entidades = new List<T>();
        var result = string.Empty;
        var elapsed = string.Empty;
        var concaParameters = parameters.ConcatDictionary();

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, $"{Url}?{concaParameters}"))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidades = JsonConvert.DeserializeObject<List<T>>(result);
            }

            //Log(concaParameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, concaParameters, result, elapsed);
        }
        return entidades;
    }

    /// <summary>
    /// Gets the list.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Returns the status object.</returns>
    public IList<T> GetList(T entity)
    {
        var entidades = new List<T>();
        var result = string.Empty;
        var elapsed = string.Empty;
        var parameters = JsonConvert.SerializeObject(entity);

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidades = JsonConvert.DeserializeObject<List<T>>(result);
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return entidades;
    }

    /// <summary>
    /// Gets the list.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Returns the status object.</returns>
    public virtual Response<T> GetListResponse(T entity)
    {
        var response = new Response<T>();
        var result = string.Empty;
        var elapsed = string.Empty;
        var parameters = JsonConvert.SerializeObject(entity);

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                response = JsonConvert.DeserializeObject<Response<T>>(result);
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return response;
    }

    /// <summary>
    /// Gets the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns the status object.</returns>
    public T Get(int id)
    {
        var entidad = new T();
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Get, Url + "/" + id.ToString(CultureInfo.CurrentCulture)))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidad = JsonConvert.DeserializeObject<T>(result);
            }

            //Log(id.ToString(), result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, id.ToString(), result, elapsed);
        }
        return entidad;
    }

    /// <summary>
    /// Inserts the specified ENTIDAD.
    /// </summary>
    /// <param name="entidad">The ENTIDAD.</param>
    /// <returns>Returns the status object.</returns>
    public T Post(T entidad)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var entidadResultado = default(T);
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {

            using (var message = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                HttpClient context = HttpClient.Value;

                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidadResultado = JsonConvert.DeserializeObject<T>(result);
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return entidadResultado;
    }

    public TResponse Post<TRequest, TResponse>(TRequest entidad)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var entidadResultado = default(TResponse);
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                _timeAudit.Start();
                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidadResultado = JsonConvert.DeserializeObject<TResponse>(result);

                //Log(parameters, result, elapsed);
            }
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return entidadResultado;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest entidad)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var entidadResultado = default(TResponse);
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                _timeAudit.Start();
                var responseContext = await context.SendAsync(request);
                result = await responseContext.Content.ReadAsStringAsync();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidadResultado = JsonConvert.DeserializeObject<TResponse>(result);

                //Log(parameters, result, elapsed);
            }
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return entidadResultado;
    }

    /// <summary>
    /// Updates the specified ENTIDAD.
    /// </summary>
    /// <param name="entidad">The ENTIDAD.</param>
    /// <param name="id">The identifier.</param>
    /// <returns>Returns the status object.</returns>
    public bool Update(T entidad, int id)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Put, Url + "/" + id.ToString(CultureInfo.CurrentCulture)))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Deletes the specified ENTIDAD.
    /// </summary>
    /// <param name="entidad">The ENTIDAD.</param>
    /// <returns>Returns the status object.</returns>
    public bool Delete(T entidad)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var result = string.Empty;
        var elapsed = string.Empty;

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Delete, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Deletes the specified ENTIDAD.
    /// </summary>
    /// <param name="entidad">The ENTIDAD.</param>
    /// <returns>Returns the status object.</returns>
    public bool Delete(int id)
    {
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Delete, this.Url + "/" + id))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
            }

            //Log(id.ToString(), result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, id.ToString(), result, elapsed);
            return false;
        }
        return true;
    }

    public TResponse Delete<TRequest, TResponse>(TRequest entidad)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var result = string.Empty;
        var elapsed = string.Empty;
        var entidadResultado = default(TResponse);

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Delete, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidadResultado = JsonConvert.DeserializeObject<TResponse>(result);
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return entidadResultado;
    }

    public TResponse Put<TRequest, TResponse>(TRequest entidad)
    {
        var parameters = JsonConvert.SerializeObject(entidad);
        var entidadResultado = default(TResponse);
        var result = string.Empty;
        var elapsed = string.Empty;
        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Put, Url))
            {
                var request = GetWebMessageWithDefaultHeadders(message);
                request.Content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseContext = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                result = responseContext.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
                entidadResultado = JsonConvert.DeserializeObject<TResponse>(result);
            }

            //Log(parameters, result, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, parameters, result, elapsed);
        }
        return entidadResultado;
    }

    /// <summary>
    /// Gets the data string.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">parameters</exception>
    public string PostGet(IDictionary<string, string> parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        var data = string.Empty;
        var elapsed = string.Empty;
        var concaParameters = parameters.ConcatDictionary();

        try
        {
            HttpClient context = HttpClient.Value;
            using (var message = new HttpRequestMessage(HttpMethod.Post, $"{Url}?{concaParameters}"))
            {
                var request = GetWebMessageWithDefaultHeadders(message);

                var response = context.SendAsync(request).GetAwaiter().GetResult();

                _timeAudit.Start();
                data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                _timeAudit.Stop();
                elapsed = _timeAudit.Elapsed.ToString();
            }

            //Log(concaParameters, data, elapsed);
        }
        catch (Exception ex)
        {
            //Log(ex, concaParameters, data, elapsed);
        }
        return data;
    }

}
