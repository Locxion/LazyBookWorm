using System;
using log4net.Appender;
using log4net.Core;

namespace LazyBookworm
{
    public delegate void MessageLoggedDelegate(Level level, DateTime logTime, string message);

    public class LazyBookwormAppender : IAppender
    {
        public event MessageLoggedDelegate OnMessageLogged;

        public string Name { get; set; } = "LazyBookwormAppender";

        public void Close()
        {
            // Do nothing
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            OnMessageLogged?.Invoke(loggingEvent.Level, loggingEvent.TimeStamp, loggingEvent.RenderedMessage);
        }
    }
}