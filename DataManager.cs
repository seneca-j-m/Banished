using Microsoft.Data.Sqlite;
namespace External;
public class DataManager
{
    internal void InitFilesystem()
    {
        // verify directories
        try
        {
            // create user directory (dont need to check if it does or doesnt exist)
            Directory.CreateDirectory("../../../User");
            
            // create user db
            //using (FileStream fs = File.Create(userDBPath)) ;
            FileStream fs = File.Create(GDirectories.userDBPath);
            fs.Close();
        }
        catch (Exception e)
        {
            throw new SaveFileCreationFailed(e);
        }
    }

    internal void InititDB()
    {
        DBManager DBM = new DBManager();

        try
        {
            DBM.ConnectToDB();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
    }
}

public class DBManager
{
    //protected SqliteConnection DBConnection = new SqliteConnection(GDirectories.userDBPath);
    internal DBManager()
    {
    }

    ~DBManager() // just in caseys
    {
        //DBConnection.Close();
    }

    internal void ConnectToDB()
    {
        using (var connection = new SqliteConnection(@"Data Source=../../../User/user.db"))
        {
            Console.WriteLine("Yay!");
        }
    }

    internal void GetDBDate()
    {
        
    }
}

public static class GDirectories
{
    public static string userPath = @"../../../User/";
    public static string userDBPath = @"../../../User/user.db";
}