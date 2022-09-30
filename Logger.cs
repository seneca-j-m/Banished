using System.Linq;

namespace BanishedMain;

public class Logger
{
    internal Logger()
    {
        uint logReferenceNumber = 0;

        // get log data from previous file
        using (StreamReader sr = new StreamReader(GDirectories.loggerFPath))
        {
            if (new FileInfo(GDirectories.loggerFPath).Length != 0)
            {
                string logVersionLine = sr.ReadLine();
                string[] logVersionLineArr = logVersionLine.Split(':');
                uint logVersion = uint.Parse(logVersionLineArr[1]);
                logReferenceNumber = (logVersion += 1);
            }
        }
        
        // set log backup path
        string logBackupFile = Path.Join(GDirectories.loggerBPath, $@"/log{logReferenceNumber.ToString()}.log");
        
        // // gain data from backup
        // try
        // {
        //     using (StreamWriter write = File.AppendText(logBackupFile))
        //     {
        //         write.WriteLine("\n\n ");
        //     };
        // }
        // catch (Exception e)
        // {
        //     // no backup file ---> move on
        // }

        // save backup of current log file
        File.Move(GDirectories.loggerFPath, logBackupFile);
        
        // write new contents to file
        using (StreamWriter write = File.AppendText(GDirectories.loggerFPath))
        {
            write.WriteLine($"LOG VERSION:{logReferenceNumber}");
            write.WriteLine("INITAL LOG SUCCESS __ >");
            write.WriteLine("LOG CLASS INSTANTIATED __ >");
            write.WriteLine("PREVIOUS CONTENTS WIPED!");
            write.WriteLine($"BACKUPS SAVED TO {GDirectories.loggerBFPath}");
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