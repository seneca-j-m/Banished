// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;
using External;
using Microsoft.Data.Sqlite;

StartMenu sMenu = new StartMenu();

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

Console.ReadKey(true);