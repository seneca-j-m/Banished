using System;
using System.Net;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Microsoft.VisualBasic.FileIO;


//TODO: ADD WARNING FUNCTION FOR EMPTY FILES, PROMPT USER TO CREATE
// TODO: ADD NEW DB FUNCTION TO ACESS MORE THAN ONE PLAYER THROUGH LOGIN
// TODO: ADD KNIGHT, SORCERER AND WARLOCK SCENES, ADD LEVELING SYSTEM

namespace BanishedMain;

public class DataManager
{
    internal void InitFilesystem()
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

            // custom paths
            Directory.CreateDirectory(GDirectories.playerCustomDataPath);
            Directory.CreateDirectory(GDirectories.playerCustomRacePath);
            Directory.CreateDirectory(GDirectories.playerCustomClassPath);
            Directory.CreateDirectory(GDirectories.playerCustomAccoladePath);

            // custom data paths
            Directory.CreateDirectory(GDirectories.playerCustomPersDataPath);
            Directory.CreateDirectory(GDirectories.playerCustomPersClassDataPath);
            Directory.CreateDirectory(GDirectories.playerCustomPersRaceDataPath);
            Directory.CreateDirectory(GDirectories.playerCustomPersAccoladeDataPath);

            // create validation paths
            Directory.CreateDirectory(GDirectories.playerCustomStoryValidationPath);

            if (!File.Exists(GDirectories.playerDBPath))
                File.Create(GDirectories.playerDBPath).Dispose();

            if (!File.Exists(GDirectories.loggerFPath))
                File.Create(GDirectories.loggerFPath).Dispose();

            if (!File.Exists(GDirectories.loggerBFPath))
                File.Create(GDirectories.loggerBFPath).Dispose();

