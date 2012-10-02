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
            var commandHandler = _windsorContainer.Resolve<ICommandHandler<TCommand>>();
            if (commandHandler == null)
                throw new HandlerNotFoundException(typeof(TCommand));

            commandHandler.Handle(command);
        }

        public TResult Dispatch<TRequest, TResult>(TRequest request)
        {
            var queryHandler = _windsorContainer.Resolve<IQueryHandler<TRequest, TResult>>();
            if (queryHandler == null)
                throw new HandlerNotFoundException(typeof(TRequest));

            var response = queryHandler.Handle(request);
            return response;
        }
    }
}