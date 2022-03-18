using System;

namespace Domain.Exceptions
{ 
    public class UnProcessedEntityException : Exception
    {
        public UnProcessedEntityException(string message) : base(message)
        {
        }
    }
}
