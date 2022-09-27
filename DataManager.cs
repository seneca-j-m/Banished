using System;
using System.Net;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;


//TODO: ADD WARNING FUNCTION FOR EMPTY FILES, PROMPT USER TO CREATE
// TODO: ADD NEW DB FUNCTION TO ACESS MORE THAN ONE PLAYER THROUGH LOGIN
// TODO: ADD KNIGHT, SORCERER AND WARLOCK SCENES, ADD LEVELING SYSTEM

namespace BanishedMain;

public class DataManager
{
    internal bool InitFilesystem()
    {
        List<string> emptyDataFiles = new List<string>();

        bool critDataExists = true;
        // verify directories
        try
        {
            // create user directory (dont need to check if it does or doesnt exist)
            //Directory.CreateDirectory(GDirectories.userPath);

            // player and data path
            Directory.CreateDirectory(GDirectories.playerPath);
            Directory.CreateDirectory(GDirectories.dataPath);

            // logger path
            Directory.CreateDirectory(GDirectories.loggerPath);
            Directory.CreateDirectory(GDirectories.loggerBPath);

            // race path
            Directory.CreateDirectory(GDirectories.playerRaceData);
            Directory.CreateDirectory(GDirectories.playerClassData);
            Directory.CreateDirectory(GDirectories.playerAccoladeData);

            // class story path
            Directory.CreateDirectory(GDirectories.playerKnightStoryDataPath);
            Directory.CreateDirectory(GDirectories.playerSorcererStoryDataPath);
            Directory.CreateDirectory(GDirectories.playerWarlockStoryDataPath);

            // knight scene paths
            Directory.CreateDirectory(GDirectories.playerKnightScenePath);
            Directory.CreateDirectory(GDirectories.playerKnightSceneOnePath);
            Directory.CreateDirectory(GDirectories.playerKnightSceneTwoPath);
            Directory.CreateDirectory(GDirectories.playerKnightSceneThreePath);

            // sorcerer scene paths
            Directory.CreateDirectory(GDirectories.playerSorcererScenePath);
            Directory.CreateDirectory(GDirectories.playerSorcererSceneOnePath);
            Directory.CreateDirectory(GDirectories.playerSorcererSceneTwoPath);
            Directory.CreateDirectory(GDirectories.playerSorcererSceneThreePath);

            // warlock scene paths
            Directory.CreateDirectory(GDirectories.playerWarlockScenePath);
            Directory.CreateDirectory(GDirectories.playerWarlockSceneOnePath);
            Directory.CreateDirectory(GDirectories.playerWarlockSceneTwoPath);
            Directory.CreateDirectory(GDirectories.playerWarlockSceneThreePath);


            // files
            // if (!File.Exists(GDirectories.userDBPath))
            //     File.Create(GDirectories.userDBPath);

            if (!File.Exists(GDirectories.playerDBPath))
                File.Create(GDirectories.playerDBPath);

            if (!File.Exists(GDirectories.loggerFPath))
                File.Create(GDirectories.loggerFPath);

            if (!File.Exists(GDirectories.loggerBFPath))
                File.Create(GDirectories.loggerBFPath);

            // player construction files

            // race data //TODO: FINISH THIS TIDY
            string[] dataFilePaths = new string[]
            {
                GDirectories.playerRaceHumanDataF, GDirectories.playerRaceElfDataF, GDirectories.playerRaceOrcDataF,
                GDirectories.playerClassKnightDataF, GDirectories.playerClassSorcererDataF,
                GDirectories.playerClassWarlockDataF, GDirectories.playerAccoladeWarriorDataF,
                GDirectories.playerAccoladeScholarDataF, GDirectories.playerAccoladeAcolyteDataF,
                GDirectories.playerKnightStoryDataBeginningF, GDirectories.playerSorcererStoryDataBeginningF,
                GDirectories.playerWarlockStoryDataBeginningF, GDirectories.playerKnightStoryDataSceneOneF,
                GDirectories.playerKnightStoryDataSceneTwoF, GDirectories.playerKnightStoryDataSceneThreeF,
                GDirectories.playerSorcererStoryDataSceneOneF, GDirectories.playerSorcererStoryDataSceneTwoF,
                GDirectories.playerSorcererStoryDataSceneThreeF, GDirectories.playerWarlockStoryDataSceneOneF,
                GDirectories.playerWarlockStoryDataSceneTwoF, GDirectories.playerWarlockStoryDataSceneThreeF,
                GDirectories.playerKnightStoryDataSceneOneOptionsF, GDirectories.playerKnightStoryDataSceneTwoOptionsF,
                GDirectories.playerKnightStoryDataSceneThreeOptionsF, GDirectories.playerSorcererStoryDataSceneOneOptionsF,
                GDirectories.playerSorcererStoryDataSceneTwoOptionsF, GDirectories.playerSorcererStoryDataSceneThreeOptionsF,
                GDirectories.playerWarlockStoryDataSceneOneOptionsF, GDirectories.playerWarlockStoryDataSceneTwoOptionsF,
                GDirectories.playerWarlockStoryDataSceneThreeOptionsF, GDirectories.playerKnightStoryDataSceneOneConsequencesF, 
                GDirectories.playerKnightStoryDataSceneTwoConsequencesF, GDirectories.playerKnightStoryDataSceneThreeConsequencesF,
                GDirectories.playerSorcererStoryDataSceneOneConsequencesF, GDirectories.playerSorcererStoryDataSceneTwoConsequencesF,
                GDirectories.playerSorcererStoryDataSceneThreeConsequencesF, GDirectories.playerWarlockStoryDataSceneOneConsequencesF,
                GDirectories.playerWarlockStoryDataSceneTwoConsequencesF, GDirectories.playerWarlockStoryDataSceneThreeConsequencesF
            };

            string missingDataFileName = "";

            foreach (var filePath in dataFilePaths)
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    critDataExists = false;  // files are empty!
                }

                if (new FileInfo(filePath).Length == 0)
                {
                    Warn.WWMNL($"WARN: {filePath} has no data!");
                    emptyDataFiles.Add(filePath);
                    critDataExists = false;
                }
            }

