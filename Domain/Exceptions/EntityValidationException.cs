using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Exceptions
{
    public class EntityValidationException : Exception
    {
        public string PropertyName { get; }

        public EntityValidationException(string propertyName, string message)
            : base(message)
        {
            PropertyName = propertyName;
        }

        public EntityValidationException(string propertyName, string message, Exception inner)
            : base(message, inner)
        {
            PropertyName = propertyName;
        }
    }
}
