using System;
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
            var queryHandler = _windsorContainer.Resolve<IQueryHandler<TRequest, TResult>>();
            if (queryHandler == null)
                throw new HandlerNotFoundException(typeof(TRequest));

            try
            {
                var response = queryHandler.Handle(request);
                return response;
            }
            finally
            {
                _windsorContainer.Release(queryHandler);
            }
        }
    }
}