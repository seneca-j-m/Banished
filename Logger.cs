using System.Linq;

namespace BanishedMain;

public class Logger
{
    private uint logReferenceNumber = 0;

    internal Logger()
    {
    }

    internal void TransferLogs()
    {
        uint logReferenceNumber = 0;

        // get log data from previous file
        using (StreamReader sr = new StreamReader(GDirectories.loggerFPath))
        {
            string logVersionLine = sr.ReadLine();
            string[] logVersionLineArr = logVersionLine.Split(':');
            uint logVersion = uint.Parse(logVersionLineArr[1]);
            logReferenceNumber = (logVersion += 1);
        }

        // set log backup path
        string logBackupFile = Path.Join(GDirectories.loggerBPath, $@"/log{logReferenceNumber.ToString()}.log");

        File.Move(GDirectories.loggerFPath, logBackupFile);
    }

    internal void TemplateLog()
    {
        using (StreamWriter sw = new StreamWriter(GDirectories.loggerFPath))
        {
            sw.WriteLine($"LOG VERSION:{logReferenceNumber}");
            sw.WriteLine("INITAL LOG SUCCESS __ >");
            sw.WriteLine("LOG CLASS INSTANTIATED __ >");
            sw.WriteLine("PREVIOUS CONTENTS WIPED!");
            sw.WriteLine($"BACKUPS SAVED TO {GDirectories.loggerBFPath}");
        }
    }

    internal void Log(Exception e, bool verbose = true)
    {
    }

    internal void Log(string message, bool verbose = false)
    {
    }

    internal void DumpLog()
    {
    }
}