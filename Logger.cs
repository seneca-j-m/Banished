namespace External;

public class Logger
{
    internal Logger()
    {
        // first save backup of old file
        
        
        using (StreamWriter write = File.AppendText(GDirectories.loggerFPath))
        {
            write.WriteLine("INITAL LOG SUCCESS __ >");
            write.WriteLine("LOG CLASS INSTATIATED __ >");
            write.WriteLine("PREVIOUS CONTENTS WIPED");
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