namespace Application.Commands.BaseCommands
{
    public interface INoResCommand<TReq>
        where TReq : class
    {
        void Execute(TReq req);
    }
}
