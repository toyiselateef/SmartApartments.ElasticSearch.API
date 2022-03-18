﻿using System;

namespace Domain.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message)
        : base(message)
        {
        }
    }
}
