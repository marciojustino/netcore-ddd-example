using Microsoft.Extensions.Logging;

namespace DDDExample.Application.Providers
{
    public class ApplicationLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ApplicationLogger(categoryName);
        }

        public void Dispose()
        {

        }
    }
}
