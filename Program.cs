using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace BanishedMain;

internal class Program
{
    static void Main(string[] args)
    {
        DataManager DM = new DataManager();
        DBManager DB = new DBManager();
        PlayerManager PM = new PlayerManager();
        StoryManager SM = new StoryManager();
        
        DM.InitFilesystem();

        // initial log write //TODO: FIX    
        // Logger logg = new Logger(); 
        // logg.TemplateLog();
        // logg.TransferLogs();
        
        Title();
        Introduction(DM, DB, PM, SM);
        MainMenu(PM, DB, DM, SM);

        // global instances
        Player player;

        //player = Menu(DB, PM);
        
        // GAME START ////////////////////////////////////////////////////////////////////////////
        // Sys.WSMNL($"WELCOME: {player.playername}");
        // Sys.WSMNL("YOUR ADVENTURE BEGINS...");
        // Sys.WSM("\n");
        // Game game = new Game(player);
        //
        // game.Info();
        // game.Start();
        // game.GAME();


        Console.WriteLine("END");
        Console.ReadKey(true);
    }
    
    // static Player Menu(DBManager DB, PlayerManager PM)
    // {
    //     Player player = new Player();
    //     
    //     bool mainMenuExit = false;
    //     
    //     while (!mainMenuExit)
    //     {
    //         CosmeticMenu.writeTitleCosmetics("MAIN MENU");
    //         Sys.WSMNL("0. Quit");
    //         Sys.WSMNL("1. Load Player");
    //         Sys.WSMNL("2. Create Player");
    //         Sys.WSMNL("3. Delete Player");
    //         
    //         try
    //         {
    //             int mainMenuSelection = int.Parse(Console.ReadLine());
    //
    //             switch (mainMenuSelection)
    //             {
    //                 case 0:
    //                     Sys.WSMNL("QUITTING");
    //                     Environment.Exit(0);
    //                     break;
    //                 case 1:
    //                     player = PM.PlayerLogin(DB);
    //                     mainMenuExit = true;
    //                     break;
    //                 case 2:
    //                     player = PM.SetupPlayer();
    //                     PM.SavePlayer(DB, player); // then -> rerun menu
    //                     break;
    //                 case 3:
    //                     PM.DeletePlayers(DB);
    //                     break;
    //             }
    //         }
    //         catch (Exception e)
    //         {
    //             Error.WEMNL("NO VALID INPUT!");
    //         }
    //     }
    //
    //     return player;
    // }
    
    static void MainMenu(PlayerManager PM, DBManager DB, DataManager DM, StoryManager SM)
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
        
        
        Player player = new Player();
        
        bool mainMenuExit = false;
        
