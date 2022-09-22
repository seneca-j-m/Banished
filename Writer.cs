namespace BanishedMain;

/// <summary>
/// Encompassing Write Class for Debug 
/// </summary>
public static class Debug
{
    /// <summary>
    /// Write Debug Message to console.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WDM(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{message.ToUpper()}"); 
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Debug Message New Line to console.
    /// Writes message to console with newline.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WDMNL(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{message.ToUpper()}");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Warning Message Delay to console.
    /// Writes each character in message given multiplyed by delay value.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WDMD(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Debug Message Delay New Line to console.
    /// Writes each character in message given multiplyed by delay value with new line character.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WDMDNL(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.WriteLine(); // add newline character
        Console.ResetColor();
    }
}

/// <summary>
/// Encompassing Write Class for Warn
/// </summary>
public static class Warn
{
    /// <summary>
    /// Write Warning Message to console.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WWM(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{message.ToUpper()}");
        Console.ResetColor();
    }

    /// <summary>
    /// Write Warning Message New Line to console.
    /// Writes message to console with newline.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WWMNL(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{message.ToUpper()}");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Warning Message Delay to console.
    /// Writes each character in message given multiplyed by delay value.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WWMD(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Warning Message Delay New Line to console.
    /// Writes each character in message given multiplyed by delay value with new line character.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WWMDNL(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.WriteLine(); // add newline character
        Console.ResetColor();
    }
}

/// <summary>
/// Encompassing Write Class for Error
/// </summary>
public static class Error
{
    /// <summary>
    /// Write Error Message to console.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WEM(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{message.ToUpper()}");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Error Message New Line to console.
    /// Writes message to console with newline.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WEMNL(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{message.ToUpper()}");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Error Message Delay to console.
    /// Writes each character in message given multiplyed by delay value.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WEMD(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Error Message Delay New Line to console.
    /// Writes each character in message given multiplyed by delay value with new line character.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WEMDNL(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.WriteLine(); // add newline character
        Console.ResetColor();
    }
}

/// <summary>
/// Encompassing Write Class for Sys
/// </summary>
public static class Sys
{
    /// <summary>
    /// Write Sys Message to console.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WSM(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{message.ToUpper()}");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Sys Message New Line to console.
    /// Writes message to console with newline.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    public static void WSMNL(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{message.ToUpper()}");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Write Sys Message Delay to console.
    /// Writes each character in message given multiplyed by delay value.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WSMD(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.ResetColor();
    }
    
    
    /// <summary>
    /// Write Sys Message Delay New Line to console.
    /// Writes each character in message given multiplyed by delay value with new line character.
    /// </summary>
    /// <param name="message">Message to be written.</param>
    /// <param name="delay">Value to delay by (in ms).</param>
    public static async void WSMDNL(string message, int delay = 250)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char c in message)
        {
            Console.Write(c);
            await Task.Delay(250);
        }
        Console.WriteLine(); // add newline character
        Console.ResetColor();
    }
}
