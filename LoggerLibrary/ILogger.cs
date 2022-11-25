using LoggerLibrary.Models;

namespace LoggerLibrary;

/// <summary>
/// Interface for deriving new loggers from <see cref="LoggerBase"/>.
/// </summary>
public interface ILogger
{
   /// <summary>
   /// <see cref="ILog"/>s Buffer.
   /// </summary>
   List<ILog> Logs { get; }

   /// <summary>
   /// Serialize logs as a binary stream to reduce file size.
   /// </summary>
   /// <returns>Stream of serialized bytes</returns>
   byte[] SerializeLogs();

   /// <summary>
   /// Saves the logs.
   /// <para>
   /// Do nothing if saving is not required.
   /// </para>
   /// </summary>
   void Save();

   /// <summary>
   /// Add a <see cref="ILog"/> to the buffer.
   /// </summary>
   /// <param name="log"><see cref="ILog"/> to add.</param>
   void Log(ILog log);
}
