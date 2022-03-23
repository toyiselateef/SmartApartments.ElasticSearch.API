

namespace SmartApartment.Models
{
    public class APIConstants
    {
       
        public enum responseType
        {
            Success,
            Invalid,
            Unsuccessful,
            Failed,
            NotFound

        }

        public static class ResponseCode
        {
            public const string _00 = "00";
            public const string _11 = "11";
            public const string _44 = "44";
            public const string _99 = "99";
            public const string _XX = "XX";
        }

        public static class ResponseDescription
        {
            public const string Success = "Request Successfull";
            public const string RequestFailed = "Request Failed";
            public const string UnknownError = "Internal Server Error";
            public const string NotFound = "Not Found";
            public const string InvalidPayload = "Invalid Payload";
        }


    }

}