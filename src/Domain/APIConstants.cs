

namespace SmartApartment.Models
{
    public class APIConstants
    {
        //public const string LocalizationSourceName = "ams";
        //public const string ConnectionStringName = "Default";
        //public const string OracleConnectionStringName = "OrDefault";
        //public const bool MultiTenancyEnabled = false;
        //public const string CacheName = "AMS.Service";

        //public class Entities
        //{
        //    public const int CustomerIDMaxLength = 20;
        //    public const int AccountNumberMaxLength = 50;
        //    public const int NameMaxLength = 50;
        //    public const int PhoneNumberMaxLength = 50;
        //}
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