            if (!File.Exists(GDirectories.playerCustomStoryValidationFile))
                File.Create(GDirectories.playerCustomStoryValidationFile).Dispose();
        }
        catch (Exception e)
        {
            throw new SaveFileCreationFailed(e);
        }
    }

    internal bool[] verifyCriticalFiles() // TODO: CHECK FOR CUSTOM STORY
    {
        bool defaultFileIntegrityValid = false;
        bool customFileIntegrityValid = false;

        bool[] validationArr = new bool[2];
        
        // race data //TODO: FINISH THIS TIDY

        try
        {
            if (!File.Exists(GDirectories.playerDataValidationFile))
            {
                defaultFileIntegrityValid = false;
                validationArr[0] = false;
            }
            else
            {
                // check default validation dir
                if (new FileInfo(GDirectories.playerDataValidationFile).Length == 0)
                {
                    defaultFileIntegrityValid = false;
                    validationArr[0] = false;
                }
                else
                {
                    defaultFileIntegrityValid = true;
                    validationArr[0] = true;
                }
            
                // check default dir
                // foreach (var filePath in GDirectories.GDdataFilePaths)
                // {
                //     if (!File.Exists(filePath))
                //     {
                //         defaultFileIntegrityValid = false;
                //         validationArr[0] = false;
                //     }
                //     else if (new FileInfo(filePath).Length == 0)
                //     {
                //         defaultFileIntegrityValid = false;
                //         validationArr[0] = false;
                //     }
                //     else
                //     {
                //         defaultFileIntegrityValid = true;
                //         validationArr[0] = true;
                //     }
                // }
            }
            
            // check validation file
            if (new FileInfo(GDirectories.playerCustomStoryValidationFile).Length == 0)
            {
                customFileIntegrityValid = false;
                validationArr[1] = false;
            }
            else
            {
                customFileIntegrityValid = true;
                validationArr[1] = true;
            }

            if (!defaultFileIntegrityValid && !customFileIntegrityValid)
            {
                validationArr[0] = false;
                validationArr[1] = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return validationArr;
    }

    internal void CreateDefaultFiles()
    {
        // create basic layout
        Directory.CreateDirectory(GDirectories.playerRaceData);
        Directory.CreateDirectory(GDirectories.playerClassData);
        Directory.CreateDirectory(GDirectories.playerAccoladeData);
        
        // create validation directoy
        Directory.CreateDirectory(GDirectories.playerDataValidationPath);

        // create individual race paths
        Directory.CreateDirectory(GDirectories.playerRaceElfData);
        Directory.CreateDirectory(GDirectories.playerRaceHumanData);
        Directory.CreateDirectory(GDirectories.playerRaceOrcData);

        // create individual class paths
        Directory.CreateDirectory(GDirectories.playerClassKnightData);
        Directory.CreateDirectory(GDirectories.playerClassSorcererData);
        Directory.CreateDirectory(GDirectories.playerClassWarlockData);

        // create individual accolade paths
        Directory.CreateDirectory(GDirectories.playerAccoladeWarriorData);
        Directory.CreateDirectory(GDirectories.playerAccoladeScholarData);
        Directory.CreateDirectory(GDirectories.playerAccoladeAcolyteData);

        // create race description directories
        Directory.CreateDirectory(GDirectories.playerRaceElfDescData);
        Directory.CreateDirectory(GDirectories.playerRaceHumanDescData);
        Directory.CreateDirectory(GDirectories.playerRaceOrcDescData);

        // create class description directories
        Directory.CreateDirectory(GDirectories.playerClassKnightDescData);
        Directory.CreateDirectory(GDirectories.playerClassSorcererDescData);
        Directory.CreateDirectory(GDirectories.playerClassWarlockDescData);

        // create accolade description directories
        Directory.CreateDirectory(GDirectories.playerAccoladeWarriorDescData);
        Directory.CreateDirectory(GDirectories.playerAccoladeScholarDescData);
        Directory.CreateDirectory(GDirectories.playerAccoladeAcolyteDescData);

        // create class beginning directories
        Directory.CreateDirectory(GDirectories.playerClassKnightBegData);
        Directory.CreateDirectory(GDirectories.playerClassSorcererBegData);
        Directory.CreateDirectory(GDirectories.playerClassWarlockBegData);

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

        try
        {
            foreach (var filePath in GDirectories.GDdataFilePaths)
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }
            }
        }
        catch (Exception e)
        {
            throw new SaveFileCreationFailed(e);
        }
    }

    internal static void WriteCustomBeginning(string customClassName, string beginningText)
    {
        string customClassBegPath = Path.Join(GDirectories.playerCustomClassPath, @$"{customClassName}/Beginning/");
        string customClassBegPathF = Path.Join(customClassBegPath, "beginning.txt");
        Directory.CreateDirectory(customClassBegPath);

        if (!File.Exists(customClassBegPathF)) // check if file is already there if so, wipe
        {
            File.Create(customClassBegPathF).Dispose();
        }
        else
        {
            File.WriteAllText(customClassBegPathF, String.Empty);
            using (StreamWriter sw = new StreamWriter(customClassBegPathF))
            {
                sw.WriteLine(beginningText);
            }
        }
    }

    internal void OverwriteDefaultBeginnings(string defaultClassName)
    {
        string defaultClassBegPathF =
            Path.Join(GDirectories.playerClassData, @$"{defaultClassName}/Beginning/beginning.txt");

        File.WriteAllText(defaultClassBegPathF, string.Empty);
    }

    internal void WriteNewBeginnings(string beginningText, string defaultClassName = null,
        string customClassName = null)
    {
        if (defaultClassName == null)
        {
            string customClassBegPathF = Path.Join(GDirectories.playerCustomClassPath,
                @$"{customClassName}/Beginning/beginning.txt");
            File.WriteAllText(customClassBegPathF, beginningText);
        }
        else if (customClassName == null)
        {
            string defaultClassBegPathF =
                Path.Join(GDirectories.playerClassData, @$"{defaultClassName}/Beginning/beginning.txt");
            File.WriteAllText(defaultClassBegPathF, beginningText);
        }
    }

    internal static void WriteNewClassDescription(string descriptionText, string defaultClassName = null,
        string customClassName = null)
    {
        if (defaultClassName == null)
        {
            string customClassBegPathF = Path.Join(GDirectories.playerCustomClassPath,
                @$"{customClassName.ToLower()}/Description/{customClassName.ToLower()}.txt");
            File.WriteAllText(customClassBegPathF, descriptionText);
        }
        else if (customClassName == null)
        {
            string defaultClassBegPathF = Path.Join(GDirectories.playerClassData,
                @$"{defaultClassName.ToLower()}/Description/{defaultClassName.ToLower()}.txt");
            File.WriteAllText(defaultClassBegPathF, descriptionText);
        }
    }

    internal static void WriteNewRaceDescription(string descriptionText, string defaultRaceName = null,
        string customRaceName = null)
    {
        if (defaultRaceName == null)
        {
            string customRaceBegPathF = Path.Join(GDirectories.playerCustomRacePath,
                @$"{customRaceName.ToLower()}/Description/{customRaceName.ToLower()}.txt");
            File.WriteAllText(customRaceBegPathF, descriptionText);
        }
        else if (customRaceName == null)
        {
            string defaultRaceBegPathF = Path.Join(GDirectories.playerRaceData,
                @$"{defaultRaceName.ToLower()}/Description/{defaultRaceName.ToLower()}.txt");
            File.WriteAllText(defaultRaceBegPathF, descriptionText);
        }
    }

    internal static void WriteNewAccoladeDescription(string descriptionText, string defaultAccoladeName = null,
        string customAccoladeName = null)
    {
        if (defaultAccoladeName == null)
        {
            string customClassBegPathF = Path.Join(GDirectories.playerCustomAccoladePath,
                @$"{customAccoladeName.ToLower()}/Description/{customAccoladeName}.txt");
            File.WriteAllText(customClassBegPathF, descriptionText);
        }
        else if (customAccoladeName == null)
        {
            string defaultClassBegPathF = Path.Join(GDirectories.playerAccoladeData,
                @$"{defaultAccoladeName.ToLower()}/Description/{defaultAccoladeName}.txt");
            File.WriteAllText(defaultClassBegPathF, descriptionText);
        }
    }

    internal static void SaveCustomClass(PlayerClass pl_class)
    {
        // create class directory structure
        string customClassPath = Path.Join(GDirectories.playerCustomClassPath, pl_class.className.ToLower());
        string customClassDescriptionPath = Path.Join(customClassPath, @"Description/");
        string customClassBeginningPath = Path.Join(customClassPath, @"Beginning/");

        string customClassDescriptionF = Path.Join(customClassDescriptionPath, @$"{pl_class.className.ToLower()}.txt");
        string customClassBeginningF = Path.Join(customClassBeginningPath, $@"beginning.txt");

        Directory.CreateDirectory(GDirectories.playerCustomClassPath);
        Directory.CreateDirectory(customClassDescriptionPath);
        Directory.CreateDirectory(customClassBeginningPath);

        if (!File.Exists(customClassDescriptionF))
        {
            File.Create(customClassDescriptionF).Dispose();
        }

        if (!File.Exists(customClassBeginningF))
        {
            File.Create(customClassBeginningF).Dispose();
        }

        // create saving mechanism
        string customClassDataFilePath =
            Path.Join(GDirectories.playerCustomPersClassDataPath, $@"{pl_class.className.ToLower()}/");
        string customClassDataFileF = Path.Join(customClassDataFilePath, $@"{pl_class.className.ToLower()}_data.txt");

        Directory.CreateDirectory(customClassDataFilePath);

        File.Create(customClassDataFileF).Dispose();

        using (StreamWriter sw = new StreamWriter(customClassDataFileF))
        {
            sw.WriteLine($"NAME: {pl_class.className}");
            sw.WriteLine($"HEALTH: {pl_class.classHealth}");
            sw.WriteLine($"FAITH: {pl_class.classFaith}");
            sw.WriteLine($"AGILITY: {pl_class.classAgility}");
            //sw.WriteLine($"");
        }

        // save description
        using (StreamWriter sw = new StreamWriter(customClassDescriptionF))
        {
            sw.WriteLine($"DESCRIPTION:{pl_class.classDescription}");
        }
    }

    internal static void SaveCustomRace(PlayerRace pl_race)
    {
        // create class directory structure
        string customRacePath = Path.Join(GDirectories.playerCustomRacePath, pl_race.raceName.ToLower());
        string customRaceDescriptionPath = Path.Join(customRacePath, @"Description/");
        string customRaceBeginningPath = Path.Join(customRacePath, @"Beginning/");

        string customRaceDescriptionF = Path.Join(customRaceDescriptionPath, @$"{pl_race.raceName.ToLower()}.txt");
        string customRaceBeginningF = Path.Join(customRaceBeginningPath, $@"beginning.txt");

        Directory.CreateDirectory(GDirectories.playerCustomRacePath);
        Directory.CreateDirectory(customRaceDescriptionPath);
        Directory.CreateDirectory(customRaceBeginningPath);

        if (!File.Exists(customRaceDescriptionF))
        {
            File.Create(customRaceDescriptionF).Dispose();
        }

        if (!File.Exists(customRaceBeginningF))
        {
            File.Create(customRaceBeginningF).Dispose();
        }

        string customRaceDataFilePath = Path.Join(GDirectories.playerCustomPersRaceDataPath, $@"{pl_race.raceName}");
        string customRaceDataFileF = Path.Join(customRaceDataFilePath, $@"{pl_race.raceName}_data.txt");

        Directory.CreateDirectory(customRaceDataFilePath);

        File.Create(customRaceDataFileF).Dispose();

        using (StreamWriter sw = new StreamWriter(customRaceDataFileF))
        {
            sw.WriteLine($"NAME: {pl_race.raceName}");
            sw.WriteLine($"DESCRIPTION: {pl_race.raceDescription}");

            if (!pl_race.raceProficencies.Any())
            {
                int counter = 0;
                foreach (var VARIABLE in pl_race.draceProficencies)
                {
                    sw.WriteLine($"PROFICIENCY COUNT: {pl_race.draceProficencies.Count().ToString()}");
                    sw.WriteLine($"PROFICIENCY {counter}: {pl_race.draceProficencies[counter].ToString()}");
                    counter++;
                }
            }
        }
    }

    internal static void SaveCustomAccolade(PlayerAccolade pl_accolade)
    {
        // create class directory structure
        string customAccoladePath =
            Path.Join(GDirectories.playerCustomAccoladePath, pl_accolade.accoladeName.ToLower());
        string customAccoladeDescriptionPath = Path.Join(customAccoladePath, @"Description/");
        string customAccoladeBeginningPath = Path.Join(customAccoladePath, @"Beginning/");

        string customAccoladeDescriptionF =
            Path.Join(customAccoladeDescriptionPath, @$"{pl_accolade.accoladeName.ToLower()}.txt");
        string customAccoladeBeginningF = Path.Join(customAccoladePath, $@"beginning.txt");

        Directory.CreateDirectory(GDirectories.playerCustomRacePath);
        Directory.CreateDirectory(customAccoladeDescriptionPath);
        Directory.CreateDirectory(customAccoladeBeginningPath);

        if (!File.Exists(customAccoladeDescriptionF))
        {
            File.Create(customAccoladeDescriptionF).Dispose();
        }

        if (!File.Exists(customAccoladeBeginningF))
        {
            File.Create(customAccoladeBeginningF).Dispose();
        }

        string customAccoladeDataFilePath =
            Path.Join(GDirectories.playerCustomPersAccoladeDataPath, $@"{pl_accolade.accoladeName}");
        string customAccoladeDataFileF = Path.Join(customAccoladeDataFilePath, $@"{pl_accolade.accoladeName}_data.txt");

        Directory.CreateDirectory(customAccoladeDataFilePath);

        File.Create(customAccoladeDataFileF).Dispose();

        using (StreamWriter sw = new StreamWriter(customAccoladeDataFileF))
        {
            sw.WriteLine($"NAME: {pl_accolade.accoladeName}");
            sw.WriteLine($"DESCRIPTION: {pl_accolade.accoladeDescription}");
        }
    }

    internal static void SaveCustomScene(Scene scene)
    {
        
        string customSceneDir = Path.Join(GDirectories.playerCustomStoryPath, $@"{scene.classAssociated.className}/Scenes/Scene_{scene.sceneName.ToString()}/");
        string customSceneFile = Path.Join(customSceneDir, @$"scene{scene.sceneNumber.ToString()}.txt");
        string customSceneConsequenceFile = Path.Join(customSceneDir, $@"scene{scene.sceneNumber.ToString()}consequences.txt");

        Directory.CreateDirectory(customSceneDir);
        
        File.Create(customSceneFile).Dispose();
        File.Create(customSceneConsequenceFile).Dispose();
        
        // write template
        using (StreamWriter sw = new StreamWriter(customSceneFile))
        {
            sw.WriteLine($"SCENE{scene.sceneNumber.ToString()}");

            int promptAt = 0;
            foreach (var prompt in scene.prompts)
            {
                sw.WriteLine($"PROMPT{promptAt}");
                sw.WriteLine(prompt);
                sw.WriteLine($"PROMPT{promptAt}END");

                promptAt++;
            }
            
            sw.WriteLine($"SCENE{scene.sceneNumber.ToString()}END");
        }

        using (StreamWriter sw = new StreamWriter(customSceneConsequenceFile))
        {
            sw.WriteLine($"SCENE{scene.sceneNumber.ToString()}");

            int consequenceAt = 0;

            foreach (var consequence in scene.consequences)
            {
                sw.WriteLine($"CONSEQUENCE{consequenceAt}");
                sw.WriteLine(consequence);
                sw.WriteLine($"CONSEQUENCE{consequenceAt}END");

                consequenceAt++;
            }
            
            sw.WriteLine($"SCENE{scene.sceneNumber.ToString()}END");
        }
        


    }

    internal static void EtchStoryValidation()
    {
        Directory.CreateDirectory(GDirectories.playerCustomStoryValidationPath);
        File.Create(GDirectories.playerCustomStoryValidationFile).Dispose();

        string validationFile = GDirectories.playerCustomStoryValidationFile;
        // File.Create(validationFile).Dispose();
        using (StreamWriter sw = new StreamWriter(validationFile))
        {
            sw.WriteLine("0");
        }
    }

    internal static void EtchDefaultStoryValidation()
    {
        string validationFile = GDirectories.playerDataValidationFile;

        using (StreamWriter sw = new StreamWriter(validationFile))
        {
            sw.WriteLine("0");
        }
    }

    internal void WriteDefaultData() //TODO: FIX
    {
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
        
        EtchDefaultStoryValidation();
    }

    internal void PurgeDataDir()
    {
        DirectoryInfo di = new DirectoryInfo(GDirectories.dataPath);

        foreach (var file in di.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }
    
    internal void PurgeDefaultSceneDir()
    {
        DirectoryInfo di = new DirectoryInfo(GDirectories.playerStoryPath);
        foreach (var file in di.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }

    internal void PurgeCustomSceneDir()
    {
        DirectoryInfo di = new DirectoryInfo(GDirectories.playerCustomStoryPath);
        foreach (var file in di.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in di.GetDirectories())
        {
            dir.Delete(true);
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

    // validation path
    public const string playerDataValidationPath = @"../../../Data/Validation";
    public const string playerDataValidationFile = @"../../../Data/Validation/validation.txt";
    
    // player construction data directories
    public const string playerRaceData = @"../../../Data/Race/";
    public const string playerClassData = @"../../../Data/Class/";
    public const string playerAccoladeData = @"../../../Data/Accolade/";

    public const string playerRaceElfData = @"../../../Data/Race/Elf/";
    public const string playerRaceHumanData = @"../../../Data/Race/Human/";
    public const string playerRaceOrcData = @"../../../Data/Race/Orc/";

    public const string playerClassKnightData = @"../../../Data/Class/Knight/";
    public const string playerClassSorcererData = @"../../../Data/Class/Sorcerer/";
    public const string playerClassWarlockData = @"../../../Data/Class/Warlock/";

    public const string playerAccoladeWarriorData = @"../../../Data/Accolade/Warrior/";
    public const string playerAccoladeScholarData = @"../../../Data/Accolade/Scholar/";
    public const string playerAccoladeAcolyteData = @"../../../Data/Accolade/Acolyte/";

    // DESCRIPTIONS
    public const string playerRaceElfDescData = @"../../../Data/Race/Elf/Description/";
    public const string playerRaceHumanDescData = @"../../../Data/Race/Human/Description/";
    public const string playerRaceOrcDescData = @"../../../Data/Race/Orc/Description/";

    public const string playerClassKnightDescData = @"../../../Data/Class/Knight/Description/";
    public const string playerClassSorcererDescData = @"../../../Data/Class/Sorcerer/Description/";
    public const string playerClassWarlockDescData = @"../../../Data/Class/Warlock/Description/";

    public const string playerAccoladeWarriorDescData = @"../../../Data/Accolade/Warrior/Description/";
    public const string playerAccoladeScholarDescData = @"../../../Data/Accolade/Scholar/Description/";
    public const string playerAccoladeAcolyteDescData = @"../../../Data/Accolade/Acolyte/Description/";

    // player race data files
    public const string playerRaceHumanDataF = @"../../../Data/Race/Human/Description/human.txt";
    public const string playerRaceElfDataF = @"../../../Data/Race/Elf/Description/elf.txt";
    public const string playerRaceOrcDataF = @"../../../Data/Race/Orc/Description/orc.txt";

    // player class data files
    public const string playerClassKnightDataF = @"../../../Data/Class/Knight/Description/knight.txt";
    public const string playerClassSorcererDataF = @"../../../Data/Class/Sorcerer/Description/sorcerer.txt";
    public const string playerClassWarlockDataF = @"../../../Data/Class/Warlock/Description/warlock.txt";

    // player accolade data files
    public const string playerAccoladeWarriorDataF = @"../../../Data/Accolade/Warrior/Description/warrior.txt";
    public const string playerAccoladeScholarDataF = @"../../../Data/Accolade/Scholar/Description/scholar.txt";
    public const string playerAccoladeAcolyteDataF = @"../../../Data/Accolade/Acolyte/Description/acolyte.txt";

    // BEGINNINGS
    public const string playerClassKnightBegData = @"../../../Data/Class/Knight/Beginning/";
    public const string playerClassSorcererBegData = @"../../../Data/Class/Sorcerer/Beginning/";
    public const string playerClassWarlockBegData = @"../../../Data/Class/Warlock/Beginning/";

    public const string playerKnightStoryDataBeginningF = @"../../../Data/Class/Knight/Beginning/beginning.txt";
    public const string playerSorcererStoryDataBeginningF = @"../../../Data/Class/Sorcerer/Beginning/beginning.txt";
    public const string playerWarlockStoryDataBeginningF = @"../../../Data/Class/Warlock/Beginning/beginning.txt";

    // player class STORY data path
    public const string playerStoryPath = @"../../../Datal/Story";
    
    public const string playerKnightStoryDataPath = @"../../../Data/Story/Knight";
    public const string playerSorcererStoryDataPath = @"../../../Data/Story/Sorcerer";
    public const string playerWarlockStoryDataPath = @"../../../Data/Story/Warlock";


    /// <summary>
    /// /////////////////////////////TODO: RESTRUCTURE FILE SYSTEM
    /// </summary>
    // scene paths
    public const string playerKnightScenePath = @"../../../Data/Story/Knight/Scenes/";

    public const string playerSorcererScenePath = @"../../../Data/Story/Sorcerer/Scenes";
    public const string playerWarlockScenePath = @"../../../Data/Story/Warlock/Scenes/";

    // inidivdual scene folder
    public const string playerKnightSceneOnePath = @"../../../Data/Story/Knight/Scenes/Scene_One/";
    public const string playerKnightSceneTwoPath = @"../../../Data/Story/Knight/Scenes/Scene_Two/";
    public const string playerKnightSceneThreePath = @"../../../Data/Story/Knight/Scenes/Scene_Three/";

    public const string playerSorcererSceneOnePath = @"../../../Data/Story/Sorcerer/Scenes/Scene_One/";
    public const string playerSorcererSceneTwoPath = @"../../../Data/Story/Sorcerer/Scenes/Scene_Two/";
    public const string playerSorcererSceneThreePath = @"../../../Data/Story/Sorcerer/Scenes/Scene_Three/";

    public const string playerWarlockSceneOnePath = @"../../../Data/Story/Warlock/Scenes/Scene_One/";
    public const string playerWarlockSceneTwoPath = @"../../../Data/Story/Warlock/Scenes/Scene_Two/";
    public const string playerWarlockSceneThreePath = @"../../../Data/Story/Warlock/Scenes/Scene_Three/";

    // individual scene files
    public const string playerKnightStoryDataSceneOneF = @"../../../Data/Story/Knight/Scenes/Scene_One/sceneone.txt";
    public const string playerKnightStoryDataSceneTwoF = @"../../../Data/Story/Knight/Scenes/Scene_Two/scenetwo.txt";

    public const string playerKnightStoryDataSceneThreeF =
        @"../../../Data/Story/Knight/Scenes/Scene_Three/scenethree.txt";

    public const string playerSorcererStoryDataSceneOneF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_One/sceneone.txt";

    public const string playerSorcererStoryDataSceneTwoF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_Two/scenetwo.txt";

    public const string playerSorcererStoryDataSceneThreeF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_Three/scenethree.txt";


    public const string playerWarlockStoryDataSceneOneF = @"../../../Data/Story/Warlock/Scenes/Scene_One/sceneone.txt";
    public const string playerWarlockStoryDataSceneTwoF = @"../../../Data/Story/Warlock/Scenes/Scene_Two/scenetwo.txt";

    public const string playerWarlockStoryDataSceneThreeF =
        @"../../../Data/Story/Warlock/Scenes/Scene_Three/scenethree.txt";

    // individual option files
    public const string playerKnightStoryDataSceneOneOptionsF =
        @"../../../Data/Story/Knight/Scenes/Scene_One/sceneoneoptions.txt";

    public const string playerKnightStoryDataSceneTwoOptionsF =
        @"../../../Data/Story/Knight/Scenes/Scene_Two/scenetwooptions.txt";

    public const string playerKnightStoryDataSceneThreeOptionsF =
        @"../../../Data/Story/Knight/Scenes/Scene_Three/sceneThreeoptions.txt";

    public const string playerSorcererStoryDataSceneOneOptionsF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_One/sceneoneoptions.txt";

    public const string playerSorcererStoryDataSceneTwoOptionsF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_Two/scenetwooptions.txt";

    public const string playerSorcererStoryDataSceneThreeOptionsF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_Three/sceneThreeoptions.txt";

    public const string playerWarlockStoryDataSceneOneOptionsF =
        @"../../../Data/Story/Warlock/Scenes/Scene_One/sceneoneoptions.txt";

    public const string playerWarlockStoryDataSceneTwoOptionsF =
        @"../../../Data/Story/Warlock/Scenes/Scene_Two/scenetwooptions.txt";

    public const string playerWarlockStoryDataSceneThreeOptionsF =
        @"../../../Data/Story/Warlock/Scenes/Scene_Three/sceneThreeoptions.txt";

    // individual consequence files
    public const string playerKnightStoryDataSceneOneConsequencesF =
        @"../../../Data/Story/Knight/Scenes/Scene_One/sceneoneconsequences.txt";

    public const string playerKnightStoryDataSceneTwoConsequencesF =
        @"../../../Data/Story/Knight/Scenes/Scene_Two/scenetwoconsequences.txt";

    public const string playerKnightStoryDataSceneThreeConsequencesF =
        @"../../../Data/Story/Knight/Scenes/Scene_Three/scenethreeconsequences.txt";

    public const string playerSorcererStoryDataSceneOneConsequencesF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_One/sceneoneconsequences.txt";

    public const string playerSorcererStoryDataSceneTwoConsequencesF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_Two/scenetwoconsequences.txt";

    public const string playerSorcererStoryDataSceneThreeConsequencesF =
        @"../../../Data/Story/Sorcerer/Scenes/Scene_Three/scenethreeconsequences.txt";

    public const string playerWarlockStoryDataSceneOneConsequencesF =
        @"../../../Data/Story/Warlock/Scenes/Scene_One/sceneoneconsequences.txt";

    public const string playerWarlockStoryDataSceneTwoConsequencesF =
        @"../../../Data/Story/Warlock/Scenes/Scene_Two/scenetwoconsequences.txt";

    public const string playerWarlockStoryDataSceneThreeConsequencesF =
        @"../../../Data/Story/Warlock/Scenes/Scene_Three/scenethreeconsequences.txt";

    // CUSTOM FILE DIRECTORY
    public const string playerCustomDataPath = @"../../../Data/Custom";
    public const string playerCustomRacePath = @"../../../Data/Custom/Race";
    public const string playerCustomClassPath = @"../../../Data/Custom/Class";
    public const string playerCustomAccoladePath = @"../../../Data/Custom/Accolade";
    public const string playerCustomStoryPath = @"../../../Data/Custom/Story";

    public const string playerCustomPersDataPath = @"../../../Data/Custom/Data_Persistent";
    public const string playerCustomPersClassDataPath = @"../../../Data/Custom/Data_Persistent/Class";
    public const string playerCustomPersRaceDataPath = @"../../../Data/Custom/Data_Persistent/Race";
    public const string playerCustomPersAccoladeDataPath = @"../../../Data/Custom/Data_Persistent/Accolade";

    public const string playerCustomStoryValidationPath = @"../../../Data/Custom/Validation/";
    public const string playerCustomStoryValidationFile = @"../../../Data/Custom/Validation/validation.txt";

    public static readonly string[] GDdataFilePaths = new string[]
    {
        GDirectories.playerDataValidationFile, GDirectories.playerRaceHumanDataF, GDirectories.playerRaceElfDataF, GDirectories.playerRaceOrcDataF,
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

public static class GGlobals
{
    public static Player NewPlayer;
    public static Player LoadPlayer;
    public static bool defaultClassesUsed = true;

    public static bool defaultStoryExists = false;
    public static bool customStoryExists = false;
    public static bool newStory = false;

    public static List<PlayerClass> activeCustomClasses = new List<PlayerClass>();
    public static List<PlayerRace> activeCustomRaces = new List<PlayerRace>();
    public static List<PlayerAccolade> activeCustomAccolades = new List<PlayerAccolade>();


}