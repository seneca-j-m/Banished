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
        DM.InitFilesystem();
        DBManager DB = new DBManager();
        DB.InitUserDB();
        DB.InitPlayerDB();
        //CosmeticMenu cMenu = new CosmeticMenu();

        // verify user
        UserManager UM = new UserManager();
        if (!UM.VerifyUser(DB))
        {
            User user = UM.SetupUser();
            UM.SaveUser(DB, user);
        }
        else
        {
            UM.UserLogin(DB);
        }
        

        Console.ReadKey(true);
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