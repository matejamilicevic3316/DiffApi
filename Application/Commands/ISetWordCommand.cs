using Application.Commands.BaseCommands;
using Application.Requests;
using Application.Response;

namespace Application.Commands
{
    public interface ISetWordCommand : ICommand<WordSetModel, EmptyResponse>
    {
    }
}
