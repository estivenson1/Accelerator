using Accelerator.Backend.Entities._1Referentials;
using Accelerator.Backend.Utils.ResponseMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accelerator.Backend.Business._1Referentials
{
    /// <summary>
    /// base class to business
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessBase<T> where T : class, new()
    {
        /// <summary>
        /// The response business
        /// </summary>
        protected Response<T> ResponseBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessBase{T}"/> class.
        /// </summary>
        public BusinessBase()
        {
            ResponseBusiness = new Response<T>
            {
                ResponseCode = 0,
                TransactionComplete = false,
                Message = new List<string>(),
                Data = new List<T>()
            };
        }

        /// <summary>
        /// Responses the success.
        /// </summary>
        /// <returns></returns>
        public Response<T> ResponseSuccess()
        {
            ResponseBusiness.TransactionComplete = true;
            ResponseBusiness.ResponseCode = (int)ServiceResponseCode.Success;
            ResponseBusiness.Message.Add(ResponseMessages.Success);
            return ResponseBusiness;
        }

        /// <summary>
        /// Responses the success.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Response<T> ResponseSuccess(ServiceResponseCode code)
        {
            ResponseBusiness.TransactionComplete = true;
            ResponseBusiness.ResponseCode = (int)code;
            ResponseBusiness.Message.Add(ResponseMessages.ResourceManager.GetString(code.ToString()));
            return ResponseBusiness;
        }

        /// <summary>
        /// Responses the fail.
        /// </summary>
        /// <returns></returns>
        public Response<T> ResponseFail()
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)ServiceResponseCode.InternalError;
            ResponseBusiness.Message.Add(ResponseMessages.InternalError);
            return ResponseBusiness;
        }

        /// <summary>
        /// Responses the fail.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Response<T> ResponseFail(ServiceResponseCode code)
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)code;
            ResponseBusiness.Message.Add(ResponseMessages.ResourceManager.GetString(code.ToString()));
            return ResponseBusiness;
        }

        /// <summary>
        /// Responses the fail.
        /// </summary>
        /// <param name="code">The code</param>
        /// <param name="message">The message</param>
        /// <returns></returns>
        public Response<T> ResponseFail(ServiceResponseCode code, string message)
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)code;
            ResponseBusiness.Message.Add(message);
            return ResponseBusiness;
        }

        /// <summary>
        /// Responses the fail.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="messages">The message.</param>
        /// <returns></returns>
        public Response<T> ResponseFail(ServiceResponseCode code, Collection<string> messages)
        {
            ResponseBusiness.TransactionComplete = false;
            ResponseBusiness.ResponseCode = (int)code;
            ResponseBusiness.Message = messages;
            return ResponseBusiness;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static Response<TEntity> ResponseSuccess<TEntity>(IList<TEntity> entity) where TEntity : class, new()
        {
            return new Response<TEntity>
            {
                ResponseCode = (int)ServiceResponseCode.Success,
                TransactionComplete = true,
                Message = new List<string> { ResponseMessages.Success },
                Data = entity
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="fields">Este campo recibe todos los campos que presentan algun error</typeparam>
        /// <returns></returns>
        public static Response<TEntity> ResponseFail<TEntity>(ServiceResponseCode responseCode) where TEntity : class, new()
        {
            return new Response<TEntity>
            {
                ResponseCode = (int)responseCode,
                TransactionComplete = false,
                Message = new List<string>
                {
                    ResponseMessages.ResourceManager.GetString(responseCode.ToString(CultureInfo.CurrentCulture))
                }
            };
        }

        public static Response<TEntity> ResponseFail<TEntity>(ServiceResponseCode responseCode, string message) where TEntity : class, new()
        {
            return new Response<TEntity>
            {
                ResponseCode = (int)responseCode,
                TransactionComplete = false,
                Message = new List<string>
                {
                    message
                }
            };
        }
    }
}
