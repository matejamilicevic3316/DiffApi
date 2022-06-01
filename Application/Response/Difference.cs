using Application.Commands.Enums.Difference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record Difference
    {
        public Difference(string leftWord, string rightWord, int offset, int errorLength)
        {
            LeftLettersDifference = new LetterDifference
            {
                Side = HorziontalCheckSide.Left.ToString(),
                DifferentLetters = leftWord.Substring(offset, errorLength)
            };
            RightLettersDifference = new LetterDifference
            {
                Side = HorziontalCheckSide.Right.ToString(),
                DifferentLetters = rightWord.Substring(offset, errorLength)
            };
            Offset = offset;
            Length = errorLength;
        }

        public LetterDifference LeftLettersDifference { get; private set; }
        public LetterDifference RightLettersDifference { get; private set; }
        public int Offset { get; private set; }
        public int Length { get; private set; }

        public class LetterDifference
        {
            public string Side { get; internal set; }
            public string DifferentLetters { get; internal set; } = string.Empty;
        }
    }
}
