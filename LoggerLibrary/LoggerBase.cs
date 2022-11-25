using LoggerLibrary.Models;

namespace LoggerLibrary;

/// <summary>
/// Base Logger class for derriving loggers.
/// </summary>
public abstract class LoggerBase : ILogger
{
   #region Local Props
   /// <summary>
   /// <see cref="ILog"/>s Buffer.
   /// </summary>
   public List<ILog> Logs { get; private set; } = new();

   /// <summary>
   /// Next logger in the chain.
   /// <see langword="null"/> if end of chain.
   /// </summary>
   protected ILogger? Next { get; init; }
   #endregion

   #region Constructors
   /// <summary>
   /// Construct logger and append next logger in chain.
   /// <para>
   /// See <see href="https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern">Chain of Responibility Pattern</see>
   /// </para>
   /// </summary>
   /// <param name="next">Next logger in chain.</param>
   public LoggerBase(ILogger? next) => Next = next;

   /// <summary>
   /// Serialize logs as a binary stream to reduce file size.
   /// </summary>
   /// <returns>Stream of serialized bytes</returns>
   public byte[] SerializeLogs()
   {
      List<byte> output = new()
      {
         29,
      };
      foreach (var log in Logs)
      {
         output.AddRange(log.Serialize());
         output.Add((byte)'\n');
      }

      output.Add(29);
      return output.ToArray();
   }

   /// <summary>
   /// Saves the logs.
   /// <para>
   /// Do nothing if saving is not required.
   /// </para>
   /// </summary>
   public void Save()
   {
      AbstSave();
      Next?.Save();
   }

   /// <summary>
   /// Add a <see cref="ILog"/> to the buffer.
   /// </summary>
   /// <param name="log"><see cref="ILog"/> to add.</param>
   public void Log(ILog log)
   {
      AbstLog(log);
      Next?.Log(log);
   }

   /// <summary>
   /// When overriden in a derived class, defines what the logger will do when an <see cref="ILog"/> is generated.
   /// </summary>
   /// <param name="log"><see cref="ILog"/> to pass down the chain.</param>
   protected abstract void AbstLog(ILog log);

   /// <summary>
   /// When overriden in a derived class, defines what the logger will do when the log buffer is saved.
   /// </summary>
   protected abstract void AbstSave();
   #endregion

   #region Methods

   #endregion

   #region Full Props

   #endregion
}