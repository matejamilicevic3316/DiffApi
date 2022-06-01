using Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationMessage> Messages; 
        public ValidationException(List<ValidationMessage> messages) : base()
        {
            Messages = messages;
        }
    }

    public class ValidationMessage
    {
        public string Message { get; set; }
        private ResponseStatus _status;
        public ResponseStatus Status { private get { return _status; }
            set { 
               _status = value;
                ResponseStatus = _status.ToString();
            }
        }
        public string ResponseStatus { get; private set; }
    }
}
