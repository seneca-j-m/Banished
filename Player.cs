namespace BanishedMain;

public class Player
{
    // database data
    public int userid { get; }
    public string playername { get; }
    public string playerrace { get; }
    public string playerclass { get; }

    public Player(int _userid, string _playername, string _playerrace, string _playerclass)
    {
        userid = _userid;
        playername = _playername;
        playerrace = _playerrace;
        playerclass = _playerclass;
    }

    public Player()
    {
        userid = -1;
        playername = "";
        playerrace = "";
        playerclass = "";

    }
}


// player enums

public enum PlayerRaceList
{
    ELF,
    HUMAN,
    ORC,
    
}

public enum PlayerClassList
{
    KNIGHT,
    SORCERER,
    WARLOCK
}

public class PlayerManager
{
    internal bool VerifyPlayer(DBManager DB)
    {
        if (!DB.GetPlayerDBData().Any())
        {
            Sys.WSMNL("SYS: NO PLAYER DATA DETECTED!");
            Sys.WSMNL("GENERATING NEW PLAYER ARCHITECTURE");
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

    internal Player SetupPlayer(User user)
    {
        CosmeticMenu.writeTitleCosmetics("player creation menu");
        Sys.WSMNL("Enter Player Name ('playername'): ");
        string playerName = Console.ReadLine();

        // RACE // 
        bool playerRaceMenuQuit = false;

        string playerRace = "";
        
        while (!playerRaceMenuQuit)
        {
            Sys.WSMNL("Enter Player Race ('playerrace') [? for help]: ");
            playerRace = Console.ReadLine();
            // verify input 
            switch (playerRace)
            {
                case "?":
                    RaceHelp.RaceOptions();
                    break;
                case "ELF":
                case "elf":
                    playerRaceMenuQuit = true;
                    break;
                case "HUMAN":
                case "human":
                    playerRaceMenuQuit = true;
                    break;
                case "ORC":
                case "orc":
                    playerRaceMenuQuit = true;
                    break;
                default:
                    Error.WEMNL("No valid race input detected!");
                    break;
            }
        }
        // CLASS // 
        bool playerClassMenuQuit = false;

        string playerClass = "";
        
        while (!playerClassMenuQuit)
        {
            Sys.WSMNL("Enter Player Class ('playerclass') [? for help]: ");
            playerClass = Console.ReadLine();

            switch (playerClass)
            {
                case "?":
                    ClassHelp.ClassOptions();
                    break;
                case "KNIGHT":
                case "knight":
                    playerClassMenuQuit = true;
                    break;
                case "SORCERER":
                case "sorcerer":
                    playerClassMenuQuit = true;
                    break;
                case "WARLOCK":
                case "warlock":
                    playerClassMenuQuit = true;
                    break;
                default:
                    Error.WEMNL("No valid class input detected!");
                    break;
            }
        }
        
        
        
        Sys.WSMNL("Adding default attributes...");
        ///////////////////////////////////////////////////////////

        Debug.WDMNL("Data Collated!");
        Sys.WSMDNL("Saving...");

        Player player = new Player(user.userid, playerName, playerRace, playerClass);


        return player;
    }
    
    internal void SavePlayer(DBManager DB, Player player)
    {
        DB.WriteToPlayerDB(player);
    }

    internal Player PlayerLogin(DBManager DB, int userid)
    {
        Player player = new Player();
        
        bool loginValid = false;

        while (!loginValid)
        {
            CosmeticMenu.writeTitleCosmetics("PLAYER SELECTION");
            Sys.WSMNL("Verifying current players...");
            Sys.WSMNL("displaying...");

            Sys.WSMNL("Players:");
            
            List<string> playerData = DB.GetPlayerDBData();
            List<string> playerDataByUserId = new List<string>();

            // remove database entry data
            for (int i = 0; i < playerData.Count(); i++)
            {
                int listTotal = playerData.Count();
                int indexToRemove = listTotal / 5; 
                
                playerData.RemoveAt(indexToRemove);
            }
            
            foreach (var VARIABLE in playerData)
            {
                Console.WriteLine(VARIABLE);
            }

            for (int i = 0; i < playerData.Count(); i++)
            {

                // see if input is parsable
                int playerItemBoundToUserid;
                try
                {
                    playerItemBoundToUserid = int.Parse(playerData[i]);
                    // if the item = the user id, add the 'name' (one index past) to the player data
                    if (playerItemBoundToUserid == userid) // this is inefficent
                    {
                        int playerNameIndex = i + 1; //TODO: FIX THIS ADDING USER ID AS WELL!!
                        break;
                        playerDataByUserId.Add(playerData[playerNameIndex]);
                    }
                }
                catch (Exception e)
                {} // catch error
            }
            
            bool userSelectionValid = false;

            while (!userSelectionValid)
            {
                // print player names for that user
                uint count = 1;
                foreach (var playerEntry in playerDataByUserId)
                {
                    Debug.WDMNL($"{count}. {playerEntry}");
                    count++;
                }
            
                // prompt user
                Sys.WSMNL("Select Player By Number: ");
                try
                {
                    int userPlayerSelection = int.Parse(Console.ReadLine());

                    // make player from working backwards
                    // get first easiest data
                    string userPlayername = playerDataByUserId[userPlayerSelection];

                    int userPlayerUseridLocation = (playerData.IndexOf(userPlayername)) - 1;
                    int userPlayerUserid = int.Parse((playerData[userPlayerUseridLocation]));
                    
                    int userPlayerraceLocation = (playerData.IndexOf(userPlayername) + 1);
                    string userPlayerrace = playerData[userPlayerraceLocation];

                    int userPlayerclassLocation = (playerData.IndexOf(userPlayername) + 2);
                    string userPlayerclass = playerData[userPlayerclassLocation];
                    
                    // create player
                    player = new Player(userPlayerUserid, userPlayername, userPlayerrace, userPlayerclass);
                    
                    Sys.WSMNL("LOGIN SUCCESFUL!");
                    
                    userSelectionValid = true;
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT!");
                }
            }
        }
        return player;
    }
}