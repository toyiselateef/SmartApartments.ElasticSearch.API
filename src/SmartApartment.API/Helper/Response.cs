using SmartApartment.API.Models;
using SmartApartment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartment.API.Helper
{
    public class Response <T> : APIResponse<T>
    {
        public Response(ResponseTypes responseType, T Result)
        {
            switch (responseType)
            {
                case ResponseTypes.notfound:
                    responseCode = APIConstants.ResponseCode._44;
                    responseDescription = APIConstants.ResponseDescription.NotFound;
                    result = default(T);
                    break;
                case ResponseTypes.success:
                    responseCode = APIConstants.ResponseCode._00;
                    responseDescription = APIConstants.ResponseDescription.Success;
                    result = Result;
                    break;
                case ResponseTypes.invalid:
                    responseCode = APIConstants.ResponseCode._11;
                    responseDescription = APIConstants.ResponseDescription.InvalidPayload;
                    result = default(T);
                    break; 
                case ResponseTypes.failed:
                    responseCode = APIConstants.ResponseCode._XX;
                    responseDescription = APIConstants.ResponseDescription.RequestFailed;
                    result = default(T);
                    break; 
                case ResponseTypes.unknown:
                    responseCode = APIConstants.ResponseCode._99;
                    responseDescription = APIConstants.ResponseDescription.UnknownError;
                    result = default(T);
                    break; 
                default:
                    break;
            }
        }
    }
}
