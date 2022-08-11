using System.Net.Http.Headers;

namespace BanishedMain;

public class User
{
    public string firstname { get; }
    public string lastname { get; }
    public uint age { get; }

    public string password { get; }

    public User(string _firstname, string _lastname, uint _age, string _password)
    {
        firstname = _firstname;
        lastname = _lastname;
        age = _age;
        password = _password;
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
            //Sys.WSMDNL("ENTERING SETUP..."); // not sure async is working correctly
            Sys.WSMNL("ENTERING SETUP...");
            Console.Out.Flush();
            return false;
        }
        else
        {
            return true;
        }
    }

    internal User SetupUser()
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

        Sys.WSMNL("Enter User Password ('password')");
        string userPassword = Console.ReadLine();

        Debug.WDMNL("Data Collated!");
        Sys.WSMDNL("Saving...");

        User newUser = new User(userFirstName, userLastName, UserAge, userPassword);

        return newUser;
    }

    internal void SaveUser(DBManager DB, User user)
    {
        DB.WriteToUserDB(user);
    }

    internal void UserLogin(DBManager DB)
    {
        bool loginValid = false;

        while (!loginValid)
        {
            CosmeticMenu.writeTitleCosmetics("USER LOGIN");
            Sys.WSMNL("FIRSTNAME: ");
            string userFirstName = Console.ReadLine();
            Sys.WSMNL("LASTNAME: ");
            string userLastname = Console.ReadLine();
            Sys.WSMNL("PASSWORD: ");
            string userPassword = Console.ReadLine();

            // check data against database
            List<string> userDbData = DB.GetUserDBData();

            if (userDbData.IndexOf(userFirstName) == -1 || userDbData.IndexOf(userLastname) == -1 ||
                userDbData.IndexOf(userPassword) == -1)
            {
                Warn.WWMNL("ERR: LOGIN INVALID!");
            }
            else
            {
                Sys.WSMNL("SUCC: LOGIN VALID!");
                break;
            }
        }
    }

    internal User GetUser(DBManager DB)
    {
        List<string> userDBData = DB.GetUserDBData();

        // make user ADJUST FOR NUMEROUS USERS
        User user = new User(userDBData[0], userDBData[1], uint.Parse(userDBData[2]), userDBData[3]);

        return user;
    }
}