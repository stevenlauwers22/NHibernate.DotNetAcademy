using System;

namespace DotNetAcademy.NhibernateArch.Infrastructure.Handlers.Exceptions
{
    public class HandlerNotFoundException : ApplicationException
    {
        private readonly Type _handlerType;

        public HandlerNotFoundException(Type handlerType)
        {
            _handlerType = handlerType;
        }

        public Type HandlerType
        {
            get { return _handlerType; }
        }
    }
}