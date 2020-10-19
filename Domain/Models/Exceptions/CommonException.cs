using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using contas.api.Domain.Extensions;

namespace contas.api.Domain.Models.Exceptions
{
    [Serializable]
    public class CommonException : Exception
    {
        public const string InnerExceptionSeparator = " --> Inner Exception: ";

        public IEnumerable<ErrorMessage> Errors { get; private set; }

        public CommonException(ErrorMessage errorMessage)
            : base(errorMessage?.Message)
        {
            if (errorMessage == null)
                throw new ArgumentNullException(nameof(errorMessage));

            Errors = new List<ErrorMessage> { errorMessage };
        }

        public CommonException(IEnumerable<ErrorMessage> errorMessages)
            : base(errorMessages?.Select(a => a.Message).AggregateIntoString())
        {
            if (errorMessages == null) throw new ArgumentNullException(nameof(errorMessages));

            Errors = errorMessages.ToList();
        }

        public CommonException(ErrorMessage errorMessage, Exception inner)
            : base(errorMessage?.Message, inner)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

            Errors = new List<ErrorMessage> { errorMessage };
        }

        public CommonException(IEnumerable<ErrorMessage> errorMessages, Exception inner)
            : base(errorMessages?.Select(a => a.Message).AggregateIntoString(), inner)
        {
            if (errorMessages == null) throw new ArgumentNullException(nameof(errorMessages));
            Errors = errorMessages.ToList();
        }

        public CommonException() : base()
        {
            Errors = new List<ErrorMessage> { new ErrorMessage() };
        }

        public CommonException(string message) : base(message)
        {
            Errors = new List<ErrorMessage> { new ErrorMessage(message) };
        }

        public CommonException(string message, Exception innerException) : base(message, innerException)
        {
            Errors = new List<ErrorMessage> { new ErrorMessage(message) };
        }
        [ExcludeFromCodeCoverage]
        protected CommonException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public string GetMessages()
        {
            Exception ex = this;
            var messages = new List<string>();
            do
            {
                messages.Add(ex.Message);
            } while ((ex = ex.InnerException) != null);

            return new StringBuilder().AppendJoin(InnerExceptionSeparator, messages).ToString();
        }
    }
}