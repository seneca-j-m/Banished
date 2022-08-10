using System.Net.Http.Headers;

namespace External;

public class User
{
    public string firstname { get; }
    public string lastname { get; }
    public int age { get; }

    User(string _firstname, string _lastname, int _age)
    {
        firstname = _firstname;
        lastname = _lastname;
        age = _age;
    }
}

public class UserManager
{
    internal bool VerifyUser(DBManager DB)
    {
        if (!DB.GetUserDBData().Any())
        {
            Sys.WSMNL("SYS: NO USER DATA DETECTED!");
            Sys.WSMNL("GENERATING NEW USER ARCHITECTURE");
            Sys.WSMDNL("ENTERING SETUP...");
            Console.Out.Flush();
            return false;
        }
        else
        {
            return true;   
        }
    }
    internal void SetupUser()
    {
        CosmeticMenu.writeTitleCosmetics("user creation menu");
        Sys.WSMNL("Enter User Firstname ('firstname'): ");
        string userFirstName = Console.ReadLine();
        Sys.WSMNL("Enter User Lastname ('lastname'): ");
        string userLastName = Console.ReadLine();
        Sys.WSMNL("Enter User Age ('age'): ");
        uint UserAge;
        while (!uint.TryParse(Console.ReadLine(), out UserAge))
        {
            Error.WEMNL("ERR: INTPUT IS NOT VALID!");
        }
        Debug.WDMNL("Data Collated!");
        Sys.WSMDNL("Saving...");
        
    }

    internal void SaveUser()
    {
        
    }
}