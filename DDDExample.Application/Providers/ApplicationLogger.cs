namespace DDDExample.Api.Providers
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;

    /// <summary>
    /// Customização de log de console.
    /// </summary>
    public class ApplicationLogger : ILogger
    {
        private readonly string _categoryName;
        private static object _MessageLock = new object();

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ApplicationLogger(string categoryName)
        {
            _categoryName = categoryName;
        }

        /// <summary>
        /// Início de escopo
        /// </summary>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Verificar se o log está ativo
        /// </summary>
        /// <param name="logLevel">Nível de log</param>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Efetuar log de operação
        /// </summary>
        /// <param name="logLevel">Nível do log</param>
        /// <param name="eventId">Identificador do evento</param>
        /// <param name="state">Conteúdo do log</param>
        /// <param name="exception">Exceção capturada</param>
        /// <param name="formatter">Formatador do conteúdo</param>
        /// <typeparam name="TState">Tipo do conteúdo</typeparam>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (_MessageLock)
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(DateTimeOffset.Now.ToString("O"));

                PrintDivider();

                Console.ForegroundColor = logLevel == LogLevel.Information
                ? ConsoleColor.Green
                : logLevel == LogLevel.Warning
                    ? ConsoleColor.Yellow
                    : logLevel == LogLevel.Error
                        ? ConsoleColor.DarkRed
                        : logLevel == LogLevel.Critical
                            ? ConsoleColor.Magenta
                            : ConsoleColor.Blue;

                Console.Write(logLevel);

                PrintDivider();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(_categoryName);

                PrintDivider();
                Console.ResetColor();
                Console.Write(Thread.CurrentThread.ManagedThreadId);

                PrintDivider();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(formatter(state, exception));
            }
        }

        private static void PrintDivider()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("|");
        }
    }
}