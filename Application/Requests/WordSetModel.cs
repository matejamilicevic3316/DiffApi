using Application.Commands;
using Application.Commands.Enums.Difference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class WordSetModel : WordSetRequest
    {
        public int Id { get; set; }
        public HorziontalCheckSide? CheckSide { get; set; }
    }

    public class WordSetRequest : BaseModel
    {
        public string Value { get; set; }
    }
}
