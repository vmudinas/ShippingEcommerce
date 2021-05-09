using System;
using System.Runtime.Serialization;

namespace ShippingEcommerce.Exceptions
{
    [Serializable]
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException()
        {
        }

        public InvalidOrderException(string message) : base(message)
        {
        }

        public InvalidOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}