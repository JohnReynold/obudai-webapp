using System;
using System.Runtime.Serialization;

namespace obudai_webapp.Controllers
{
    [Serializable]
    internal class BalanceEmptyException : Exception
    {
        public BalanceEmptyException()
        {
        }

        public BalanceEmptyException(string message) : base(message)
        {
        }

        public BalanceEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BalanceEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}