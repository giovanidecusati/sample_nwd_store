using System;

namespace BuildingBlock.Core.DomainEvents.Events
{
    internal class RaiseEventException : Exception
    {
        public RaiseEventException()
        {
        }

        public RaiseEventException(string message)
            : base(message)
        {
        }

        public RaiseEventException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