        while (!mainMenuExit)
        {
            Console.Clear();
            Sys.WSMNL(mainMenuString);
            Sys.WSMNL("\n\n\n");
            
            Sys.WSMNL("0. QUIT");
            Sys.WSMNL("1. PLAYER OPTIONS");
            Sys.WSMNL("2. STORY OPTIONS");
            Sys.WSMNL("3. QUICKSTART");
            
            
            Sys.WSM("> ");


            string userMainMenuSelection = Console.ReadLine();

            switch (userMainMenuSelection)
            {
                case "0":
                    Sys.WSMNL("QUITTING");
                    Sys.WSMNL(">>> ");
                    Console.ReadLine();
                    Sys.WSMNL("GOODBYE!");
                    Environment.Exit(0);
                    break;
                case "1":
                    mainMenuExit = PlayerMenu(PM, DB, mainMenuExit);
                    break;
                case "2":
                    mainMenuExit = StoryMenu(SM, DM, mainMenuExit);
                    PM.SavePlayer(DB, player); // then -> rerun menu
                    break;
                case "3":
                    GameManager.PreStoryValidate();
                    GameManager.QUICKSTART();
                    break;
                default:
                    Error.WEMNL("NO VALID INPUT!");
                    break;
            }
        }

        
        
    }

    static bool PlayerMenu(PlayerManager PM, DBManager DB, bool mainMenuExit)
    {
        string playerMenuString =
            @"
             _______   __                                                __       __                               
            /       \ /  |                                              /  \     /  |                              
            $$$$$$$  |$$ |  ______   __    __   ______    ______        $$  \   /$$ |  ______   _______   __    __ 
            $$ |__$$ |$$ | /      \ /  |  /  | /      \  /      \       $$$  \ /$$$ | /      \ /       \ /  |  /  |
            $$    $$/ $$ | $$$$$$  |$$ |  $$ |/$$$$$$  |/$$$$$$  |      $$$$  /$$$$ |/$$$$$$  |$$$$$$$  |$$ |  $$ |
            $$$$$$$/  $$ | /    $$ |$$ |  $$ |$$    $$ |$$ |  $$/       $$ $$ $$/$$ |$$    $$ |$$ |  $$ |$$ |  $$ |
            $$ |      $$ |/$$$$$$$ |$$ \__$$ |$$$$$$$$/ $$ |            $$ |$$$/ $$ |$$$$$$$$/ $$ |  $$ |$$ \__$$ |
            $$ |      $$ |$$    $$ |$$    $$ |$$       |$$ |            $$ | $/  $$ |$$       |$$ |  $$ |$$    $$/ 
            $$/       $$/  $$$$$$$/  $$$$$$$ | $$$$$$$/ $$/             $$/      $$/  $$$$$$$/ $$/   $$/  $$$$$$/  
                                    /  \__$$ |                                                                     
                                    $$    $$/                                                                      
                                     $$$$$$/                                                                        
            ";
        
        bool playerMenuInputValid = false;

        while (!playerMenuInputValid)
        {
            Console.Clear();
            Sys.WSMNL(playerMenuString);
            Sys.WSMNL("\n\n\n");
            
            // print options
            Sys.WSMNL("0. RETURN");
            Sys.WSMNL("1. VERIFY PLAYERS");
            Sys.WSMNL("2. CREATE NEW PLAYER");
            Sys.WSMNL("3. LOAD PLAYER");
            Sys.WSMNL("4. DELETE PLAYER");
            Sys.WSMNL("5. PURGE PLAYERS");

            Sys.WSM("> ");

            string userPlayerMenuInput = Console.ReadLine();

            switch (userPlayerMenuInput)
            {
                case "0":
                    return mainMenuExit = false;
                    break;
                case "1":
                    Sys.WSMNL("VERIFYING PLAYERS...");
                    if (DB.GetPlayerDBData().Count == 0)
                    {
                        Error.WEMNL("PLAYER VERIFICATION FAILED!");

                        bool userPlayerMenuCreatePlayerInputValid = false;

                        while (!userPlayerMenuCreatePlayerInputValid)
                        {
                            Sys.WSMNL("CREATE NEW PLAYER [Y/N]: ");
                            Sys.WSM("> ");
                            
                            string userPlayerMenuCreateStoryInput = Console.ReadLine();
                            switch (userPlayerMenuCreateStoryInput)
                            {
                                case "Y":
                                case "y":
                                    GGlobals.NewPlayer = PM.SetupPlayer();
                                    userPlayerMenuCreatePlayerInputValid = true;
                                    break;
                                case "N":
                                case "n":
                                    userPlayerMenuCreatePlayerInputValid = true;
                                    break;
                                default:
                                    Error.WEMNL("NO VALID INPUT!");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        PM.ListPlayers(DB);
                        Sys.WSM(">>> ");
                        Console.ReadLine();   
                    }
                    break;
                case "2":
                    GGlobals.NewPlayer = PM.SetupPlayer();
                    DB.WriteToPlayerDB(GGlobals.NewPlayer);
                    break;
                case "3":
                    GGlobals.LoadPlayer = PM.PlayerLogin(DB);
                    break;
                case "4":
                    PM.DeletePlayers(DB);
                    break;
                case "5":
                    PM.PurgePlayers(DB);
                    break;
                default:
                    Error.WEMNL("NO VALID INPUT!");
                    break;
            }
        }

        return mainMenuExit = true;
    }

    static bool StoryMenu(StoryManager SM, DataManager DM, bool mainMenuExit)
    {

        string storyMenuString =
            @"
              ______    __                                          __       __                               
             /      \  /  |                                        /  \     /  |                              
            /$$$$$$  |_$$ |_     ______    ______   __    __       $$  \   /$$ |  ______   _______   __    __ 
            $$ \__$$// $$   |   /      \  /      \ /  |  /  |      $$$  \ /$$$ | /      \ /       \ /  |  /  |
            $$      \$$$$$$/   /$$$$$$  |/$$$$$$  |$$ |  $$ |      $$$$  /$$$$ |/$$$$$$  |$$$$$$$  |$$ |  $$ |
             $$$$$$  | $$ | __ $$ |  $$ |$$ |  $$/ $$ |  $$ |      $$ $$ $$/$$ |$$    $$ |$$ |  $$ |$$ |  $$ |
            /  \__$$ | $$ |/  |$$ \__$$ |$$ |      $$ \__$$ |      $$ |$$$/ $$ |$$$$$$$$/ $$ |  $$ |$$ \__$$ |
            $$    $$/  $$  $$/ $$    $$/ $$ |      $$    $$ |      $$ | $/  $$ |$$       |$$ |  $$ |$$    $$/ 
             $$$$$$/    $$$$/   $$$$$$/  $$/        $$$$$$$ |      $$/      $$/  $$$$$$$/ $$/   $$/  $$$$$$/  
                                                   /  \__$$ |                                                 
                                                   $$    $$/                                                  
                                                    $$$$$$/                                                                         
            ";
        bool storyMenuInputValid = false;

        while (!storyMenuInputValid)
        {
            Console.Clear();
            Sys.WSMNL(storyMenuString);
            Sys.WSMNL("\n\n\n");

            // print options
            Sys.WSMNL("0. RETURN");
            Sys.WSMNL("1. VERIFY STORY");
            Sys.WSMNL("2. LOAD STORY");
            Sys.WSMNL("3. CREATE NEW RACE");
            Sys.WSMNL("4. CREATE NEW CLASS");
            Sys.WSMNL("5. CREATE NEW ACCOLADE");
            Sys.WSMNL("6. CREATE NEW SCENE");
            Sys.WSMNL("7. CREATE NEW PROMPT");
            Sys.WSMNL("8. PURGE STORY");
            Sys.WSMNL("9. RESET INTEGRITY");

            Sys.WSM("> ");

            string userStoryMenuInput = Console.ReadLine();

            switch (userStoryMenuInput)
            {
                case "0":
                    return mainMenuExit = false;
                    break;
                case "1":
                    Console.Clear();
                    Sys.WSMNL("VERIFYING STORY");
                    Sys.WSMNL(">>> ");
                    Console.ReadLine();
                    if (!SM.VerifyStory())
                    {
                        Error.WEMNL("STORY VERIFICATION FAILED!");
                        

                        bool userStoryMenuCreateStoryInputValid = false;

                        while (!userStoryMenuCreateStoryInputValid)
                        {
                            Sys.WSMNL("CREATE NEW STORY [Y/N]: ");
                            Sys.WSM("> ");
                            
                            string userStoryMenuCreateStoryInput = Console.ReadLine();
                            switch (userStoryMenuCreateStoryInput)
                            {
                                case "Y":
                                case "y":
                                    StoryManager.CREATESTORY(SM, DM);
                                    userStoryMenuCreateStoryInputValid = true;
                                    break;
                                case "N":
                                case "n":
                                    userStoryMenuCreateStoryInputValid = true;
                                    break;
                                default:
                                    Error.WEMNL("NO VALID INPUT!");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Sys.WSMNL("STORY VERIFICATION SUCEEDED!");
                        Sys.WSMNL("VALID STORY DETECTED!");
                        Sys.WSM(">>> ");
                        Console.ReadLine();
                    }
                    break;
                case "2":
                    //TODO:
                    break;
                case "3":
                    SM.CreateRaces();
                    break;
                case "4":
                    SM.CreateClasses();
                    break;
                case "5":
                    SM.CreateAccolades();
                    break;
                case "6":
                    SM.CreateScenes();
                    break;
                case "7":
                    SM.CreatePrompt();
                    break;
                case "8":
                    //TODO:
                    break;
                case "9":
                    bool userStoryMenuRestoreStoryInputValid = false;

                    while (!userStoryMenuRestoreStoryInputValid)
                    {
                        Warn.WWMNL("WARN: THIS SILL PURGE STORY DATA AND RESTORE DEFAULTS. CONTINUE [Y/N]");
                        Warn.WWM("> ");
                            
                        string userStoryMenuRestoreStoryInput = Console.ReadLine();
                        switch (userStoryMenuRestoreStoryInput)
                        {
                            case "Y":
                            case "y":
                                SM.RestoreDefault(DM);
                                userStoryMenuRestoreStoryInputValid = true;
                                break;
                            case "N":
                            case "n":
                                Warn.WWMNL("RESTORE CANCELLED!"); 
                                userStoryMenuRestoreStoryInputValid = true;
                                break;
                            default:
                                Error.WEMNL("NO VALID INPUT!");
                                break;
                        }
                    }
                    break;
                default:
                    Error.WEMNL("NO VALID INPUT!");
                    break;
            }
        }
        return mainMenuExit = true;
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
        Warn.WWMNL("PRESS ENTER ON >>> PROMPTS TO CONTINUE");
        Sys.WSM(">>>");
        Console.ReadLine();
        Sys.WSMNL("...");
        Sys.WSMNL("VERIFYING DATA...");
        Sys.WSM(">>> ");
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
                        Sys.WSMNL("NEW PLAYER CREATED!");
                        Sys.WSM("> ");
                        Console.ReadLine();
                        Sys.WSMNL("SAVING PLAYER...");
                        DB.WriteToPlayerDB(player);
                        Sys.WSMNL("PLAYER SAVED");
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
        if (!DM.verifyCriticalFiles()[0] && !DM.verifyCriticalFiles()[1])
        {
            GGlobals.defaultStoryExists = false;
            GGlobals.customStoryExists = false;
            
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
                        // bool createClassInputValid = false;
                        //
                        // while (!createClassInputValid)
                        // {
                        //     Sys.WSMNL("Create new classes? [Y/N]");
                        //     
                        //     string classOptionInput = Console.ReadLine();
                        //     switch (classOptionInput)
                        //     {
                        //         case "Y":
                        //         case"y":
                        //     
                        //             bool playerCreateClassContinueInputValid = false;
                        //     
                        //             while (!playerCreateClassContinueInputValid)
                        //             {
                        //                 Warn.WWMNL("WARNING: THIS WILL WIPE ALL DEFAULT DATA! CONTINUE [Y/N]");
                        //     
                        //                 string playerCreateClassContinueInput = Console.ReadLine();
                        //     
                        //                 switch (playerCreateClassContinueInput)
                        //                 {
                        //                     case "Y":
                        //                     case "y":
                        //                         DM.PurgeDataDir();
                        //                         SM.CreateClasses(); ///TODO: FINISH
                        //                         playerCreateClassContinueInputValid = true;
                        //                         break;
                        //                     case "N":
                        //                     case "n":
                        //                         Error.WEMNL("ABORTED!");
                        //                         createClassInputValid = true;
                        //                         playerCreateClassContinueInputValid = true;
                        //                         break;
                        //                     default:
                        //                         Error.WEMNL("NO VALID INPUT");
                        //                         break;
                        //                 }
                        //             }
                        //             createClassInputValid = true;
                        //             break;
                        //         case "N":
                        //         case "n":
                        //             SM.CreateClasses(true);
                        //             
                        //             createClassInputValid = true;
                        //             break;
                        //         default:
                        //             Error.WEMNL("NO VALID INPUT!");
                        //             break;
                        //     }
                        // }
                        // SM.CreateBeginning(DM); // TODO: WATCH
                        // playerFileInputValid = true;
                        // break;
                        DM.PurgeDataDir();
                        StoryManager.CREATESTORY(SM, DM);
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
        else if (DM.verifyCriticalFiles()[0] && !DM.verifyCriticalFiles()[1])
        {
            Sys.WSMNL("STORY VERIFICATION SUCCESSFUL!! DEFAULT DATA EXISTS!");
            GGlobals.defaultStoryExists = true;
        }
        else if (!DM.verifyCriticalFiles()[0] && DM.verifyCriticalFiles()[1])
        {
            Sys.WSMNL("STORY VERIFICATION SUCCESSFUL!! CUSTOM DATA EXISTS!");
            GGlobals.defaultStoryExists = false;
            GGlobals.customStoryExists = true;
        }
        else
        {
            Sys.WSMNL("STORY VERIFICATION SUCCESSFUL!! CUSTOM AND DEFAULT DATA EXISTS!");
            GGlobals.defaultStoryExists = true;
            GGlobals.customStoryExists = true;
        }
    }
}