            if (!critDataExists)
            {
                Warn.WWMNL("WARNING: DATA MISSING IN ONE OR MORE CRITICAL FILES!");
                Warn.WWMNL("Writing template data ");

                // write templates
                foreach (var filePath in emptyDataFiles)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        if (filePath.Contains("human.txt") || filePath.Contains("elf.txt") || // races
                            filePath.Contains("orc.txt"))
                        {
                            sw.WriteLine("DESCRIPTION");
                            sw.WriteLine("\n");
                            sw.WriteLine("DESCRIPTIONEND");
                        }
                        else if (filePath.Contains("knight.txt") || filePath.Contains("sorcerer.txt") || // classes
                                 filePath.Contains("warlock.txt"))
                        {
                            sw.WriteLine("DESCRIPTION");
                            sw.WriteLine("\n");
                            sw.WriteLine("DESCRIPTIONEND");
                        }
                        else if (filePath.Contains("warrior.txt") || filePath.Contains("scholar.txt") ||
                                 filePath.Contains("acolyte.txt"))
                        {
                            sw.WriteLine("DESCRIPTION");
                            sw.WriteLine("\n");
                            sw.WriteLine("DESCRIPTIONEND");
                        }
                        else if (filePath.Contains("Knight/beginning.txt") ||
                                 filePath.Contains("Sorcerer/beginning.txt") ||
                                 filePath.Contains("Warlock/beginning.txt"))
                        {
                            sw.WriteLine("BEGINNING");
                            sw.WriteLine("\n");
                            sw.WriteLine("BEGINNINGEND");
                        }
                        else if (filePath.Contains("sceneone.txt")) // add prompts later
                        {
                            sw.WriteLine("SCENEONE");
                            sw.WriteLine("\n");
                            sw.WriteLine("SCENEONEEND");
                        }
                        else if (filePath.Contains("scenetwo.txt"))
                        {
                            sw.WriteLine("SCENETWO");
                            sw.WriteLine("\n");
                            sw.WriteLine("SCENETWOEND");
                        }
                        else if (filePath.Contains("scenethree.txt"))
                        {
                            sw.WriteLine("SCENETHREE");
                            sw.WriteLine("\n");
                            sw.WriteLine("SCENETHREEEND");
                        }
                        else if (filePath.Contains("sceneoneoptions")) //TODO: FINISH THIS
                        { }
                        else if (filePath.Contains("sceneoneconsequences"))
                        { }
                    }
                }

                return false; 


                // // write race info here
                // for (int i = 0; i < 2; i++)
                // {
                //     using (StreamWriter sw = new StreamWriter(dataFilePaths[i]))
                //     {
                //         sw.WriteLine("DESCRIPTION"); //TODO: ADJUST TO ACTUAL VALUES
                //         sw.WriteLine();
                //         sw.WriteLine("DESCRIPTIONEND");
                //     }
                // }
                //
                // // write class info here
                // for (int i = 3; i < 5; i++)
                // {
                //     using (StreamWriter sw = new StreamWriter(dataFilePaths[i]))
                //     {
                //         sw.WriteLine("DESCRIPTION");
                //         sw.WriteLine();
                //         sw.WriteLine("DESCRIPTIONEND");
                //     }
                // }
                //
                // // write accolade info here
                // for (int i = 6; i < 8; i++)
                // {
                //     using (StreamWriter sw = new StreamWriter(dataFilePaths[i]))
                //     {
                //         sw.WriteLine("DESCRIPTION");
                //         sw.WriteLine();
                //         sw.WriteLine("DESCRIPTIONEND");
                //     }
                // }
                //
                // // story data
                // for (int i = 9; i < 11; i++)
                // {
                //     using (StreamWriter sw = new StreamWriter(dataFilePaths[i]))
                //     {
                //         sw.WriteLine("BEGINNING");
                //         sw.WriteLine();
                //         sw.WriteLine("BEGINNINGEND");
                //     }
                // }
                //
                // for (int i = 12; i < 14; i++) //TODO: FINISH THIS
                // {
                //     using (StreamWriter sw = new StreamWriter(dataFilePaths[i]))
                //     {
                //         sw.WriteLine("SCENEONE");
                //         sw.WriteLine();
                //         sw.WriteLine("PROMPTONE");
                //         sw.WriteLine();
                //         sw.WriteLine("PROMPTONEEND");
                //         sw.WriteLine();
                //         sw.WriteLine("PROMPTTWO");
                //         sw.WriteLine();
                //         sw.WriteLine("PROMPTTWOEND");
                //         sw.WriteLine();
                //         sw.WriteLine("PROMPTTHREE");
                //         sw.WriteLine();
                //         sw.WriteLine("PROMPTTHREEEND");
                //         sw.WriteLine();
                //         sw.WriteLine("SCENEONEEND");
                //         sw.WriteLine();
                //     }
                // }
            }


            //
            // if (!File.Exists(GDirectories.playerRaceHumanDataF))
            //     File.Create(GDirectories.playerRaceHumanDataF);
            //
            // if (!File.Exists(GDirectories.playerRaceElfDataF))
            //     File.Create(GDirectories.playerRaceElfDataF);
            //
            // if (!File.Exists(GDirectories.playerRaceOrcDataF))
            //     File.Create(GDirectories.playerRaceOrcDataF);
            //
            // // class data
            // if (!File.Exists(GDirectories.playerClassKnightDataF))
            //     File.Create(GDirectories.playerClassKnightDataF);
            //
            // if (!File.Exists(GDirectories.playerClassSorcererDataF))
            //     File.Create(GDirectories.playerClassSorcererDataF);
            //
            // if (!File.Exists(GDirectories.playerClassWarlockDataF))
            //     File.Create(GDirectories.playerClassWarlockDataF);
            //
            // // accolade data
            // if (!File.Exists(GDirectories.playerAccoladeWarriorDataF))
            //     File.Create(GDirectories.playerAccoladeWarriorDataF);
            //
            // if (!File.Exists(GDirectories.playerAccoladeScholarDataF))
            //     File.Create(GDirectories.playerAccoladeScholarDataF);
            //
            // if (!File.Exists(GDirectories.playerAccoladeAcolyteDataF))
            //     File.Create(GDirectories.playerAccoladeAcolyteDataF);
            //
            // //  story data
            // if (!File.Exists(GDirectories.playerKnightStoryDataBeginningF))
            //     File.Create(GDirectories.playerKnightStoryDataBeginningF);
            //
            // if (!File.Exists(GDirectories.playerSorcererStoryDataBeginningF))
            //     File.Create(GDirectories.playerSorcererStoryDataBeginningF);
            //
            // if (!File.Exists(GDirectories.playerWarlockStoryDataBeginningF))
            //     File.Create(GDirectories.playerWarlockStoryDataBeginningF);
        }
        catch (Exception e)
        {
            return false;
            throw new SaveFileCreationFailed(e);
        }

        return true; //TODO: EHAT IS THIS
    }
}

