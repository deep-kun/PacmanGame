using System;

namespace PacManLibrary.Interfaces
{
    public interface IEventSink
    {
        void Publish<TEvent>(TEvent value);
        void Subscribe<TEvent>(Action<TEvent> action);
        void Unsubscribe<TEvent>(Action<TEvent> action);
    }
}
