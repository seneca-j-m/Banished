// using System.ComponentModel.Design;
// using Banished;
using Microsoft.Data.Sqlite;
using System;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace BanishedMain;

internal class Program
{
    static void Main(string[] args)
    {
        Logger logg = new Logger();
        DataManager DM = new DataManager();
        DBManager DB = new DBManager();
        PlayerManager PM = new PlayerManager();
        StoryManager SM = new StoryManager();
        
        Title();
        Introduction(DM, DB, PM, SM);
        MainMenu();
        

        // if (!DM.InitFilesystem()) // change this 
        // {
        //     Warn.WWMNL("WARN: FAILURE IN FILE INTEGRITY");
        //     Warn.WWMNL("WARN: SENDING TO STORY MANAGER");
        // }
        // DB.InitPlayerDB();
        //CosmeticMenu cMenu = new CosmeticMenu();
        
        
        // global instances
        Player player;

        // Sys.WSMNL("Welcome!");
        // Sys.WSMNL("Lets Begin!");
        //
        // // now verify player
        // // if (!PM.VerifyPlayer(DB))
        // // {
        // //     player = PM.SetupPlayer();
        // //     PM.SavePlayer(DB, player);
        // // }
        // // else
        // // {
        // //     Sys.WSMNL("Player data detected!");
        // // }
        
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

    static void Title()
    {
        string titleString = 
            @"
         __       __   ______   _______   __        _______         _______   __    __  ______  __        _______   ________  _______          ______  
        /  |  _  /  | /      \ /       \ /  |      /       \       /       \ /  |  /  |/      |/  |      /       \ /        |/       \        /      \ 
        $$ | / \ $$ |/$$$$$$  |$$$$$$$  |$$ |      $$$$$$$  |      $$$$$$$  |$$ |  $$ |$$$$$$/ $$ |      $$$$$$$  |$$$$$$$$/ $$$$$$$  |      /$$$$$$  |
        $$ |/$  \$$ |$$ |  $$ |$$ |__$$ |$$ |      $$ |  $$ |      $$ |__$$ |$$ |  $$ |  $$ |  $$ |      $$ |  $$ |$$ |__    $$ |__$$ |      $$ |  $$/ 
        $$ /$$$  $$ |$$ |  $$ |$$    $$< $$ |      $$ |  $$ |      $$    $$< $$ |  $$ |  $$ |  $$ |      $$ |  $$ |$$    |   $$    $$<       $$ |      
        $$ $$/$$ $$ |$$ |  $$ |$$$$$$$  |$$ |      $$ |  $$ |      $$$$$$$  |$$ |  $$ |  $$ |  $$ |      $$ |  $$ |$$$$$/    $$$$$$$  |      $$ |   __ 
        $$$$/  $$$$ |$$ \__$$ |$$ |  $$ |$$ |_____ $$ |__$$ |      $$ |__$$ |$$ \__$$ | _$$ |_ $$ |_____ $$ |__$$ |$$ |_____ $$ |  $$ |      $$ \__/  |
        $$$/    $$$ |$$    $$/ $$ |  $$ |$$       |$$    $$/       $$    $$/ $$    $$/ / $$   |$$       |$$    $$/ $$       |$$ |  $$ |      $$    $$/ 
        $$/      $$/  $$$$$$/  $$/   $$/ $$$$$$$$/ $$$$$$$/        $$$$$$$/   $$$$$$/  $$$$$$/ $$$$$$$$/ $$$$$$$/  $$$$$$$$/ $$/   $$/        $$$$$$/                                                                                                                                                                                                                                                                                                                                                                                                                                    
            ";
        
        Sys.WSMNL(titleString);
        Sys.WSMNL("\n\n\n");
    }

    static void Introduction(DataManager DM, DBManager DB, PlayerManager PM, StoryManager SM)
    {
        Sys.WSMNL("...");
        Sys.WSMNL("Welcome User!");
        Warn.WWMNL("PRESS ENTER TO CONTINUE");
        Sys.WSM("> ");
        Console.ReadLine();
        Sys.WSMNL("...");
        Sys.WSMNL("VERIFYING DATA...");
        Sys.WSM("> ");
        Console.ReadLine();
        
        DB.InitPlayerDB();

        if (!PM.VerifyPlayer(DB))
        {
            Player player = new Player();
            
            Error.WEMNL("NO PLAYER DETECTED!");
            
            bool playerMenuInputValid = false;

            while (!playerMenuInputValid)
            {
                Sys.WSMNL("Create new player? [Y/N}");
                Sys.WSM("> ");
                
                string playerMenuInput = Console.ReadLine();

                switch (playerMenuInput)
                {
                    case "Y":
                    case "y":
                        player = PM.SetupPlayer();
                        Sys.WSMNL("New Player created!");
                        Sys.WSM("> ");
                        Console.ReadLine();
                        Sys.WSMNL("Saving player...");
                        DB.WriteToPlayerDB(player);
                        Sys.WSMNL("Player saved!");
                        playerMenuInputValid = true;
                        break;
                    case "N":
                    case "n": // just for clarity
                        playerMenuInputValid = true;
                        break;
                    default:
                        Error.WEMNL("NO VALID INPUT!");
                        break;
                }

            }
        }
        // verify files next
        DM.InitFilesystem();
        if (!DM.verifyCriticalFiles())
        {
            Error.WEMNL("VERFIYNG INTEGRITY COMPLETED!");
            Error.WEMNL("STORY CREATION REQUIRED");

            bool playerFileInputValid = false;
            
            while (!playerFileInputValid)
            {
                Sys.WSMNL("Create new story? [Y/N}");
                Sys.WSM("> ");
                
                string fileMenuInput = Console.ReadLine();

                switch (fileMenuInput)
                {
                    case "Y":
                    case "y":
                        bool createClassInputValid = false;

                        while (!createClassInputValid)
                        {
                            Sys.WSMNL("Create new classes? [Y/N]");

                            string classOptionInput = Console.ReadLine();
                            switch (classOptionInput)
                            {
                                case "Y":
                                case"y":
                                    SM.CreateClasses(); ///
                                    createClassInputValid = true;
                                    break;
                                case "N":
                                case "n":
                                    SM.CreateClasses(true);
                                    createClassInputValid = true;
                                    break;
                                default:
                                    Error.WEMNL("NO VALID INPUT!");
                                    break;
                            }
                        }
                        
                        
                        SM.CreateBeginning();
                        playerFileInputValid = true;
                        break;
                    case "N":
                    case "n": // just for clarity
                        playerFileInputValid = true; 
                        break;
                    default:
                        Error.WEMNL("NO VALID INPUT!");
                        break;
                }

            }
            
        }
    }

    static void MainMenu()
    {
        string mainMenuString = 
            @"
             __       __   ______   ______  __    __        __       __  ________  __    __  __    __ 
            /  \     /  | /      \ /      |/  \  /  |      /  \     /  |/        |/  \  /  |/  |  /  |
            $$  \   /$$ |/$$$$$$  |$$$$$$/ $$  \ $$ |      $$  \   /$$ |$$$$$$$$/ $$  \ $$ |$$ |  $$ |
            $$$  \ /$$$ |$$ |__$$ |  $$ |  $$$  \$$ |      $$$  \ /$$$ |$$ |__    $$$  \$$ |$$ |  $$ |
            $$$$  /$$$$ |$$    $$ |  $$ |  $$$$  $$ |      $$$$  /$$$$ |$$    |   $$$$  $$ |$$ |  $$ |
            $$ $$ $$/$$ |$$$$$$$$ |  $$ |  $$ $$ $$ |      $$ $$ $$/$$ |$$$$$/    $$ $$ $$ |$$ |  $$ |
            $$ |$$$/ $$ |$$ |  $$ | _$$ |_ $$ |$$$$ |      $$ |$$$/ $$ |$$ |_____ $$ |$$$$ |$$ \__$$ |
            $$ | $/  $$ |$$ |  $$ |/ $$   |$$ | $$$ |      $$ | $/  $$ |$$       |$$ | $$$ |$$    $$/ 
            $$/      $$/ $$/   $$/ $$$$$$/ $$/   $$/       $$/      $$/ $$$$$$$$/ $$/   $$/  $$$$$$/  
            ";
        
        Sys.WSMNL(mainMenuString);
        Sys.WSMNL("\n\n\n");
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