public class DBManager
{
    //protected SqliteConnection DBConnection = new SqliteConnection(GDirectories.userDBPath);
    internal DBManager()
    {
    }

    ~DBManager() // just in caseys
    {
        //DBConnection.Close();
    }
    //
    // internal void InitUserDB()
    // {
    //     try
    //     {
    //         using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
    //         {
    //             conn.Open();
    //             Debug.WDMNL("Connection Initialised: userDB");
    //             var comm = conn.CreateCommand();
    //             comm.CommandText = // TODO: ADD USER ID SECTION
    //                 @"
    //                 CREATE TABLE IF NOT EXISTS users (
    //                     id INTEGER PRIMARY KEY,
    //                     firstname TEXT NOT NULL,
    //                     lastname TEXT NOT NULL,
    //                     age INTEGER NOT NULL,
    //                     password TEXT NOT NULL 
    //                 );
    //                 ";
    //             var readr = comm.ExecuteNonQuery();
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         throw new DatabaseConnectionFailed(e);
    //     }
    // }

    internal void InitPlayerDB()
    {
        try
        {
            using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
            {
                conn.Open();
                Debug.WDMNL("Connection Initialsed: playerDB");
                var comm = conn.CreateCommand();
                comm.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS players (
                        id INTEGER PRIMARY KEY,
                        playername TEXT NOT NULL,
                        playerrace TEXT NOT NULL,
                        playerclass TEXT NOT NULL,
                        playeraccolade TEXT NOT NULL
                    )
                    ";
                var readr = comm.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            throw new DatabaseConnectionFailed(e);
        }
    }

    // internal List<string> GetUserDBData()
    // {
    //     List<string> userDBData = new List<string>();
    //
    //     using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
    //     {
    //         conn.Open();
    //         var comm = conn.CreateCommand();
    //         comm.CommandText =
    //             @"
    //                 SELECT * FROM users;
    //             ";
    //         using (var readr = comm.ExecuteReader())
    //         {
    //             while (readr.Read())
    //             {
    //                 for (int i = 0; i <= 4; i++)
    //                 {
    //                     userDBData.Add(readr.GetValue(i).ToString());
    //                 }
    //             }
    //         }
    //     }
    //
    //     return userDBData;
    // }

    internal List<Player> GetPlayerDBData()
    {
        List<Player> playerDBData = new List<Player>();

        using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                @"
                    SELECT * FROM players;
                ";
            using (var readr = comm.ExecuteReader())
            {
                while (readr.Read())
                {
                    Enum.TryParse(readr.GetValue(2).ToString().ToUpper(), out PlayerRace plRace);
                    Enum.TryParse(readr.GetValue(3).ToString().ToUpper(), out PlayerClass plClass);
                    Enum.TryParse(readr.GetValue(4).ToString().ToUpper(), out PlayerAccolade plAccolade);
                    Player player = new Player(readr.GetValue(1).ToString(), plRace,
                        plClass, plAccolade);

                    playerDBData.Add(player);
                }
            }
        }

        return playerDBData;
    }

