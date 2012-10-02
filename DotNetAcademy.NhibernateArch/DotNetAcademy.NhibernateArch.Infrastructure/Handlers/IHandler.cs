namespace DotNetAcademy.NhibernateArch.Infrastructure.Handlers
{
    public interface IHandler
    {
    }

    public interface ICommandHandler<in TCommand> : IHandler
    {
        void Handle(TCommand command);
    }

    public interface IQueryHandler<in TRequest, out TResult> : IHandler
    {
        TResult Handle(TRequest request);
    }
}