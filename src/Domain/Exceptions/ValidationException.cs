
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occured")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
