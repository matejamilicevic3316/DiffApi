using Application.Commands;

namespace Application.Implementation
{
    public interface IDifferenceCheckerCommand : ICommand<DiffCheckModel,CheckResponse>
    {
    }
}
