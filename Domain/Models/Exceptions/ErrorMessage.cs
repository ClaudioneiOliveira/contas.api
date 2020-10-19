using System.Collections.Generic;
using contas.api.Domain.Models.Exceptions;

namespace contas.api.Domain.Models.Exceptions
{
    public class ErrorMessage
    {
        public const string DefaultMessage = "Undefined Error";

        public ErrorMessage() : this(DefaultMessage)
        {
        }

        public ErrorMessage(string message)
        {
            Message = message;
            Code = -1;
            Category = Category.Undefined;
        }

        public ErrorMessage(string message, Category category) : this(message)
        {
            Code = -1;
            Category = category;
        }

        public ErrorMessage(string message, long code, Category category) : this(message, category)
        {
            Code = code;
        }

        public IEnumerable<string> Fields { get; set; }
        public long Code { get; set; }
        public string Message { get; set; }
        public Category Category { get; set; }
    }
}