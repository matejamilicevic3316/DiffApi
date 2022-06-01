using Application.Commands;
using Application.Commands.BaseCommands;
using Application.Commands.Enums.Difference;
using Application.Exceptions;
using Application.Helpers;
using Application.Requests;
using Application.Response;

namespace Application.Implementation
{
    public class SetWordCommand : ISetWordCommand
    {
        private WordStoreAccessor _accessor;
        public SetWordCommand(WordStoreAccessor accessor)
        {
            _accessor = accessor;
        }

        public EmptyResponse Execute(WordSetModel request)
        {
            List<ValidationMessage> messages = new List<ValidationMessage>();
            if (request.Id == 0)
            {
                messages.Add(new ValidationMessage
                {
                    Message = "id must be set",
                    Status = ResponseStatus.NoId
                });
            }
            if (request.CheckSide == null)
            {
                messages.Add(new ValidationMessage
                {
                    Message = "side of the comparing word must be set",
                    Status = ResponseStatus.NoMatchingSide
                });
            }
            if (request.Value == null)
            {
                messages.Add(new ValidationMessage
                {
                    Message = "value must be sed",
                    Status = ResponseStatus.ValueNotSet
                });
            }
            if (messages.Any())
            {
                throw new ValidationException(messages);
            }

            _accessor.SetWord(request.Id, request.CheckSide.Value, request.Value);

            return new EmptyResponse();
        }
    }
}