    // internal void WriteToUserDB(User user)
    // {
    //     using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
    //     {
    //         conn.Open();
    //         var comm = conn.CreateCommand();
    //         comm.CommandText =
    //             $@"
    //                 INSERT INTO users (firstname, lastname, age, password) VALUES ('{user.firstname}', '{user.lastname}', '{user.age}', '{user.password}');
    //             ";
    //         using (var readr = comm.ExecuteReader()) ;
    //     }
    // }

    internal void WriteToPlayerDB(Player player)
    {
        using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                $@"
                    INSERT INTO players (playername, playerrace, playerclass, playeraccolade) VALUES ('{player.playername}', '{player.playerrace.ToString()}', '{player.playerclass.ToString()}', '{player.playeraccolade.ToString()}');
                ";
            using (var readr = comm.ExecuteReader()) ;
        }
    }

    internal void DeleteFromPlayerDB(string playerName)
    {
        using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                $@"
                    DELETE FROM players WHERE playername = '{playerName}';
                ";
            using (var readr = comm.ExecuteReader()) ;
        }
    }
}

public static class GDirectories
{
    // user data and database paths
    // public const string userPath = @"../../../User/";
    // public const string userDBPath = @"../../../User/user.sqlite";

    // player data and database paths
    public const string playerPath = @"../../../Player/";
    public const string playerDBPath = @"../../../Player/player.sqlite";

