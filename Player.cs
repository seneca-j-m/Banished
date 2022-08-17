namespace BanishedMain;

public class Player
{
    // database data
    public string playername { get; }
    public string playerrace { get; }
    public string playerclass { get; }

    // general player data
    public int health { get; set; }

    public Player(string _playername, string _playerrace, string _playerclass, int _health)
    {
        playername = _playername;
        playerrace = _playerrace;
        playerclass = _playerclass;
        health = _health;
    }
}


// player enums

public enum PlayerRaceList
{
    HUMAN,
    ELF,
    ORC,
    
}

public enum PlayerClassList
{
    KNIGHT,
    CLERIC,
    PALADIN,
}

public class PlayerManager
{
    internal bool VerifyPlayer(DBManager DB)
    {
        if (!DB.GetUserDBData().Any())
        {
            Sys.WSMNL("SYS: NO PLAYER DATA DETECTED!");
            Sys.WSMNL("GENERATING NEW USER ARCHITECTURE");
            //Sys.WSMDNL("ENTERING SETUP..."); // not sure async is working correctly
            Sys.WSMNL("ENTERING SETUP...");
            Console.Out.Flush();
            return false;
        }
        else // previous user data exists
        {
            Sys.WSMNL("PLAYER DATA DETECTED!");
            return true;
        }
    }

    internal Player SetupPlayer()
    {
        CosmeticMenu.writeTitleCosmetics("player creation menu");
        Sys.WSMNL("Enter Player Name ('playername'): ");
        string playerName = Console.ReadLine();
        Sys.WSMNL("Enter Player Race ('playerrace') [? for help]: ");
        string playerRace = Console.ReadLine();
        // verify input 
        switch (playerRace)
        {
            case "?":
                ClassHelp.classOptions();
                break;
            default:
                break;
        }
        Sys.WSMNL("Enter Player Class ('playerclass') [? for help]: ");
        string playerClass = Console.ReadLine();
        
        Sys.WSMNL("Adding default attributes...");
        ///////////////////////////////////////////////////////////

        Debug.WDMNL("Data Collated!");
        Sys.WSMDNL("Saving...");

        //Player newPlayer = new Player(playerName, playerRace, playerClass);

        //return newPlayer;

        return new Player("placeholder", "placeholder", "placeholder", 0);
    }

    // TODO: FINISH THIS
    internal void SavePlayer()
    {
        
    }
}