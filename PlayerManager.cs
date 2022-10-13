namespace BanishedMain;

public class PlayerManager
{
    internal bool VerifyPlayer(DBManager DB)
    {
        if (!DB.GetPlayerDBData().Any())
        {
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
        Sys.WSMNL("Enter Player Name ('playername') [q/Q for quit]: ");
        Sys.WSM("> ");
        string playerName = Console.ReadLine();
        
        // just a single check is needed here
        if (string.Equals(playerName, "q") || string.Equals(playerName, "Q"))
        {
            Sys.WSMNL("QUITING...");
            Environment.Exit(0);
        }

        // RACE // 
        bool playerRaceMenuQuit = false;

        string playerRace = "";
        
        while (!playerRaceMenuQuit)
        {
            Sys.WSMNL("Enter Player Race ('playerrace') [? for help] [q/Q for quit]: ");
            Sys.WSM("> ");
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
                case "q":
                case "Q":
                    Sys.WSMNL("QUITIING...");
                    Environment.Exit(0); //TODO: RECTIFY THIS
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
            Sys.WSMNL("Enter Player Class ('playerclass') [? for help] [q/Q for quit]: ");
            Sys.WSM("> ");
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
                case "q":
                case "Q":
                    Sys.WSMNL("QUITIING...");
                    Environment.Exit(0); //TODO: RECTIFY THIS
                    break;
                default:
                    Error.WEMNL("No valid class input detected!");
                    break;
            }
        }
        
        // ACOLADE // 
        bool playerAccoladeMenuQuit = false;
        string playerAccolade = "";
        
        while (!playerAccoladeMenuQuit)
        {
            Sys.WSMNL("Enter Player Accolade ('playeraccolade') [? for help] [q/Q for quit]: ");
            Sys.WSM("> ");
            playerAccolade = Console.ReadLine();

            switch (playerAccolade)
            {
                case "?":
                    AccoladeHelp.AccoladeOptions();
                    break;
                case "WARRIOR":
                case "warrior":
                    playerAccoladeMenuQuit = true;
                    break;
                case "SCHOLAR":
                case "scholar":
                    playerAccoladeMenuQuit = true;
                    break;
                case "ACOLYTE":
                case "acolyte":
                    playerAccoladeMenuQuit = true;
                    break;
                case "q":
                case "Q":
                    Sys.WSMNL("QUITIING...");
                    Environment.Exit(0); //TODO: RECTIFY THIS
                    break;
                default:
                    Error.WEMNL("No valid accolade input detected!");
                    break;
            }
        }
        
        
        
        Sys.WSMNL("Adding default attributes...");
        ///////////////////////////////////////////////////////////

        Debug.WDMNL("Data Collated!");

        // parse data as Enum
        Enum.TryParse(playerRace.ToUpper(), out DefaultPlayerRace plRace);
        Enum.TryParse(playerClass.ToUpper(), out DefaultPlayerClass plClass);
        Enum.TryParse(playerAccolade.ToUpper(), out DefaultPlayerAccolade plAccolade);

        Player player = new Player(playerName, plRace, plClass, plAccolade);


        return player;
    }
    
    internal void SavePlayer(DBManager DB, Player player)
    {
        DB.WriteToPlayerDB(player);
    }

    internal void DeletePlayers(DBManager DB)
    {

        Player player; // no need to initialise

        bool deleteSelectionValid = false;

        while (!deleteSelectionValid)
        {
                    
            CosmeticMenu.writeTitleCosmetics("PLAYER DELETION");
            Sys.WSMNL("VERIFYING CURRENT PLAYERS...");
            Sys.WSMNL("SELECT PLAYER FOR DELETION [q/Q FOR QUIT]: ");
            Sys.WSM("\n");
            Sys.WSMNL("PLAYERS: ");
            Sys.WSMNL("\n");

            List<Player> playerData = DB.GetPlayerDBData();
        
            int counter = 0;

            Sys.WSMNL("SELECT PLAYER: ");

            foreach (var playerDat in playerData)
            {
                Sys.WSMNL($"{counter}. {playerDat.playername}");
                counter++;
            }
        
            Sys.WSM("> ");

            string playerSelection = Console.ReadLine();
        
            try
            {
                if (string.Equals(playerSelection, "q") || string.Equals(playerSelection, "Q"))
                {
                    // return to menu
                    Sys.WSMNL("QUITING...");
                    deleteSelectionValid = true;
                }
                else if (playerData.ElementAt(int.Parse(playerSelection)) != null)
                {
                    player = playerData[int.Parse(playerSelection)];
                    string playerName = player.playername;
                    
                    Sys.WSM("PLAYER LOCATED SUCCESFULLY!");

                    bool playerDeletionContinueInputValid = false;

                    while (!playerDeletionContinueInputValid)
                    {
                        Warn.WWMNL("\n\nWARNING: THIS WILL PURGE THE PLAYER CONTINUE [Y/N]");
                        
                        string playerDeletionContinueInput = Console.ReadLine();

                        switch (playerDeletionContinueInput)
                        {
                            case "Y":
                            case "y":
                                DB.DeleteFromPlayerDB(playerName);  
                                Sys.WSMNL($"PLAYER {player.playername} DELETED SUCCESFULLY");
                                playerDeletionContinueInputValid = true;
                                break;
                            case "N":
                            case "n":
                                Sys.WSMNL("ABORTED!");
                                playerDeletionContinueInputValid = true;
                                break;
                            default:
                                Error.WEMNL("NO VALID INPUT!");
                                break;
                        }
                    }

                    deleteSelectionValid = true;
                }
                else
                {
                    Error.WEMNL("NO VALID PLAYER!");
                }
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
            }
        }

        
        
    }

