namespace DotNetAcademy.NhibernateArch.Infrastructure.Handlers
{
    public interface IDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
        TResult Dispatch<TRequest, TResult>(TRequest request);
    }
}