    // logger paths
    public const string loggerPath = @"../../../Logger";
    public const string loggerFPath = @"../../../Logger/log.log";
    public const string loggerBPath = @"../../../Logger/Backup";
    public const string loggerBFPath = @"../../../Logger/Backup/log_backup.log"; // watch this chief

    // primary data path
    public const string dataPath = @"../../../Data/";

    // player construction data directories
    public const string playerRaceData = @"../../../Data/playerRaceData/";
    public const string playerClassData = @"../../../Data/playerClassData/";
    public const string playerAccoladeData = @"../../../Data/playerAccoladeData/";

    // player race data files
    public const string playerRaceHumanDataF = @"../../../Data/playerRaceData/human.txt";
    public const string playerRaceElfDataF = @"../../../Data/playerRaceData/elf.txt";
    public const string playerRaceOrcDataF = @"../../../Data/playerRaceData/orc.txt";

    // player class data files
    public const string playerClassKnightDataF = @"../../../Data/playerClassData/knight.txt";
    public const string playerClassSorcererDataF = @"../../../Data/playerClassData/sorcerer.txt";
    public const string playerClassWarlockDataF = @"../../../Data/playerClassData/warlock.txt";

    // player accolade data files
    public const string playerAccoladeWarriorDataF = @"../../../Data/playerAccoladeData/warrior.txt";
    public const string playerAccoladeScholarDataF = @"../../../Data/playerAccoladeData/scholar.txt";
    public const string playerAccoladeAcolyteDataF = @"../../../Data/playerAccoladeData/acolyte.txt";

    // player class STORY data path
    public const string playerKnightStoryDataPath = @"../../../Data/Knight";
    public const string playerSorcererStoryDataPath = @"../../../Data/Sorcerer";
    public const string playerWarlockStoryDataPath = @"../../../Data/Warlock";

    // public class STORY data files
    public const string playerKnightStoryDataBeginningF = @"../../../Data/Knight/beginning.txt";
    public const string playerSorcererStoryDataBeginningF = @"../../../Data/Sorcerer/beginning.txt";
    public const string playerWarlockStoryDataBeginningF = @"../../../Data/Warlock/beginning.txt";

    // scene paths
    public const string playerKnightScenePath = @"../../../Data/Knight/Scenes/";
    public const string playerSorcererScenePath = @"../../../Data/Sorcerer/Scenes";
    public const string playerWarlockScenePath = @"../../../Data/Warlock/Scenes/";

    // inidivdual scene folder
    public const string playerKnightSceneOnePath = @"../../../Data/Knight/Scenes/Scene_One/";
    public const string playerKnightSceneTwoPath = @"../../../Data/Knight/Scenes/Scene_Two/";
    public const string playerKnightSceneThreePath = @"../../../Data/Knight/Scenes/Scene_Three/";

    public const string playerSorcererSceneOnePath = @"../../../Data/Sorcerer/Scenes/Scene_One/";
    public const string playerSorcererSceneTwoPath = @"../../../Data/Sorcerer/Scenes/Scene_Two/";
    public const string playerSorcererSceneThreePath = @"../../../Data/Sorcerer/Scenes/Scene_Three/";

    public const string playerWarlockSceneOnePath = @"../../../Data/Warlock/Scenes/Scene_One/";
    public const string playerWarlockSceneTwoPath = @"../../../Data/Warlock/Scenes/Scene_Two/";
    public const string playerWarlockSceneThreePath = @"../../../Data/Warlock/Scenes/Scene_Three/";