    internal void ListPlayers(DBManager DB)
    {
        List<Player> playerData = DB.GetPlayerDBData();
        
        Sys.WSMNL($"CURRENT PLAYER COUNT: {playerData.Count}");

        int counter = 0;
        foreach (var player in playerData)
        {
            Sys.WSMNL($"{counter}. {player.playername}");
            counter++;
        }
    }

    internal void PurgePlayers(DBManager DB)
    {

        bool purgePlayerContinueMenuInputValid = false;

        while (!purgePlayerContinueMenuInputValid)
        {
            Warn.WWMNL("WARNING: THIS WILL PURGE ALL PLAYER DATA! ARE YOU SURE YOU WANT TO CONTINUE [Y/N]?");
            
            Warn.WWM("> ");

            string purgePlayerContinueMenuInput = Console.ReadLine();

            switch (purgePlayerContinueMenuInput)
            {
                case "Y":
                case "y":
                    DB.PurgePlayerDB();
                    DB.InitPlayerDB(); // remake main table here
                    Sys.WSMNL("ALL PLAYERS PURGED SUCCESFULLY!");
                    purgePlayerContinueMenuInputValid = true;
                    break;
                case "N": 
                case "n":
                    Error.WEMNL("ABORTED!");
                    purgePlayerContinueMenuInputValid = true;
                    break;
                default:
                    Error.WEMNL("NO VALID INPUT!");
                    break;
            }
            
        }
    }

    internal Player PlayerLogin(DBManager DB)
    {
        Player player = new Player();
        
        bool loginValid = false;

        while (!loginValid)
        {
            CosmeticMenu.writeTitleCosmetics("PLAYER SELECTION");
            Sys.WSMNL("Verifying current players...");
            Sys.WSMNL("displaying...");
            Sys.WSMNL("Select Player [q/Q for quit]: ");
            Sys.WSM("\n");
            Sys.WSMNL("Players:");
            Sys.WSM("\n");
            
            
            List<Player> playerData = DB.GetPlayerDBData();

            int counter = 0;

            foreach (var playerDat in playerData)
            {
                Sys.WSMNL($"{counter}. {playerDat.playername}");
                counter++;
            }
            
            Sys.WSM("> ");

            string playerSelection = Console.ReadLine();

            try
            {
                if (string.Equals(playerSelection, "q") || string.Equals(playerSelection, "Q"))
                {
                    // return to menu
                    Sys.WSMNL("QUITING...");
                    loginValid = true;
                }
                else if (playerData.ElementAt(int.Parse(playerSelection)) != null)
                {
                    player = playerData[int.Parse(playerSelection)];
                    loginValid = true;
                }
                else
                {
                    Error.WEMNL("NO VALID PLAYER!");
                }
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
            }


            // List<string> playerDataByUserId = new List<string>();

            //     // remove database entry data
            //     for (int i = 0; i < playerData.Count(); i++)
            //     {
            //         int listTotal = playerData.Count();
            //         int indexToRemove = listTotal / 5; 
            //         
            //         playerData.RemoveAt(indexToRemove);
            //     }
            //     
            //     foreach (var VARIABLE in playerData)
            //     {
            //         Console.WriteLine(VARIABLE);
            //     }
            //
            //     for (int i = 0; i < playerData.Count(); i++)
            //     {
            //
            //         // see if input is parsable
            //         int playerItemBoundToUserid;
            //         try
            //         {
            //             playerItemBoundToUserid = int.Parse(playerData[i]);
            //             // if the item = the user id, add the 'name' (one index past) to the player data
            //             if (playerItemBoundToUserid == userid) // this is inefficent
            //             {
            //                 int playerNameIndex = i + 1; //TODO: FIX THIS ADDING USER ID AS WELL!!
            //                 break;
            //                 playerDataByUserId.Add(playerData[playerNameIndex]);
            //             }
            //         }
            //         catch (Exception e)
            //         {} // catch error
            //     }
            //     
            //     bool userSelectionValid = false;
            //
            //     while (!userSelectionValid)
            //     {
            //         // print player names for that user
            //         uint count = 1;
            //         foreach (var playerEntry in playerDataByUserId)
            //         {
            //             Debug.WDMNL($"{count}. {playerEntry}");
            //             count++;
            //         }
            //     
            //         // prompt user
            //         Sys.WSMNL("Select Player By Number: ");
            //         try
            //         {
            //             int userPlayerSelection = int.Parse(Console.ReadLine());
            //
            //             // make player from working backwards
            //             // get first easiest data
            //             string userPlayername = playerDataByUserId[userPlayerSelection];
            //
            //             int userPlayerUseridLocation = (playerData.IndexOf(userPlayername)) - 1;
            //             int userPlayerUserid = int.Parse((playerData[userPlayerUseridLocation]));
            //             
            //             int userPlayerraceLocation = (playerData.IndexOf(userPlayername) + 1);
            //             string userPlayerrace = playerData[userPlayerraceLocation];
            //
            //             int userPlayerclassLocation = (playerData.IndexOf(userPlayername) + 2);
            //             string userPlayerclass = playerData[userPlayerclassLocation];
            //             
            //             // create player
            //             player = new Player(userPlayername, userPlayerrace, userPlayerclass);
            //             
            //             Sys.WSMNL("LOGIN SUCCESFUL!");
            //             
            //             userSelectionValid = true;
            //         }
            //         catch (Exception e)
            //         {
            //             Error.WEMNL("NO VALID INPUT!");
            //         }
            //     }
            // }
        }
        
        Sys.WSMNL("LOGIN SUCCESFUL!");
        return player;
    }
}