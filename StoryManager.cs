using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BanishedMain;

public class StoryManager
{
    private readonly List<DefaultPlayerRace> _defaultPlayerRaces;
    private readonly List<DefaultPlayerClass> _defaultPlayerClasses;
    private readonly List<DefaultPlayerAccolade> _defaultPlayerAccolades;

    private List<PlayerRace> userMadeRaces = new List<PlayerRace>();
    private List<PlayerClass> userMadeClasses = new List<PlayerClass>();
    private List<PlayerAccolade> userMadeAccolades = new List<PlayerAccolade>();
    private static List<RaceProficiency> userMadeRaceProficiencies = new List<RaceProficiency>();


    public StoryManager()
    {
        // populate lists
        _defaultPlayerRaces = Enum.GetValues(typeof(DefaultPlayerRace)).Cast<DefaultPlayerRace>().ToList();
        _defaultPlayerRaces.Remove(0);
        _defaultPlayerClasses = Enum.GetValues(typeof(DefaultPlayerClass)).Cast<DefaultPlayerClass>().ToList();
        _defaultPlayerClasses.Remove(0);
        _defaultPlayerAccolades = Enum.GetValues(typeof(DefaultPlayerAccolade)).Cast<DefaultPlayerAccolade>().ToList();
        _defaultPlayerAccolades.Remove(0);
    }

