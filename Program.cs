// using System.ComponentModel.Design;
// using Banished;
using Microsoft.Data.Sqlite;
using System;

namespace BanishedMain;

internal class Program
{
    static void Main(string[] args)
    {
        Logger logg = new Logger();
        DataManager DM = new DataManager();
        if (!DM.InitFilesystem()) // change this 
        {
            Warn.WWMNL("WARN: FAILURE IN FILE INTEGRITY");
            Warn.WWMNL("WARN: SENDING TO STORY MANAGER");
        }
        DBManager DB = new DBManager();
        DB.InitPlayerDB();
        //CosmeticMenu cMenu = new CosmeticMenu();

        // verify user
        PlayerManager PM = new PlayerManager();
        
        // global instances
        Player player;

        Sys.WSMNL("Welcome!");
        Sys.WSMNL("Lets Begin!");

        // now verify player
        if (!PM.VerifyPlayer(DB))
        {
            player = PM.SetupPlayer();
            PM.SavePlayer(DB, player);
        }
        else
        {
            Sys.WSMNL("Player data detected!");
        }
        
        player = Menu(DB, PM);
        
        // GAME START ////////////////////////////////////////////////////////////////////////////
        Sys.WSMNL($"WELCOME: {player.playername}");
        Sys.WSMNL("YOUR ADVENTURE BEGINS...");
        Sys.WSM("\n");
        Game game = new Game(player);
        
        game.Info();
        game.Start();
        game.GAME();


        Console.WriteLine("END");
        Console.ReadKey(true);
    }
    
    static Player Menu(DBManager DB, PlayerManager PM)
    {
        Player player = new Player();
        
        bool mainMenuExit = false;
        
        while (!mainMenuExit)
        {
            CosmeticMenu.writeTitleCosmetics("MAIN MENU");
            Sys.WSMNL("0. Quit");
            Sys.WSMNL("1. Load Player");
            Sys.WSMNL("2. Create Player");
            Sys.WSMNL("3. Delete Player");
            
            try
            {
                int mainMenuSelection = int.Parse(Console.ReadLine());

                switch (mainMenuSelection)
                {
                    case 0:
                        Sys.WSMNL("QUITTING");
                        Environment.Exit(0);
                        break;
                    case 1:
                        player = PM.PlayerLogin(DB);
                        mainMenuExit = true;
                        break;
                    case 2:
                        player = PM.SetupPlayer();
                        PM.SavePlayer(DB, player); // then -> rerun menu
                        break;
                    case 3:
                        PM.DeletePlayers(DB);
                        break;
                }
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
            }
        }

        return player;
    }
    
    static void StoryFacilitation()
    {
    
    }
}



// CALL FUNCTIONS

// StartMenu sMenu = new StartMenu();


// static void PrintMenu()
// {
//     Console.WriteLine("Hello, World!");
//     Console.WriteLine("Reading Data...");
//     Console.WriteLine("0. Quit");
//     Console.WriteLine("1. Start");
//
//     bool menuActive = true;
//     while (menuActive)
//     {
//         string? input = Console.ReadLine();
//         if (input is null)
//         {
//            Console.WriteLine("INPUT IS NULL");
//         }
//         switch (input.ToString())
//         {
//             case "0":
//                 Environment.Exit(0);
//                 menuActive = false;
//                 break;
//             case "1":
//                 menuActive = false;
//                 break;
//             case "D":
//             case "d":
//                 DebugMenu DMenu = new DebugMenu();
//                 break;
//             default:
//                 break;
//         }
//     }
// }


//
//
//
// PrintMenu();