using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DiffCheckResponse : CheckResponse
    {
        public List<Difference>? Differences;
    }
    public class CheckResponse
    {
        private ResponseStatus _status;
        public ResponseStatus Status
        {
            private get { return _status; }
            set
            {
                _status = value;
                DiffStatus = _status.ToString();
            }
        }
        public string DiffStatus { get; private set; }
    }
    public enum ResponseStatus
    {
        Equals = 0,
        ContentDoNotMatch = 1,
        SizeDoNotMatch = 2,
        NoId = 3,
        NoMatchingSide = 4,
        ValueNotSet = 5,
    }
}
