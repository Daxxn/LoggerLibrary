using LoggerLibrary.Models;

namespace LoggerLibrary;

/// <summary>
/// <see cref="LoggerBase"/> implementation for a logger that will send messages to the <see cref="Console"/>.
/// </summary>
public class ConsoleLogger : LoggerBase
{
   #region Constructors
   /// <summary>
   /// Creates a new <see cref="ConsoleLogger"/>.
   /// </summary>
   /// <param name="next">Next logger in the chain.</param>
   public ConsoleLogger(ILogger? next) : base(next) { }
   #endregion

   #region Methods
   protected override void AbstLog(ILog log) => Console.WriteLine(log);

   protected override void AbstSave() => Console.WriteLine("Save Logs");
   #endregion

   #region Full Props

   #endregion
}
