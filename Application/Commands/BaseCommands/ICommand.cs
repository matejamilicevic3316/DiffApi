namespace Application.Commands
{
    public interface ICommand<TReq,TRes>
        where TReq : BaseModel
        where TRes : class
    {
        public TRes Execute(TReq req);
    }
}
