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

        }



    }
}
