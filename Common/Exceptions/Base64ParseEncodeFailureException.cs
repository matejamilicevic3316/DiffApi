using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class Base64ParseEncodeFailureException : Exception
    {
        public Base64ParseEncodeFailureException(string? message) : base(message)
        {
        }
    }
}
