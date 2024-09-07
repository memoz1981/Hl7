
namespace Hl7.App.Utilities;

public class FileLogger : IFileLogger
{
    private const string LOG_FILE_DIRECTORY_KEY = "LogFileDirectory"; 
    private readonly string _logFileDirectory;

    public FileLogger(IConfiguration configuration)
    {
        _logFileDirectory = configuration.GetValue<string>(LOG_FILE_DIRECTORY_KEY);
    }

    public async Task Log(Exception ex)
    {
        try
        {
            var dateTime = DateTime.UtcNow.ToString("yyyy-MMM-dd hh-mm-ss-fff");

            if(!Directory.Exists(_logFileDirectory))
                Directory.CreateDirectory(_logFileDirectory);

            var filePath = $"{_logFileDirectory}\\{dateTime}.txt";

            await File.WriteAllTextAsync(filePath, ex.ToString());
        }
        catch {}
    }
}
