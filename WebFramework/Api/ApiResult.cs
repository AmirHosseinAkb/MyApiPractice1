using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Common;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebFramework.Api
{
    public class ApiResult
    {

        public ApiResult(bool isSucceeded, ApiResultStatusCode statusCode, string message = null)
        {
            IsSucceeded = isSucceeded;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay(DisplayProperty.Name);
        }
        public string Message { get; set; }

        public ApiResultStatusCode StatusCode { get; set; }

        public bool IsSucceeded { get; set; }


        #region IMplicit Operators

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(isSucceeded: false, statusCode: ApiResultStatusCode.NotFound,null);
        }

        public static implicit operator ApiResult(NotFoundObjectResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.NotFound);
        }

        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResult(OkObjectResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(e => (string[])e.Value);
                message = string.Join(" | ", errorMessages);
            }

            return new ApiResult(false, ApiResultStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult content)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, content.Content);
        }


        #endregion
    }

    public class ApiResult<TData> : ApiResult where TData:class
    {
        public ApiResult(bool isSucceeded, ApiResultStatusCode statusCode,TData data, string message = null)
            :base(isSucceeded, statusCode, message)
        {
            Data = data;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }


        #region IMplicit Operators

        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(isSucceeded: false, statusCode: ApiResultStatusCode.NotFound,null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData) result.Value);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData) result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(e => (string[])e.Value);
                message = string.Join(" | ", errorMessages);
            }

            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, message);
        }

        public static implicit operator ApiResult<TData>(ContentResult content)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, content.Content);
        }


        #endregion
        

    }
}
