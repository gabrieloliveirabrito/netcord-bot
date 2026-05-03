using NetCord.Logging;

namespace NetCordBot.Features.Logging;

public class BotLogger(bool isDebug) : IRestLogger
{
    private static readonly Serilog.ILogger _log = Serilog.Log.ForContext<BotLogger>();

    public bool IsEnabled(NetCord.Logging.LogLevel logLevel)
    {
        if (isDebug)
        {
            return true;
        }

        return logLevel switch
        {
            NetCord.Logging.LogLevel.None => false,
            NetCord.Logging.LogLevel.Debug => false,
            NetCord.Logging.LogLevel.Trace => false,
            _ => true
        };
    }

    public void Log<TState>(NetCord.Logging.LogLevel logLevel, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);

        switch (logLevel)
        {
            case NetCord.Logging.LogLevel.Trace:
                _log.Verbose(exception, message);
                return;

            case NetCord.Logging.LogLevel.Debug:
                _log.Debug(exception, message);
                return;

            case NetCord.Logging.LogLevel.Information:
                _log.Information(exception, message);
                return;

            case NetCord.Logging.LogLevel.Warning:
                _log.Warning(exception, message);
                return;

            case NetCord.Logging.LogLevel.Error:
                _log.Error(exception, message);
                return;

            case NetCord.Logging.LogLevel.Critical:
                _log.Fatal(exception, message);
                return;
        }
    }
}