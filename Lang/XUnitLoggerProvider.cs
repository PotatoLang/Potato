namespace Potato;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

public class XUnitLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper _testOutputHelper;

    public XUnitLoggerProvider(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public ILogger CreateLogger(string categoryName) => new XUnitLogger(_testOutputHelper, categoryName);

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

public class XUnitLogger : ILogger
{
    private readonly string _categoryName;
    private readonly ITestOutputHelper _testOutputHelper;

    public XUnitLogger(ITestOutputHelper testOutputHelper, string categoryName)
    {
        _testOutputHelper = testOutputHelper;
        _categoryName = categoryName;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
                            Func<TState, Exception?, string> formatter)
    {
        string message = formatter(state, exception);
        _testOutputHelper.WriteLine(
            $"[{logLevel}] [{_categoryName}] {message}");
        if (exception != null)
        {
            _testOutputHelper.WriteLine(exception.ToString());
        }
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) => null;
}
