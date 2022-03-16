using System;
using System.Runtime.Serialization;

namespace TypeCheck
{
    [Serializable]
    internal class NotDeclaredException : Exception
    {
        public NotDeclaredException()
        {
        }

        public NotDeclaredException(string message) : base(message)
        {
        }

        public NotDeclaredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotDeclaredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}