    public List<PlayerClass> CreateClasses(bool useDefaults = false)
    {
        CosmeticMenu.writeTitleCosmetics("CLASS CREATOR");
        if (useDefaults)
        {
            Sys.WSMNL("...");
            Sys.WSMNL("Using default classes...");
            Sys.WSM("> ");
            Console.ReadLine();
        }
        else
        {
            // create new classes
            Sys.WSMNL("Welcome to class creator!");

            int userClassCountFinal = 0;
            bool classCountInputValid = false;
            while (!classCountInputValid)
            {
                Sys.WSMNL("Specify the number of classes you want: ");
                Sys.WSM("> ");

                string userClassCount = Console.ReadLine();

                try
                {
                    userClassCountFinal = int.Parse(userClassCount);

                    if (userClassCountFinal == 0)
                    {
                        Error.WEMNL("MUST BE AT LEAST ONE CLASS");
                    }
                    else if (userClassCountFinal > 10)
                    {
                        Error.WEMNL("TOO MANY CLASSES!");
                    }
                    else
                    {
                        classCountInputValid = true;
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }

            for (int i = 0; i < userClassCountFinal; i++)
            {
                Sys.WSM(">>> ");
                Console.ReadLine();

                Sys.WSMNL("Specify name of class: ");
                Sys.WSM("> ");
                string userClassName = Console.ReadLine().ToUpper();
                
                Sys.WSM(">>> ");
                
                bool defaultHealthUsed = false;
                bool userValidHealthInput = false;
                int userClassHealthMultiplyerFinal = 0;

                while (!userValidHealthInput)
                {
                    Sys.WSMNL("Specify health multiplyer [default 100] [D for default]");
                    Sys.WSM("> ");
                    string userClassHealthMultiplyer = Console.ReadLine();

                    try
                    {
                        userClassHealthMultiplyerFinal = int.Parse(userClassHealthMultiplyer);

                        if (userClassHealthMultiplyerFinal < 0)
                        {
                            Error.WEMNL("VALUE MUST BE GREATOR THAN 0");
                        }
                        else if (userClassHealthMultiplyerFinal > 100)
                        {
                            Error.WEMNL("VALUE MUST BE BELOW 100");
                        }
                        else if (string.Equals(userClassHealthMultiplyer, "D") || string.Equals(userClassHealthMultiplyer, "d"))
                        {
                            defaultHealthUsed = true;
                        }
                        else
                        {
                            userValidHealthInput = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Error.WEMNL("NO VALID INPUT!");
                    }
                }

                Sys.WSM(">>> ");
                Console.ReadLine();

                bool userValidFaithInput = false;
                bool defaultFaithUsed = false;
                int userClassFaithMultiplyerFinal = 0;

                while (!userValidFaithInput)
                {
                    Sys.WSMNL("Specify faith multiplyer [default 40] [D for default]");
                    Sys.WSM("> ");

                    string userClassFaithMultiplyer = Console.ReadLine();

                    try
                    {
                        userClassFaithMultiplyerFinal = int.Parse(userClassFaithMultiplyer);

                        if (userClassFaithMultiplyerFinal < 0)
                        {
                            Error.WEMNL("VALUE MUST BE GREATOR THAN 0");
                        }
                        else if (userClassFaithMultiplyerFinal > 100)
                        {
                            Error.WEMNL("VALUE MUST BE BELOW 100");
                        }
                        else if (string.Equals(userClassFaithMultiplyer, "D") || string.Equals(userClassFaithMultiplyer, "d"))
                        {
                            defaultFaithUsed = true;
                        }
                        else
                        {
                            userValidFaithInput = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Error.WEMNL("NO VALID INPUT!");
                    }
                }

                Sys.WSM(">>> ");
                Console.ReadLine();

                bool userValidAgilityInput = false;
                bool defaultAgilityUsed = false;
                int userClassAgilityMultiplyerFinal = 0;

                while (!userValidAgilityInput)
                {
                    Sys.WSMNL("Specify agility multiplayer [default4 40] [D for default]");
                    Sys.WSM("> ");

                    string userClassAgilityMultiplyer = Console.ReadLine();

                    try
                    {
                        userClassAgilityMultiplyerFinal = int.Parse(userClassAgilityMultiplyer);

                        if (userClassAgilityMultiplyerFinal < 0)
                        {
                            Error.WEMNL("VALUE MUST BE GREATOR THAN 0");
                        }
                        else if (userClassAgilityMultiplyerFinal > 100)
                        {
                            Error.WEMNL("VALUE MUST BE BELOW 100");
                        }
                        else if (string.Equals(userClassAgilityMultiplyer, "D") || string.Equals(userClassAgilityMultiplyer, "d"))
                        {
                            defaultAgilityUsed = true;
                        }
                        else
                        {
                            userValidAgilityInput = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Error.WEMNL("NO VALID INPUT!");
                    }
                }

                Sys.WSM(">>> ");
                Console.ReadLine();

                Sys.WSMNL("Data Collated!");

                Sys.WSMNL("Formulate class description [ENTER when done]: ");
                Sys.WSMNL(">");

                string userClassDescription = Console.ReadLine();

                // observe default values
                if (defaultHealthUsed)
                    userClassHealthMultiplyerFinal = 100;
                if (defaultFaithUsed)
                    userClassFaithMultiplyerFinal = 40;
                if (defaultAgilityUsed)
                    userClassAgilityMultiplyerFinal = 40;

                //TODO: INVENTORY
                
                // make class
                PlayerClass newPlayerClass = new PlayerClass(userClassName, userClassHealthMultiplyerFinal,
                    userClassFaithMultiplyerFinal, userClassAgilityMultiplyerFinal, userClassDescription);
                
                userMadeClasses.Add(newPlayerClass);
                            
                // SAVE ALL DATA
                DataManager.SaveCustomClass(newPlayerClass);
                
                DataManager.WriteNewClassDescription(newPlayerClass.classDescription, null, newPlayerClass.className);
            }
            
            Sys.WSMNL(">>>");
            Sys.WSMNL("Class Creation Succesful!");
            Sys.WSMNL("Created Classes: ");
            int counter = 1;
            foreach (var userMadeClass in userMadeClasses)
            {
                Sys.WSMNL($"{counter}. {userMadeClass.className}");
                Sys.WSMNL($"HEALTH: {userMadeClass.classHealth}");
                Sys.WSMNL($"FAITH: {userMadeClass.classFaith}");
                Sys.WSMNL($"AGILITY: {userMadeClass.classAgility}");
                Sys.WSMNL($"DESCRIPTION: ");
                Sys.WSMNL(userMadeClass.classDescription);
                counter++;
            }

            GGlobals.activeCustomClasses = userMadeClasses;
        }
        return userMadeClasses;
    }
    
    public void CreateBeginning(DataManager DM)
    {
        CosmeticMenu.writeTitleCosmetics("BEGINNING CREATOR");

        bool classSelectionValid = false;

        while (!classSelectionValid)
        {
            Sys.WSMNL("SELECT CLASS: ");
            Sys.WSM("\n");

            int counter = 1;
            
            // check which classes are used
            if (GGlobals.defaultClassesUsed)
            {

                foreach (var playerClass in _defaultPlayerClasses)
                {
                    Sys.WSMNL($"{counter}. {playerClass}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Sys.WSM("> ");
                
                string userClassSelection = Console.ReadLine();

                try /////////////////////////////////
                {
                    Enum.TryParse(userClassSelection, out DefaultPlayerClass pl_class);
                    if (_defaultPlayerClasses.IndexOf(pl_class) == -1)
                    {
                        Error.WEMNL("NO VALID INPUT");
                    }
                    else // class is valid
                    {
                        DM.OverwriteDefaultBeginnings(pl_class.ToString());
                        
                        Sys.WSMNL($"ENTER NEW BEGINNING FOR {pl_class.ToString()}");
                        Sys.WSM("> ");
                        
                        string newBeginning = Console.ReadLine();
                        
                        DM.WriteNewBeginnings(newBeginning, pl_class.ToString());
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("BAD INPUT!");
                }
            }
            else // FOR CUSTOM CLASSES
            {
                counter = 0;
                foreach (var playerClass in userMadeClasses)
                {
                    Sys.WSMNL($"{counter}. {playerClass.className.ToLower()}");
                    counter++;
                }

                string userClassSelection = Console.ReadLine();

                try
                {
                    int userClassSelectionIndex = int.Parse(userClassSelection);

                    PlayerClass selectedClass = userMadeClasses[userClassSelectionIndex];
                    
                    Sys.WSMNL($"WRTIE NEW BEGINNING FOR {selectedClass.className.ToLower()}");
                    Sys.WSMNL("> ");

                    string newClassBeginning = Console.ReadLine();
                    
                    DataManager.WriteCustomBeginning(selectedClass.className.ToLower(), newClassBeginning);
                    
                    classSelectionValid = true;
                }
                catch (Exception e)
                {
                    Error.WEMNL("BAD INPUT!");
                }
            }
        }
    }

    public bool VerifyStory()
    {
        if (!Directory.Exists(GDirectories.dataPath))
        {
            return false;
        }
        else
        {
            foreach (var filePath in GDirectories.GDdataFilePaths)
            {
                if (!Directory.Exists(filePath) || !File.Exists(filePath))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void RestoreDefault(DataManager DM)
    {
        DM.PurgeDataDir();
        DM.CreateDefaultFiles();
        DM.WriteDefaultData();
    }

    public List<PlayerAccolade> CreateAccolades(bool useDefaults = false)
    {
        CosmeticMenu.writeTitleCosmetics("ACCOLADE CREATOR");
        if (useDefaults)
        {
            Sys.WSMNL("...");
            Sys.WSMNL("Using default accolades...");
            Sys.WSM("> ");
            Console.ReadLine();
        }
        else
        {
            Sys.WSMNL("Welcome to accolade creator!");
            
            int userAccoladeCountFinal = 0;
            bool AccoladeCountInputValid = false;
            while (!AccoladeCountInputValid)
            {
                Sys.WSMNL("Specify the number of Accolades to be included: ");
                Sys.WSM("> ");

                string userAccoladeCount = Console.ReadLine();

                try
                {
                    userAccoladeCountFinal = int.Parse(userAccoladeCount);

                    if (userAccoladeCountFinal == 0)
                    {
                        Error.WEMNL("MUST BE AT LEAST ONE ACCOLADE");
                    }
                    else if (userAccoladeCountFinal > 10)
                    {
                        Error.WEMNL("TOO MANY ACCOLADES!");
                    }
                    else
                    {
                        AccoladeCountInputValid = true;
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }


            for (int i = 0; i < userAccoladeCountFinal; i++)
            {
                Sys.WSM(">>> ");
                Console.ReadLine();

                Sys.WSMNL("Specify name of accolade: ");
                Sys.WSM("> ");
                string userAccoladeName = Console.ReadLine().ToUpper();

                Sys.WSMNL("\n");
                
                Sys.WSMNL("Formulate accolade description ENTER WHEN DONE: ");
                Sys.WSM("> ");

                string userAccoladeDescription = Console.ReadLine().ToUpper();
                
                PlayerAccolade newPlayerAccolade = new PlayerAccolade(userAccoladeName, userAccoladeDescription);
                userMadeAccolades.Add(newPlayerAccolade);
                
                DataManager.SaveCustomAccolade(newPlayerAccolade);
                
                DataManager.WriteNewAccoladeDescription(newPlayerAccolade.accoladeDescription, null, newPlayerAccolade.accoladeName);
            }

            GGlobals.activeCustomAccolades = userMadeAccolades;
        }

        return userMadeAccolades;
    }
    
    public List<PlayerRace> CreateRaces(bool useDefaults = false)
    {
        CosmeticMenu.writeTitleCosmetics("RACE CREATOR");
        if (useDefaults)
        {
            Sys.WSMNL("...");
            Sys.WSMNL("Using default races...");
            Sys.WSM("> ");
            Console.ReadLine();
        }
        else
        {
            // create new classes
            Sys.WSMNL("Welcome to race creator!");

            int userRaceCountFinal = 0;
            bool raceCountInputValid = false;
            while (!raceCountInputValid)
            {
                Sys.WSMNL("Specify the number of races to be included: ");
                Sys.WSM("> ");

                string userClassCount = Console.ReadLine();

                try
                {
                    userRaceCountFinal = int.Parse(userClassCount);

                    if (userRaceCountFinal == 0)
                    {
                        Error.WEMNL("MUST BE AT LEAST ONE RACE");
                    }
                    else if (userRaceCountFinal > 10)
                    {
                        Error.WEMNL("TOO MANY RACES!");
                    }
                    else
                    {
                        raceCountInputValid = true;
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }


            for (int i = 0; i < userRaceCountFinal; i++)
            {
                Sys.WSM(">>> ");
                Console.ReadLine();

                Sys.WSMNL("Specify name of race: ");
                Sys.WSM("> ");
                string userRaceName = Console.ReadLine().ToUpper();

                Sys.WSMNL("\n");

                bool userRaceProficiencySelectionInputValid = false;
                bool defaultRaceProficencyUsed = false;
                while (!userRaceProficiencySelectionInputValid)
                {
                    Sys.WSMNL("Create new proficenceis [Y/N]: ");
                    
                    string userRaceProficencySelectionInput = Console.ReadLine();
                    
                    switch (userRaceProficencySelectionInput)
                    {
                        case "Y":
                        case "y":
                            StoryManager.CreateRaceProficiencies();
                            userRaceProficiencySelectionInputValid = true;
                            break;
                        case "N":
                        case "n":
                            StoryManager.CreateRaceProficiencies(true);
                            defaultRaceProficencyUsed = true;
                            userRaceProficiencySelectionInputValid = true;
                            break;
                        default:
                            Error.WEMNL("NO VALID INPUT!");
                            break;
                    }
                }
                
                Sys.WSMNL(">>> ");
                Console.ReadLine();

                // bool userRaceReputationInputValid = false;
                // bool defaultRaceReputationUsed = false;
                // while (!userRaceReputationInputValid)
                // {
                //     Sys.WSMNL("Create new reputation [Y/N]: ");
                //     
                //     string userRaceProficencySelectionInput = Console.ReadLine();
                //     
                //     switch (userRaceProficencySelectionInput)
                //     {
                //         case "Y":
                //         case "y":
                //             StoryManager.CreateRaceProficiencies();
                //             userRaceProficiencySelectionInputValid = true;
                //             break;
                //         case "N":
                //         case "n":
                //             StoryManager.CreateRaceProficiencies(true);
                //             defaultRaceProficencyUsed = true;
                //             userRaceProficiencySelectionInputValid = true;
                //             break;
                //         default:
                //             Error.WEMNL("NO VALID INPUT!");
                //             break;
                //     }
                // }
                

                Sys.WSMNL("Formulate race description [ENTER when done]: ");
                Sys.WSMNL(">");

                string userRaceDescription = Console.ReadLine();

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // while (!userValidHealthInput)
                // {
                //     Sys.WSMNL("Specify health multiplyer [default 100] [D for default]");
                //     Sys.WSM("> ");
                //     string userClassHealthMultiplyer = Console.ReadLine();
                //
                //     try
                //     {
                //         userClassHealthMultiplyerFinal = int.Parse(userClassHealthMultiplyer);
                //
                //         if (userClassHealthMultiplyerFinal < 0)
                //         {
                //             Error.WEMNL("VALUE MUST BE GREATOR THAN 0");
                //         }
                //         else if (userClassHealthMultiplyerFinal > 100)
                //         {
                //             Error.WEMNL("VALUE MUST BE BELOW 100");
                //         }
                //         else if (string.Equals(userClassHealthMultiplyer, "D") ||
                //                  string.Equals(userClassHealthMultiplyer, "d"))
                //         {
                //             defaultHealthUsed = true;
                //         }
                //         else
                //         {
                //             userValidHealthInput = true;
                //         }
                //     }
                //     catch (Exception e)
                //     {
                //         Error.WEMNL("NO VALID INPUT!");
                //     }
                // }
                PlayerRace newPlayerRace = new PlayerRace();
                if (defaultRaceProficencyUsed)
                {
                    var userRaceProficencies = Enum.GetValues(typeof(DefaultRaceProficiency)).Cast<DefaultRaceProficiency>().ToList();
                    newPlayerRace = new PlayerRace(userRaceName, userRaceDescription, userRaceProficencies);
                }
                else
                {
                    var userRaceProficencies = Enum.GetValues(typeof(RaceProficiency)).Cast<RaceProficiency>().ToList();
                    newPlayerRace = new PlayerRace(userRaceName, userRaceDescription, userRaceProficencies);
                }
                userMadeRaces.Add(newPlayerRace);
                
                DataManager.SaveCustomRace(newPlayerRace);
                
                DataManager.WriteNewRaceDescription(newPlayerRace.raceDescription, null, newPlayerRace.raceName);
            }
            
            Sys.WSMNL(">>>");
            Console.ReadLine();
            
            Sys.WSMNL("Race Creation Succesful!");
            Sys.WSMNL("Created Races: ");
            int counter = 1;
            foreach (var userMadeRace in userMadeRaces)
            {
                Sys.WSMNL($"{counter}. {userMadeRace.raceName}");
                Sys.WSMNL($"DESCRIPTION: ");
                Sys.WSMNL(userMadeRace.raceDescription);
                counter++;
            }

            GGlobals.activeCustomRaces = userMadeRaces;

        }
        return userMadeRaces;
    }

    public static List<RaceProficiency> CreateRaceProficiencies(bool useDefaults = false)
    {
        if (useDefaults)
        {
            Sys.WSMNL("...");
            Sys.WSMNL("Using default proficiencies...");
            // Sys.WSM(">>> ");
            // Console.ReadLine();
        }
        else
        {
            // create new classes
            Sys.WSMNL("Welcome to proficency creator!");

            int userProficiencyCountFinal = 0;
            bool proficienctCountInputValid = false;
            while (!proficienctCountInputValid)
            {
                Sys.WSMNL("Specify the number of proficencies to be included: ");
                Sys.WSM("> ");

                string userClassCount = Console.ReadLine();

                try
                {
                    userProficiencyCountFinal = int.Parse(userClassCount);

                    if (userProficiencyCountFinal == 0)
                    {
                        Error.WEMNL("MUST BE AT LEAST ONE PROFICIENCY");
                    }
                    else if (userProficiencyCountFinal > 10)
                    {
                        Error.WEMNL("TOO MANY PROFICIENCIES!");
                    }
                    else
                    {
                        proficienctCountInputValid = true;
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }

            for (int i = 0; i < userProficiencyCountFinal; i++)
            {
                Sys.WSM(">>> ");
                Console.ReadLine();

                Sys.WSMNL("Specify name of proficency: ");
                Sys.WSM("> ");
                string userRaceProficiencyName = Console.ReadLine().ToUpper();

                Sys.WSMNL("\n");

                Sys.WSMNL("Specify description of proficency: ");
                string userRaceProficiencyDescription = Console.ReadLine();

                //TODO: DO THIS LATER
                // Sys.WSMNL("Specify attribute modifers [0 to skip] ");
                //
                //
                // bool userRaceProficiencyAtrributeModifcationInputValid = false;
                // while (!userRaceProficiencyAtrributeModifcationInputValid)
                // {
                //     Sys.WSMNL("Create new proficenceis [Y/N]: ");
                //
                //     string userRaceProficencySelectionInput = Console.ReadLine();
                //
                //     switch (userRaceProficencySelectionInput)
                //     {
                //         case "Y":
                //         case "y":
                //             StoryManager.CreateRaceProficiencies();
                //             userRaceProficiencyAtrributeModifcationInputValid = true;
                //             break;
                //         case "N":
                //         case "n":
                //             StoryManager.CreateRaceProficiencies(true);
                //             userRaceProficiencyAtrributeModifcationInputValid = true;
                //             break;
                //         default:
                //             Error.WEMNL("NO VALID INPUT!");
                //             break;
                //     }
                // }

                RaceProficiency newRaceProficiency =
                    new RaceProficiency(userRaceProficiencyName, userRaceProficiencyDescription);
            }
        }

        return userMadeRaceProficiencies;
    }

    public static void CreateRaceDescription()
    {
    }

    public static void CreateClassDescription()
    {
    }

    public static void CreateAccoladeDescription()
    {
    }

    public void CreateScenes()
    {

        List<string> userDefPrompts = new List<string>();
        List<string> userDefConsequences = new List<string>();
        List<Scene> userDefScenes = new List<Scene>();

        CosmeticMenu.writeTitleCosmetics("SCENE CREATOR");
            
        Sys.WSMNL("Welcome to scene creator!");
        
        Sys.WSMNL("Guidlines for creating a new story:");
        Sys.WSMNL("1. 3 Scenes = short story");
        Sys.WSMNL("2. 4-5 scenes = medium story");
        Sys.WSMNL("3. 6 and over scenes = long story");
        
        Sys.WSMNL("--------------");
        Sys.WSMNL("Scenes are each composed of prompts:");
        Sys.WSMNL("1. 1 prompt = short scene (used for effect)");
        Sys.WSMNL("2. 2 prompts = medium scene (used for combat)");
        Sys.WSMNL("3. 3 prompts = long scene (used for plot development)");
        
        Sys.WSMNL("Associate scene with class: ");


        PlayerClass associatedClass = new PlayerClass();
        
        bool userClassSceneSelectionValid = false;

        while (!userClassSceneSelectionValid)
        {
            int counter = 0;
            foreach (var pl_class in GGlobals.activeCustomClasses)
            {
                Sys.WSMNL($"{counter}. {pl_class.className}");
                counter++;
            }
        
            Sys.WSMNL("Select class: ");
            Sys.WSM("> ");

            string userClassSceneSelection = Console.ReadLine();

            try
            {
                int classIndex = int.Parse(userClassSceneSelection);
                PlayerClass selectedClass = GGlobals.activeCustomClasses[classIndex];

                if (GGlobals.activeCustomClasses.IndexOf(selectedClass) == -1)
                {
                    Error.WEMNL("NO VALID CLASS!");
                }
                else
                {
                    associatedClass = selectedClass;
                    userClassSceneSelectionValid = true;
                }

            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
                throw;
            }
        }
        
        
        
        Sys.WSMNL("Enter name of scene: ");
        
        string sceneName = Console.ReadLine();

        int userSceneCountFinal = 0;
        bool sceneCountInputValid = false;
        while (!sceneCountInputValid)
        {
            Sys.WSMNL("Specify the number of scenes to be included: ");
            Sys.WSM("> ");

            string userSceneCount = Console.ReadLine();

            try
            {
                userSceneCountFinal = int.Parse(userSceneCount);

                if (userSceneCountFinal == 0)
                {
                    Error.WEMNL("MUST BE AT LEAST ONE Scene");
                }
                else
                {
                    sceneCountInputValid = true;
                }
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT");
            }
        }

        for (int i = 0; i < userSceneCountFinal; i++)
        {
            int userPromptCountFinal = 0;
            
            CosmeticMenu.writeTitleCosmetics($"Scene {i+1} Creation");

            bool promptCountInputValid = false;

            while (!promptCountInputValid)
            {
                Sys.WSMNL("Specify number of prompts for scene: ");

                string userPromptCount = Console.ReadLine();
                
                try
                {
                    userPromptCountFinal = int.Parse(userPromptCount);

                    if (userPromptCountFinal == 0)
                    {
                        Error.WEMNL("MUST BE AT LEAST ONE PROMPT");
                    }
                    else
                    {
                        promptCountInputValid = true;
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }

            for (int j = 0; j < userPromptCountFinal; j++)
            {
                Sys.WSMNL("Write Prompt");
                Sys.WSMNL("> ");
                
                string userDefPrompt = Console.ReadLine();

                bool userConfirmPromptInputValid = false;

                while (!userConfirmPromptInputValid)
                {
                    Sys.WSMNL("Confirm prompt [Y/N]?");

                    string userConfirmPromptInput = Console.ReadLine();
                    
                    switch (userConfirmPromptInput)
                    {
                        case "Y":
                        case"y":
                            Sys.WSMNL("prompt confirmed!");
                            userDefPrompts.Add(userConfirmPromptInput);
                            userConfirmPromptInputValid = true;
                            break;
                        case "N":
                        case "n":
                            Sys.WSMNL("prompt dumped! Reseting...");
                            j = j - 1;
                            userConfirmPromptInputValid = true;
                            break;
                        default:
                            Error.WEMNL("NO VALID INPUT!");
                            break;
                    }
                }
            }

            // for consequences
            for (int j = 0; j < userDefPrompts.Count(); j++)
            {
                Sys.WSMNL("Write Consequence");

                Sys.WSMNL("Prompt: ");
                Sys.WSMNL($@"\'{userDefPrompts[i]}'\");

                Sys.WSMNL("\n");
                Sys.WSMNL("> ");

                string userDefConsequence = Console.ReadLine();

                bool userConfirmConsqeuenceInputValid = false;

                while (!userConfirmConsqeuenceInputValid)
                {
                    Sys.WSMNL("Confirm prompt [Y/N]?");

                    string userConfirmConsequenceInput = Console.ReadLine();

                    switch (userConfirmConsequenceInput)
                    {
                        case "Y":
                        case "y":
                            Sys.WSMNL("consequence confirmed!");
                            userDefConsequences.Add(userConfirmConsequenceInput);
                            userConfirmConsqeuenceInputValid = true;
                            break;
                        case "N":
                        case "n":
                            Sys.WSMNL("consequence dumped! Reseting...");
                            j = j - 1;
                            userConfirmConsqeuenceInputValid = true;
                            break;
                        default:
                            Error.WEMNL("NO VALID INPUT!");
                            break;
                    }
                }
            }

            Scene newScene = new Scene(sceneName, i, associatedClass, userDefPrompts, userDefConsequences);
            
            DataManager.SaveCustomScene(newScene);
            
            userDefScenes.Add(newScene);
            
        }


        Sys.WSM(">>> ");
        Console.ReadLine();
    }

    public static void EditScenes()
    {
        
        bool userOverhaulScenesInputValid = false;

        if (GGlobals.defaultStoryExists && GGlobals.customStoryExists)
        {
            bool userOverhaulBothScenesInputValid = false;

            while (!userOverhaulScenesInputValid)
            {
                Sys.WSMNL("Scene data detected in both custom and story!");
                Sys.WSMNL("\n");

                Sys.WSMNL("Resolve conflict:");
                
                Sys.WSMNL("1. Purge both");
                Sys.WSMNL("2. Purge default scenes");
                Sys.WSMNL("3. Purge custom scenes");

                string userOverHaulBothScenesInput = Console.ReadLine();
                
                switch (userOverHaulBothScenesInput)
                {
                    case "1":
                        Sys.WSMNL("Conflict resolved: purging both");
                        break;
                    case "2":
                        Sys.WSMNL("Conflict resolved: purging default");
                        break;
                    case "3":
                        Sys.WSMNL("Conflict resolved: purging custom");
                        break;
                    default:
                        Error.WEMNL("NO VALID INPUT!");
                        break;
                }                
            }
        }
        else if (GGlobals.defaultStoryExists)
        { 
            Sys.WSMNL("Wiping default scene data...");   
        }
        else if (GGlobals.customStoryExists)
        {
            Sys.WSMNL("Wiping custom scene data");    
        }

        if (!GGlobals.newStory)
        {
            while (!userOverhaulScenesInputValid)
            {
                Sys.WSMNL("Overhaul existing scenes [Y/N]? ");
                string userOverhaulScenesInput = Console.ReadLine();


                switch (userOverhaulScenesInput)
                {
                    case "Y":
                    case "y":
                        Sys.WSMNL("Wiping scenes...");
                        break;
                    case "N":
                    case "n":
                        Sys.WSMNL("Existing scene files will not be wiped!");
                        break;
                    default:
                        Error.WEMNL("NO VALID INPUT");
                        break;
                }
            }
        }
    }

    public static void CREATESTORY(StoryManager SM, DataManager DM)
    {
        GGlobals.defaultClassesUsed = false;
        GGlobals.newStory = true;
        
        // scaffold
        SM.CreateRaces();
        SM.CreateClasses();
        SM.CreateAccolades();
        
        // story meat
        SM.CreateBeginning(DM);
        SM.CreateScenes();
        // create characters


        // story creation has succeeded!
        DataManager.EtchStoryValidation();
    }
}

public struct Scene
{
    public string sceneName;
    public int sceneNumber;
    public PlayerClass classAssociated;
    public List<string> prompts{ get; set; }
    public List<string> consequences { get; set; }

    public Scene(string _sceneName, int _sceneNumber, PlayerClass _classAssociated, List<string> _prompts, List<string> _consequences)
    {
        sceneName = _sceneName;
        sceneNumber = _sceneNumber;
        classAssociated = _classAssociated;
        prompts = _prompts;
        consequences = _consequences;
    }

    public Scene(string _sceneName, int _sceneNumber, PlayerClass _classAssociated)
    {
        sceneName = _sceneName;
        sceneNumber = _sceneNumber;
        classAssociated = _classAssociated;
        prompts = new List<string>();
        consequences = new List<string>();
    }
}