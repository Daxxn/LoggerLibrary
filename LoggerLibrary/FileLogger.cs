using LoggerLibrary.Models;

namespace LoggerLibrary;

/// <summary>
/// <see cref="LoggerBase"/> implementation for saving <see cref="ILog"/>s to a file.
/// </summary>
public class FileLogger : LoggerBase
{
   #region Local Props
   /// <summary>
   /// Log file save path
   /// </summary>
   public string SavePath { get; set; }
   /// <summary>
   /// Maximum line count before the old logs are dropped from the log file.
   /// </summary>
   public static long MaxFileSize { get; set; } = 100;
   #endregion

   #region Constructors
   /// <summary>
   /// Create a new <see cref="FileLogger"/>.
   /// </summary>
   /// <param name="next">Next logger in the chain.</param>
   /// <param name="savePath">Log file save path.</param>
   public FileLogger(ILogger? next, string savePath) : base(next) => SavePath = savePath;
   /// <summary>
   /// Create a new <see cref="FileLogger"/>.
   /// </summary>
   /// <param name="next">Next logger in the chain.</param>
   /// <param name="savePath">Log file save path.</param>
   /// <param name="maxFileSize">Maximum line count of the log file.</param>
   public FileLogger(ILogger? next, string savePath, long maxFileSize) : base(next)
   {
      SavePath = savePath;
      MaxFileSize = maxFileSize;
   }
   #endregion

   #region Methods
   /// <summary>
   /// Shortens the log file to keep the size of the file from becoming too large.
   /// </summary>
   /// <returns></returns>
   private List<string> ShortenFile()
   {
      using StreamReader reader = new(SavePath);
      List<string> lines = new();
      long currentReadLength = 0;
      while (!reader.EndOfStream)
      {
         var line = reader.ReadLine();
         currentReadLength++;
         if (line == null)
            continue;
         lines.Add(line);
      }
      if (currentReadLength + Logs.Count > MaxFileSize)
      {
         lines.RemoveRange(0, (lines.Count - (int)MaxFileSize) + Logs.Count);
      }
      return lines;
   }

   protected override void AbstSave()
   {
      List<string> lines = new();
      if (File.Exists(SavePath))
      {
         lines = ShortenFile();
      }
      lines.AddRange(Logs.ConvertAll<string>((log) => log.ToString() ?? ""));
      File.WriteAllLines(SavePath, lines);
   }

   protected override void AbstLog(ILog log) => Logs.Add(log);
   #endregion
}
