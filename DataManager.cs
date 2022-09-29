using System;
using System.Net;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.VisualBasic.FileIO;


//TODO: ADD WARNING FUNCTION FOR EMPTY FILES, PROMPT USER TO CREATE
// TODO: ADD NEW DB FUNCTION TO ACESS MORE THAN ONE PLAYER THROUGH LOGIN
// TODO: ADD KNIGHT, SORCERER AND WARLOCK SCENES, ADD LEVELING SYSTEM

namespace BanishedMain;

public class DataManager
{
    internal bool InitFilesystem()
    {
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


            return true; //TODO: EHAT IS THIS
        }
        catch (Exception e)
        {
            throw new SaveFileCreationFailed(e);
        }
    }

    internal bool verifyCriticalFiles()
    {
        List<string> emptyDataFiles = new List<string>();

        bool critDataExists = true;
        // race data //TODO: FINISH THIS TIDY

        try
        {
            foreach (var filePath in GDirectories.GDdataFilePaths)
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    critDataExists = false; // files are empty!
                }

                if (new FileInfo(filePath).Length == 0)
                {
                    emptyDataFiles.Add(filePath);
                    critDataExists = false;
                }
            }

            return emptyDataFiles.Any();
        }
        catch (Exception e)
        {
            throw new SaveFileCreationFailed(e);
        }
    }

    internal void writeTemplateData() //TODO: FIX
    {
        Warn.WWMNL("WARNING: DATA MISSING IN ONE OR MORE CRITICAL FILES!");
        Warn.WWMNL("STORY INCOMPLETE!");

        // purge all contetns
        foreach (var filepath in GDirectories.GDdataFilePaths)
        {
            File.WriteAllText(filepath, string.Empty);
        }
        
        // write templates
        foreach (var filePath in GDirectories.GDdataFilePaths)
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
                {
                    sw.WriteLine("SCENEONEOPTIONS"); // add prompts later
                    sw.WriteLine("\n");
                    sw.WriteLine("SCENEONEOPTIONSEND");
                }
                else if (filePath.Contains("scenetwooptions")) // probably not efficent
                {
                }
                else if (filePath.Contains("scenethreeoptions"))
                {
                }
                else if (filePath.Contains("sceneoneconsequences"))
                {
                }
            }
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

    public const string playerKnightStoryDataSceneThreeF =
        @"../../../Data/Knight/Scenes/Scene_Three/scenethree.txt";

    public const string playerSorcererStoryDataSceneOneF = @"../../../Data/Sorcerer/Scenes/Scene_One/sceneone.txt";
    public const string playerSorcererStoryDataSceneTwoF = @"../../../Data/Sorcerer/Scenes/Scene_Two/scenetwo.txt";

    public const string playerSorcererStoryDataSceneThreeF =
        @"../../../Data/Sorcerer/Scenes/Scene_Two/scenethree.txt";


    public const string playerWarlockStoryDataSceneOneF = @"../../../Data/Warlock/Scenes/Scene_One/sceneone.txt";
    public const string playerWarlockStoryDataSceneTwoF = @"../../../Data/Warlock/Scenes/Scene_Two/scenetwo.txt";

    public const string playerWarlockStoryDataSceneThreeF =
        @"../../../Data/Warlock/Scenes/Scene_Three/scenethree.txt";

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


    public static readonly string[] GDdataFilePaths = new string[]
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
        GDirectories.playerSorcererStoryDataSceneTwoOptionsF,
        GDirectories.playerSorcererStoryDataSceneThreeOptionsF,
        GDirectories.playerWarlockStoryDataSceneOneOptionsF, GDirectories.playerWarlockStoryDataSceneTwoOptionsF,
        GDirectories.playerWarlockStoryDataSceneThreeOptionsF,
        GDirectories.playerKnightStoryDataSceneOneConsequencesF,
        GDirectories.playerKnightStoryDataSceneTwoConsequencesF,
        GDirectories.playerKnightStoryDataSceneThreeConsequencesF,
        GDirectories.playerSorcererStoryDataSceneOneConsequencesF,
        GDirectories.playerSorcererStoryDataSceneTwoConsequencesF,
        GDirectories.playerSorcererStoryDataSceneThreeConsequencesF,
        GDirectories.playerWarlockStoryDataSceneOneConsequencesF,
        GDirectories.playerWarlockStoryDataSceneTwoConsequencesF,
        GDirectories.playerWarlockStoryDataSceneThreeConsequencesF
    };
}