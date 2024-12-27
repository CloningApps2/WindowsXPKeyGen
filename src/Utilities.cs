using Newtonsoft.Json;
using System;
using System.IO;

class Config
{
    public ProductKeySettings ProductKeySettings { get; set; }
    public LogSettings LogSettings { get; set; }
    public RetrySettings RetrySettings { get; set; }
    public ErrorHandling ErrorHandling { get; set; }
    public UserSettings UserSettings { get; set; }
    public SystemSettings SystemSettings { get; set; }
    public DebugSettings DebugSettings { get; set; }

    public static Config LoadConfig(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Config>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading config: " + ex.Message);
            return null;
        }
    }
}

public class ProductKeySettings
{
    public string DefaultKey { get; set; }
    public bool ValidateKeyFormat { get; set; }
    public int KeyRetryAttempts { get; set; }
}

public class LogSettings
{
    public bool EnableLogging { get; set; }
    public string LogFilePath { get; set; }
    public string LogLevel { get; set; }
    public int MaxLogFileSizeMB { get; set; }
    public bool EnableLogRotation { get; set; }
    public int LogRetentionDays { get; set; }
}

public class RetrySettings
{
    public int MaxRetryAttempts { get; set; }
    public int RetryDelaySeconds { get; set; }
}

public class ErrorHandling
{
    public bool RetryOnError { get; set; }
    public int MaxErrorCount { get; set; }
    public string ErrorLogLevel { get; set; }
}

public class UserSettings
{
    public bool AskForConfirmation { get; set; }
    public bool AutoExitOnSuccess { get; set; }
    public bool ShowVerboseMessages { get; set; }
}

public class SystemSettings
{
    public bool RegistryBackupEnabled { get; set; }
    public string BackupLocation { get; set; }
    public bool EnableRegistryCheck { get; set; }
    public bool CheckForUpdates { get; set; }
}

public class DebugSettings
{
    public bool EnableDebugMode { get; set; }
    public string DebugLogFilePath { get; set; }
}
