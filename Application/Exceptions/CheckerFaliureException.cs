using Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class CheckerFaliureException : Exception
    {
        public DiffCheckResponse Response;
        public CheckerFaliureException(DiffCheckResponse response)
        {
            Response = response;
        }
    }
}