    // individual scene files
    public const string playerKnightStoryDataSceneOneF = @"../../../Data/Knight/Scenes/Scene_One/sceneone.txt";
    public const string playerKnightStoryDataSceneTwoF = @"../../../Data/Knight/Scenes/Scene_Two/scenetwo.txt";
    public const string playerKnightStoryDataSceneThreeF = @"../../../Data/Knight/Scenes/Scene_Three/scenethree.txt";

    public const string playerSorcererStoryDataSceneOneF = @"../../../Data/Sorcerer/Scenes/Scene_One/sceneone.txt";
    public const string playerSorcererStoryDataSceneTwoF = @"../../../Data/Sorcerer/Scenes/Scene_Two/scenetwo.txt";
    public const string playerSorcererStoryDataSceneThreeF = @"../../../Data/Sorcerer/Scenes/Scene_Two/scenethree.txt";


    public const string playerWarlockStoryDataSceneOneF = @"../../../Data/Warlock/Scenes/Scene_One/sceneone.txt";
    public const string playerWarlockStoryDataSceneTwoF = @"../../../Data/Warlock/Scenes/Scene_Two/scenetwo.txt";
    public const string playerWarlockStoryDataSceneThreeF = @"../../../Data/Warlock/Scenes/Scene_Three/scenethree.txt";

    // individual option files
    public const string playerKnightStoryDataSceneOneOptionsF =
        @"../../../Data/Knight/Scenes/Scene_One/sceneoneoptions.txt";

    public const string playerKnightStoryDataSceneTwoOptionsF =
        @"../../../Data/Knight/Scenes/Scene_Two/scenetwooptions.txt";

    public const string playerKnightStoryDataSceneThreeOptionsF =
        @"../../../Data/Knight/Scenes/Scene_Three/sceneThreeoptions.txt";

    public const string playerSorcererStoryDataSceneOneOptionsF =
        @"../../../Data/Sorcerer/Scenes/Scene_One/sceneoneoptions.txt";

    public const string playerSorcererStoryDataSceneTwoOptionsF =
        @"../../../Data/Sorcerer/Scenes/Scene_Two/scenetwooptions.txt";

    public const string playerSorcererStoryDataSceneThreeOptionsF =
        @"../../../Data/Sorcerer/Scenes/Scene_Three/sceneThreeoptions.txt";

    public const string playerWarlockStoryDataSceneOneOptionsF =
        @"../../../Data/Warlock/Scenes/Scene_One/sceneoneoptions.txt";

    public const string playerWarlockStoryDataSceneTwoOptionsF =
        @"../../../Data/Warlock/Scenes/Scene_Two/scenetwooptions.txt";

    public const string playerWarlockStoryDataSceneThreeOptionsF =
        @"../../../Data/Warlock/Scenes/Scene_Three/sceneThreeoptions.txt";

    // individual consequence files
    public const string playerKnightStoryDataSceneOneConsequencesF =
        @"../../../Data/Knight/Scenes/Scene_One/sceneoneconsequences.txt";

    public const string playerKnightStoryDataSceneTwoConsequencesF =
        @"../../../Data/Knight/Scenes/Scene_Two/scenetwoconsequences.txt";

    public const string playerKnightStoryDataSceneThreeConsequencesF =
        @"../../../Data/Knight/Scenes/Scene_Three/scenethreeconsequences.txt";

    public const string playerSorcererStoryDataSceneOneConsequencesF =
        @"../../../Data/Sorcerer/Scenes/Scene_One/sceneoneconsequences.txt";

    public const string playerSorcererStoryDataSceneTwoConsequencesF =
        @"../../../Data/Sorcerer/Scenes/Scene_Two/scenetwoconsequences.txt";

    public const string playerSorcererStoryDataSceneThreeConsequencesF =
        @"../../../Data/Sorcerer/Scenes/Scene_Three/scenethreeconsequences.txt";

    public const string playerWarlockStoryDataSceneOneConsequencesF =
        @"../../../Data/Warlock/Scenes/Scene_One/sceneoneconsequences.txt";

    public const string playerWarlockStoryDataSceneTwoConsequencesF =
        @"../../../Data/Warlock/Scenes/Scene_Two/scenetwoconsequences.txt";

    public const string playerWarlockStoryDataSceneThreeConsequencesF =
        @"../../../Data/Warlock/Scenes/Scene_Three/scenethreeconsequences.txt";
}