namespace BanishedMain;

public class SaveFileCreationFailed : Exception
{
    public SaveFileCreationFailed()
    {
        Error.WEMNL("FATAL: FAILED TO CREATE SAVE FILES");
        Error.WEMDNL("TERMINATING...");
        Environment.Exit(-1);
    }

    public SaveFileCreationFailed(Exception record)
    {
        Error.WEMNL("FATAL: FAILED TO CREATE SAVE FILES");
        Error.WEMNL("EXCEPTION: ");
        Error.WEMNL(record.ToString());
        Error.WEMDNL("TERMINATING...");
        Environment.Exit(-1);
    }
}

public class DatabaseConnectionFailed : Exception
{
    public DatabaseConnectionFailed()
    {
        Error.WEMNL("FATAL: FAILED TO CONNECT TO DATABASE");
        Error.WEMDNL("TERMINATING...");
        Environment.Exit(-1);
    }

    public DatabaseConnectionFailed(Exception record)
    {
        Error.WEMNL("FATAL: FAILED TO CONNECT TO DATABASE");
        Error.WEMNL("EXCEPTION: ");
        Error.WEMNL(record.ToString());
        Error.WEMDNL("TERMINATING...");
        Environment.Exit(-1);
    }
}