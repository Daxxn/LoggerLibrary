namespace LoggerLibrary.Models;

public interface ILog
{
   LogType Type { get; init; }
   int Severity { get; init; }
   string? Message { get; }
   object? Data { get; init; }

   byte[] Serialize();
}
