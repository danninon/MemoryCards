public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;

    public FileLoggerProvider(string filePath)
    {
        _filePath = filePath;

        // Ensure the log directory exists
        var logDirectory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_filePath);
    }

    public void Dispose() { }
}

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly object _lock = new object();

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        lock (_lock)
        {
            File.AppendAllText(_filePath, $"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")} [{logLevel}] {formatter(state, exception)}{Environment.NewLine}");
        }
    }
}
