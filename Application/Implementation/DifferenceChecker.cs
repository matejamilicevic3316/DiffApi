using Application.Commands;
using Application.Commands.Enums.Difference;
using Application.Exceptions;
using Application.Helpers;
using Common.Extensions;

namespace Application.Implementation
{
    public class DifferenceChecker : IDisposable, IDifferenceCheckerCommand
    {
        private WordStoreAccessor _accessor;
        public DifferenceChecker(WordStoreAccessor accessor)
        {
            _accessor = accessor;
        }

        private List<Difference> _differences { get; set; } = new List<Difference>();

        public CheckResponse Execute(DiffCheckModel request)
        {
            var response = new CheckResponse { Status = ResponseStatus.Equals };

            var leftWord = _accessor.GetWord(request.Id, HorziontalCheckSide.Left);
            var rightWord = _accessor.GetWord(request.Id, HorziontalCheckSide.Right);

            if (leftWord == null || rightWord == null)
            {
                var validationErrorList = new List<ValidationMessage>();

                if (leftWord == null)
                {
                    validationErrorList.Add(new ValidationMessage() { Message = "Left word doesn't exist", Status = ResponseStatus.NoMatchingSide });
                }

                if (rightWord == null)
                {
                    validationErrorList.Add(new ValidationMessage() { Message = "Right word doesn't exist", Status = ResponseStatus.NoMatchingSide });
                }

                throw new ValidationException(validationErrorList);
            }

            if (leftWord.Length != rightWord.Length)
            {
                return new CheckResponse { Status = ResponseStatus.SizeDoNotMatch };
            }

            CheckDifferences(leftWord, rightWord);

            if (_differences.Any())
            {
                response = new DiffCheckResponse
                {
                    Status = ResponseStatus.ContentDoNotMatch,
                    Differences = _differences.ToList()
                };
            }

            return response;
        }

        public void Dispose()
        {
            _differences = new List<Difference>();
        }

        private void CheckDifferences(string leftWord, string rightWord)
        {
            int differentLetterLength = 0;

            try
            {
                for (var i = 0; i < rightWord.Length; i++)
                {
                    if (leftWord[i] == rightWord[i] && differentLetterLength > 0)
                    {
                        _differences.Add(new Difference(leftWord, rightWord, i-1, differentLetterLength));

                        differentLetterLength = 0;
                    }

                    if (leftWord[i] != rightWord[i]) 
                    { 
                        differentLetterLength ++;
                    }
                }
            }
            finally
            {
                if (differentLetterLength > 0)
                {
                    _differences.Add(new Difference(leftWord, rightWord, leftWord.ReverseCharIndex(differentLetterLength), differentLetterLength));
                }
            }
        }
    }
}
