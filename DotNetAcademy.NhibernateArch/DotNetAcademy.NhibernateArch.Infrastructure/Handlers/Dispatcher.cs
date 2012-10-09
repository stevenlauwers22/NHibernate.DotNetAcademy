using Castle.Windsor;
using DotNetAcademy.NhibernateArch.Infrastructure.Handlers.Exceptions;

namespace DotNetAcademy.NhibernateArch.Infrastructure.Handlers
{
    public class Dispatcher : IDispatcher
    {
        private readonly IWindsorContainer _windsorContainer;

        public Dispatcher(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            var commandHandlerType = typeof(ICommandHandler<TCommand>);
            var commandHandlerIsRegistered = _windsorContainer.Kernel.HasComponent(commandHandlerType);
            if (!commandHandlerIsRegistered)
            {
                throw new HandlerNotFoundException(commandHandlerType);
            }

            var commandHandler = (ICommandHandler<TCommand>) _windsorContainer.Resolve(commandHandlerType);
            try
            {
                commandHandler.Handle(command);
            }
            finally
            {
                _windsorContainer.Release(commandHandler);
            }
        }

        public TResult Dispatch<TRequest, TResult>(TRequest request)
        {
            var queryHandlerType = typeof(IQueryHandler<TRequest, TResult>);
            var queryHandlerIsRegistered = _windsorContainer.Kernel.HasComponent(queryHandlerType);
            if (!queryHandlerIsRegistered)
            {
                throw new HandlerNotFoundException(queryHandlerType);
            }

            var queryHandler = (IQueryHandler<TRequest, TResult>)_windsorContainer.Resolve(queryHandlerType);
            try
            {
                var result = queryHandler.Handle(request);
                return result;
            }
            finally
            {
                _windsorContainer.Release(queryHandler);
            }
        }
    }
}