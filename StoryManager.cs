using System.Net;
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
                    Sys.WSMNL("Specify health multiplyer [default 100]");
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
                    Sys.WSMNL("Specify faith multiplyer [default 40]: ");
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
                    Sys.WSMNL("Specify agility multiplayer [default4 40]");
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
        if (DataManager.verifyCriticalFiles()[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool VerifyCustomStory()
    {
        if (DataManager.verifyCriticalFiles()[1])
        {
            return true;
        }
        else
        {
            return false;
        }
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

                DataManager.WriteNewAccoladeDescription(newPlayerAccolade.accoladeDescription, null,
                    newPlayerAccolade.accoladeName);
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
                    Sys.WSM("> ");

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
                    var userRaceProficencies = Enum.GetValues(typeof(DefaultRaceProficiency))
                        .Cast<DefaultRaceProficiency>().ToList();
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

    public void CreateScenes()
    {
        List<string> userDefPrompts = new List<string>();
        List<string> userDefConsequences = new List<string>();
        List<string> userDefOptions = new List<string>();
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

            CosmeticMenu.writeTitleCosmetics($"Scene {i + 1} Creation");

            bool promptCountInputValid = false;

            while (!promptCountInputValid)
            {
                Sys.WSMNL("Specify number of prompts for scene: ");
                Sys.WSM("> ");

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
                        case "y":
                            Sys.WSMNL("prompt confirmed!");
                            userDefPrompts.Add(userDefPrompt);
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
                Sys.WSMNL("Write Consequence For: ");

                Sys.WSMNL("Prompt: ");
                Sys.WSMNL($@"\'{userDefPrompts[j]}'\");

                Sys.WSMNL("\n");
                Sys.WSMNL("> ");

                string userDefConsequence = Console.ReadLine();

                bool userConfirmConsqeuenceInputValid = false;

                while (!userConfirmConsqeuenceInputValid)
                {
                    Sys.WSMNL("Confirm consequence [Y/N]?");

                    string userConfirmConsequenceInput = Console.ReadLine();

                    switch (userConfirmConsequenceInput)
                    {
                        case "Y":
                        case "y":
                            Sys.WSMNL("consequence confirmed!");
                            userDefConsequences.Add(userDefConsequence);
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

            for (int j = 0; j < userDefPrompts.Count(); j++)
            {
                Sys.WSMNL("Write Option For: ");

                Sys.WSMNL("Prompt: ");
                Sys.WSMNL($@"\'{userDefPrompts[j]}'\");

                Sys.WSMNL("\n");
                Sys.WSMNL("> ");

                string userDefOption = Console.ReadLine();

                bool userConfirmOptionInputValid = false;

                while (!userConfirmOptionInputValid)
                {
                    Sys.WSMNL("Confirm option [Y/N]?");

                    string userConfirmOptionInput = Console.ReadLine();

                    switch (userConfirmOptionInput)
                    {
                        case "Y":
                        case "y":
                            Sys.WSMNL("consequence confirmed!");
                            userDefOptions.Add(userDefOption);
                            userConfirmOptionInputValid = true;
                            break;
                        case "N":
                        case "n":
                            Sys.WSMNL("consequence dumped! Reseting...");
                            j = j - 1;
                            userConfirmOptionInputValid = true;
                            break;
                        default:
                            Error.WEMNL("NO VALID INPUT!");
                            break;
                    }
                }
            }

            Scene newScene = new Scene(sceneName, i, associatedClass, userDefPrompts, userDefOptions,
                userDefConsequences);

            DataManager.SaveCustomScene(newScene);

            userDefScenes.Add(newScene);
        }


        Sys.WSM(">>> ");
        Console.ReadLine();
    }

    public void CreatePrompt()
    {
        CosmeticMenu.writeTitleCosmetics("Prompt Creator");

        string prompt;
        string consequence;
        int promptNumber = 0;
        string selectedScene = "";
        string promptString = "";
        string consequenceString = "";

        bool userSelectClassForPromptInputValid = false;

        PlayerClass associatedClass = new PlayerClass();

        while (!userSelectClassForPromptInputValid)
        {
            int counter = 0;
            foreach (var pl_class in GGlobals.activeCustomClasses)
            {
                Sys.WSMNL($"{counter}. {pl_class.className}");
                counter++;
            }

            Sys.WSMNL("Select class: ");
            Sys.WSM("> ");

            string userClassSelection = Console.ReadLine();

            try
            {
                int classIndex = int.Parse(userClassSelection);
                PlayerClass selectedClass = GGlobals.activeCustomClasses[classIndex];

                if (GGlobals.activeCustomClasses.IndexOf(selectedClass) == -1)
                {
                    Error.WEMNL("NO VALID CLASS!");
                }
                else
                {
                    associatedClass = selectedClass;
                    userSelectClassForPromptInputValid = true;
                }
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
                throw;
            }
        }

        // get scene data
        string[] classScenes = DataManager.GetScenes(associatedClass.className);

        bool userSceneNumberInputValid = false;

        while (!userSceneNumberInputValid)
        {
            int counter = 0;
            foreach (var scene in classScenes)
            {
                Sys.WSMNL($"{counter}. {scene}");
                counter++;
            }

            Sys.WSMNL($"Specify Scene for {associatedClass.className}: ");
            Sys.WSM("> ");

            string userSceneNumberInput = Console.ReadLine();

            try
            {
                int sceneIndex = int.Parse(userSceneNumberInput);
                selectedScene = classScenes[sceneIndex];
                userSceneNumberInputValid = true;
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
                throw;
            }
        }

        bool userPromptNumberInputValid = false;

        while (!userPromptNumberInputValid)
        {
            Sys.WSMNL("Enter prompt number: ");
            Sys.WSM("> ");
            string userPromptNumber = Console.ReadLine();

            try
            {
                int classIndex = int.Parse(userPromptNumber);
                promptNumber = classIndex;

                userPromptNumberInputValid = true;
            }
            catch (Exception e)
            {
                Error.WEMNL("NO VALID INPUT!");
                throw;
            }
        }

        bool userConfirmPromptInputValid = false;

        while (!userConfirmPromptInputValid)
        {
            Sys.WSMNL("Write Prompt: ");
            Sys.WSMNL("> ");
            promptString = Console.ReadLine();

            Sys.WSMNL("Confirm prompt [Y/N]?");

            string userConfirmPromptInput = Console.ReadLine();

            switch (userConfirmPromptInput)
            {
                case "Y":
                case "y":
                    Sys.WSMNL("prompt confirmed!");
                    prompt = promptString;
                    userConfirmPromptInputValid = true;
                    break;
                case "N":
                case "n":
                    Sys.WSMNL("prompt dumped! Reseting...");
                    break;
                default:
                    Error.WEMNL("NO VALID INPUT!");
                    break;
            }
        }

        bool userConfirmConsqeuenceInputValid = false;

        while (!userConfirmConsqeuenceInputValid)
        {
            Sys.WSMNL("Write Consequence: ");
            Sys.WSMNL("> ");
            consequenceString = Console.ReadLine();


            Sys.WSMNL("Confirm Consequence [Y/N]?");

            string userConfirmConsequenceInput = Console.ReadLine();

            switch (userConfirmConsequenceInput)
            {
                case "Y":
                case "y":
                    Sys.WSMNL("consequence confirmed!");
                    consequence = consequenceString;
                    userConfirmConsqeuenceInputValid = true;
                    break;
                case "N":
                case "n":
                    Sys.WSMNL("consequence dumped! Reseting...");
                    break;
                default:
                    Error.WEMNL("NO VALID INPUT!");
                    break;
            }
        }

        DataManager.SavePrompt(associatedClass.className, selectedScene, promptNumber, promptString,
            consequenceString); ///////////////////////////////
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

    public void CreateStoryTitle()
    {
        Debug.WDMNL("STORY CREATOR LOADING...");

        Debug.WDMNL("SPECIFY NAME OF STORY:");
        Debug.WDM("> ");
        string storyName = Console.ReadLine();

        DataManager.SaveCustomStoryName(storyName.ToUpper());

        Debug.WDM("NAME ACCEPTED...");
        GGlobals.customStoryTitle = storyName;
    }

    public static void CREATESTORY(StoryManager SM, DataManager DM)
    {
        GGlobals.defaultClassesUsed = false;
        GGlobals.newStory = true;

        // initial
        SM.CreateStoryTitle();

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

    public void FILLSTORY()
    {
        CosmeticMenu.writeTitleCosmetics("FILL MENU");

        Sys.WSMNL("Welcome player!");
        Sys.WSMNL("This menu will guide you through filling the default story!");

        Sys.WSMNL("Prompts: 3");
        Sys.WSMNL("Scenes: 3 ");
        Sys.WSMNL("Classes: 3 ");

        bool knightSceneWritten = false;
        bool sorcererSceneWritten = false;
        bool warlockSceneWritten = false;

        DefaultPlayerClass pl_class = DefaultPlayerClass.EMPTY;
        List<DefaultPlayerClass> activeClasses = new List<DefaultPlayerClass>();
        activeClasses.Add(DefaultPlayerClass.KNIGHT);
        activeClasses.Add(DefaultPlayerClass.SORCERER);
        activeClasses.Add(DefaultPlayerClass.WARLOCK);
        
        // KNIGHT
        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write Prompt For Knight - scene {i}: ");
            WriteDefaultPrompt(DefaultPlayerClass.KNIGHT, i);
        }

        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write option for Knight - scene {i}");
            WriteDefaultOption(DefaultPlayerClass.KNIGHT, i);
        }

        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write consequence for Knight - scene {i}");
            WriteDefaultConsequence(DefaultPlayerClass.KNIGHT, i);
        }
        
        // SORCERER
        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write Prompt For Sorcerer - scene {i}: ");
            WriteDefaultPrompt(DefaultPlayerClass.SORCERER, i);
        }

        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write option for Sorcerer - scene {i}");
            WriteDefaultOption(DefaultPlayerClass.SORCERER, i);
        }

        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write consequence for Sorcerer - scene {i}");
            WriteDefaultConsequence(DefaultPlayerClass.SORCERER, i);
        }
        // WARLOCK
        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write Prompt For Warlock - scene {i}: ");
            WriteDefaultPrompt(DefaultPlayerClass.WARLOCK, i);
        }

        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write option for Warlock - scene {i}");
            WriteDefaultOption(DefaultPlayerClass.WARLOCK, i);
        }

        for (int i = 1; i < 4; i++)
        {
            Sys.WSMNL($"Write consequence for Warlock - scene {i}");
            WriteDefaultConsequence(DefaultPlayerClass.WARLOCK, i);
        }
    }

    public void FILLBEGINNING()
    {
        CosmeticMenu.writeTitleCosmetics("FILL MENU - BEGINNING");
        
        Sys.WSMNL("This menu will guide you through filling the default beginnings!");

        Sys.WSMNL("Prompts: 3");
        Sys.WSMNL("Scenes: 3 ");
        Sys.WSMNL("Classes: 3 ");

        bool knightBeginningWritten = false;
        bool sorcererBeginningWritten = false;
        bool warlockBeginningWritten = false;

        DefaultPlayerClass pl_class = DefaultPlayerClass.EMPTY;
        List<DefaultPlayerClass> activeClasses = new List<DefaultPlayerClass>();
        activeClasses.Add(DefaultPlayerClass.KNIGHT);
        activeClasses.Add(DefaultPlayerClass.SORCERER);
        activeClasses.Add(DefaultPlayerClass.WARLOCK);
        
        for (int i = 0; i < 3; i++)
        {
            // if (knightBeginningWritten)
            // {
            //     Sys.WSMNL("Knight Beginning: WRITTEN");
            //     try
            //     {
            //         activeClasses.Remove(DefaultPlayerClass.KNIGHT);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (sorcererBeginningWritten)
            // {
            //     Sys.WSMNL("Sorcerer Beginning: WRITTEN");
            //     try
            //     {
            //         activeClasses.Remove(DefaultPlayerClass.SORCERER);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (warlockBeginningWritten)
            // {
            //     Sys.WSMNL("Warlock Beginning: WRITTEN");
            //     try
            //     {
            //         activeClasses.Remove(DefaultPlayerClass.WARLOCK);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }

            bool userSelectClassForFillInputValid = false;
            
            while (!userSelectClassForFillInputValid)
            {
                Sys.WSMNL("Select Class: ");

                int counter = 0;
                foreach (var playerClass in activeClasses)
                {
                    Sys.WSMNL($"{counter}. {playerClass.ToString()}");
                    counter++;
                }
                Sys.WSM("> ");
                string userSelectedClassInput = Console.ReadLine();

                try
                {
                    Enum.TryParse(userSelectedClassInput, out pl_class);

                    WriteDefaultBeginning(pl_class);
                    
                    switch (pl_class)
                    {
                        case DefaultPlayerClass.KNIGHT:
                            knightBeginningWritten = true;
                            break;
                        case DefaultPlayerClass.SORCERER:
                            sorcererBeginningWritten = true;
                            break;
                        case DefaultPlayerClass.WARLOCK:
                            warlockBeginningWritten = true;
                            break;
                    }
                    
                    userSelectClassForFillInputValid = true;
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }
        }
    }
    
    public void FILLCLASSDESCRIPTION()
    { 
        CosmeticMenu.writeTitleCosmetics("FILL MENU - DESCRIPTION");
        
        Sys.WSMNL("This menu will guide you through filling the default descriptions!");

        Sys.WSMNL("Prompts: 3");
        Sys.WSMNL("Scenes: 3 ");
        Sys.WSMNL("Classes: 3 ");

        bool knightDescriptionWritten = false;
        bool sorcererDescriptionWritten = false;
        bool warlockDescriptionWritten = false;

        DefaultPlayerClass pl_class = DefaultPlayerClass.EMPTY;
        List<DefaultPlayerClass> activeClasses = new List<DefaultPlayerClass>();
        activeClasses.Add(DefaultPlayerClass.KNIGHT);
        activeClasses.Add(DefaultPlayerClass.SORCERER);
        activeClasses.Add(DefaultPlayerClass.WARLOCK);
        
        for (int i = 0; i < 3; i++)
        {
            // if (knightDescriptionWritten)
            // {
            //     Sys.WSMNL("Knight Description: WRITTEN");
            //     try
            //     {
            //         activeClasses.Remove(DefaultPlayerClass.KNIGHT);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (sorcererDescriptionWritten)
            // {
            //     Sys.WSMNL("Sorcerer Description: WRITTEN");
            //     try
            //     {
            //         activeClasses.Remove(DefaultPlayerClass.SORCERER);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (warlockDescriptionWritten)
            // {
            //     Sys.WSMNL("Warlock Description: WRITTEN");
            //     try
            //     {
            //         activeClasses.Remove(DefaultPlayerClass.WARLOCK);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }

            bool userSelectClassForFillInputValid = false;
            
            while (!userSelectClassForFillInputValid)
            {
                Sys.WSMNL("Select Class: ");

                int counter = 0;
                foreach (var playerClass in activeClasses)
                {
                    Sys.WSMNL($"{counter}. {playerClass.ToString()}");
                    counter++;
                }
                Sys.WSM("> ");
                string userSelectedClassInput = Console.ReadLine();

                try
                {
                    Enum.TryParse(userSelectedClassInput, out pl_class);

                    WriteClassDescription(pl_class);
                    
                    switch (pl_class)
                    {
                        case DefaultPlayerClass.KNIGHT:
                            knightDescriptionWritten = true;
                            break;
                        case DefaultPlayerClass.SORCERER:
                            sorcererDescriptionWritten = true;
                            break;
                        case DefaultPlayerClass.WARLOCK:
                            warlockDescriptionWritten = true;
                            break;
                    }
                    
                    userSelectClassForFillInputValid = true;
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }
        }
    }

    public void FILLRACEDESCRIPTION()
    { 
        CosmeticMenu.writeTitleCosmetics("FILL MENU - DESCRIPTION (RACE)");
        
        Sys.WSMNL("This menu will guide you through filling the default race descriptions!");

        Sys.WSMNL("Prompts: 3");
        Sys.WSMNL("Scenes: 3 ");
        Sys.WSMNL("Classes: 3 ");

        bool elfDescriptionWritten = false;
        bool humanDescriptionWritten = false;
        bool orcDescriptionWritten = false;

        DefaultPlayerRace pl_race = DefaultPlayerRace.EMPTY;
        List<DefaultPlayerRace> activeRaces = new List<DefaultPlayerRace>();
        activeRaces.Add(DefaultPlayerRace.ELF);
        activeRaces.Add(DefaultPlayerRace.HUMAN);
        activeRaces.Add(DefaultPlayerRace.ORC);

        for (int i = 0; i < 3; i++)
        {
            // if (elfDescriptionWritten)
            // {
            //     Sys.WSMNL("Elf Description: WRITTEN");
            //     try
            //     {
            //         activeRaces.Remove(DefaultPlayerRace.ELF);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (humanDescriptionWritten)
            // {
            //     Sys.WSMNL("Human Description: WRITTEN");
            //     try
            //     {
            //         activeRaces.Remove(DefaultPlayerRace.HUMAN);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (orcDescriptionWritten)
            // {
            //     Sys.WSMNL("Orc Description: WRITTEN");
            //     try
            //     {
            //         activeRaces.Remove(DefaultPlayerRace.ORC);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }

            bool userSelectRaceForFillInputValid = false;
            
            while (!userSelectRaceForFillInputValid)
            {
                Sys.WSMNL("Select Race: ");

                int counter = 0;
                foreach (var playerRace in activeRaces)
                {
                    Sys.WSMNL($"{counter}. {playerRace.ToString()}");
                    counter++;
                }
                Sys.WSM("> ");
                string userSelectedClassInput = Console.ReadLine();

                try
                {
                    Enum.TryParse(userSelectedClassInput, out pl_race);

                    WriteRaceDescription(pl_race);
                    
                    switch (pl_race)
                    {
                        case DefaultPlayerRace.ELF:
                            elfDescriptionWritten = true;
                            break;
                        case DefaultPlayerRace.HUMAN:
                            humanDescriptionWritten = true;
                            break;
                        case DefaultPlayerRace.ORC:
                            orcDescriptionWritten = true;
                            break;
                    }
                    
                    userSelectRaceForFillInputValid = true;
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }
        }
    }

    public void FILLACCOLADEDESCRIPTION()
    { 
        CosmeticMenu.writeTitleCosmetics("FILL MENU - DESCRIPTION (ACCOLADE)");
        
        Sys.WSMNL("This menu will guide you through filling the default accolade descriptions!");

        Sys.WSMNL("Prompts: 3");
        Sys.WSMNL("Scenes: 3 ");
        Sys.WSMNL("Classes: 3 ");

        bool warriorDescriptionWritten = false;
        bool scholarDescriptionWritten = false;
        bool acolyteDescriptionWritten = false;

        DefaultPlayerAccolade pl_accolade = DefaultPlayerAccolade.EMPTY;
        List<DefaultPlayerAccolade> activeAccolades = new List<DefaultPlayerAccolade>();
        activeAccolades.Add(DefaultPlayerAccolade.WARRIOR);
        activeAccolades.Add(DefaultPlayerAccolade.SCHOLAR);
        activeAccolades.Add(DefaultPlayerAccolade.ACOLYTE);


        for (int i = 0; i < 3; i++)
        {
            // if (warriorDescriptionWritten)
            // {
            //     Sys.WSMNL("Warrior Description: WRITTEN");
            //     try
            //     {
            //         activeAccolades.Remove(DefaultPlayerAccolade.WARRIOR);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (scholarDescriptionWritten)
            // {
            //     Sys.WSMNL("Scholar Description: WRITTEN");
            //     try
            //     {
            //         activeAccolades.Remove(DefaultPlayerAccolade.SCHOLAR);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }
            //
            // if (acolyteDescriptionWritten)
            // {
            //     Sys.WSMNL("Acolyte Description: WRITTEN");
            //     try
            //     {
            //         activeAccolades.Remove(DefaultPlayerAccolade.ACOLYTE);
            //     }
            //     catch (Exception e)
            //     {
            //     }
            // }

            bool userSelectRaceForFillInputValid = false;
            
            while (!userSelectRaceForFillInputValid)
            {
                Sys.WSMNL("Select Race: ");

                int counter = 0;
                foreach (var playerRace in activeAccolades)
                {
                    Sys.WSMNL($"{counter}. {playerRace.ToString()}");
                    counter++;
                }
                Sys.WSM("> ");
                string userSelectedClassInput = Console.ReadLine();

                try
                {
                    Enum.TryParse(userSelectedClassInput, out pl_accolade);

                    WriteAccoladeDescription(pl_accolade);
                    
                    switch (pl_accolade)
                    {
                        case DefaultPlayerAccolade.WARRIOR:
                            warriorDescriptionWritten = true;
                            break;
                        case DefaultPlayerAccolade.SCHOLAR:
                            scholarDescriptionWritten = true;
                            break;
                        case DefaultPlayerAccolade.ACOLYTE:
                            acolyteDescriptionWritten = true;
                            break;
                    }
                    
                    userSelectRaceForFillInputValid = true;
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT");
                }
            }
        }
    }

    public void WriteDefaultPrompt(DefaultPlayerClass pl_class, int sceneNumber)
    {
        List<string> prompts = new List<string>();
        for (int i = 0; i < GGlobals.defaultPromptLimit; i++)
        {
            Sys.WSMNL($"Enter prompt {i}:");
            Sys.WSM("> ");

            string prompt = Console.ReadLine();
            prompts.Add(prompt);
        }
        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneOneF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneOneF))
                        {
                            sw.WriteLine("SCENEONE");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENEONEEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneTwoF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneTwoF))
                        {
                            sw.WriteLine("SCENETWO");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENETWOEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneThreeF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneThreeF))
                        {
                            sw.WriteLine("SCENETHREE");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENETHREEEND");
                        }

                        break;
                }

                break;
            case DefaultPlayerClass.SORCERER:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneOneF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneOneF))
                        {
                            sw.WriteLine("SCENEONE");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENEONEEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneTwoF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneTwoF))
                        {
                            sw.WriteLine("SCENETWO");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENETWOEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneThreeF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneThreeF))
                        {
                            sw.WriteLine("SCENETHREE");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENETHREEEND");
                        }

                        break;
                }

                break;
            case DefaultPlayerClass.WARLOCK:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneOneF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneOneF))
                        {
                            sw.WriteLine("SCENEONE");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENEONEEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneTwoF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneTwoF))
                        {
                            sw.WriteLine("SCENETWO");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENETWOEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneThreeF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneThreeF))
                        {
                            sw.WriteLine("SCENETHREE");

                            sw.WriteLine("PROMPTONE");
                            sw.WriteLine(prompts[0]);
                            sw.WriteLine("PROMPTONEEND");

                            sw.WriteLine("PROMPTTWO");
                            sw.WriteLine(prompts[1]);
                            sw.WriteLine("PROMPTTWOEND");

                            sw.WriteLine("PROMPTTHREE");
                            sw.WriteLine(prompts[2]);
                            sw.WriteLine("PROMPTTHREEEND");

                            sw.WriteLine("SCENETHREEND");
                        }

                        break;
                }

                break;
        }
    }

    public void WriteDefaultOption(DefaultPlayerClass pl_class, int sceneNumber)
    {
        List<string> options = new List<string>();
        for (int i = 0; i < GGlobals.defaultPromptLimit; i++)
        {
            Sys.WSMNL($"Enter option {i}:");
            Sys.WSM("> ");

            string option = Console.ReadLine();
            options.Add(option);
        }
        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneOneOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneOneOptionsF))
                        {
                            sw.WriteLine("SCENEONEOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENEONEOPTIONSEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneTwoOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneTwoOptionsF))
                        {
                            sw.WriteLine("SCENETWO");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENETWOOPTIONSEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneThreeOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneThreeOptionsF))
                        {
                            sw.WriteLine("SCENETHREEOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENETHREEOPTIONSEND");
                        }

                        break;
                }

                break;
            case DefaultPlayerClass.SORCERER:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneOneOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneOneOptionsF))
                        {
                            sw.WriteLine("SCENEONEOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENEONEOPTIONSEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneTwoOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneTwoOptionsF))
                        {
                            sw.WriteLine("SCENETWOOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENETWOOPTIONSEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneThreeOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneThreeOptionsF))
                        {
                            sw.WriteLine("SCENETHREEOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENETHREEOPTIONSEND");
                        }

                        break;
                }

                break;
            case DefaultPlayerClass.WARLOCK:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneOneOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneOneOptionsF))
                        {
                            sw.WriteLine("SCENEONEOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENEONEOPTIONSEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneTwoOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneTwoOptionsF))
                        {
                            sw.WriteLine("SCENETWOOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENETWOOPTIONSEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneThreeOptionsF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneThreeOptionsF))
                        {
                            sw.WriteLine("SCENETHREEOPTIONS");

                            sw.WriteLine("PROMPTONEOPTIONS");
                            sw.WriteLine(options[0]);
                            sw.WriteLine("PROMPTONEOPTIONSEND");

                            sw.WriteLine("PROMPTTWOOPTIONS");
                            sw.WriteLine(options[1]);
                            sw.WriteLine("PROMPTTWOOPTIONSEND");

                            sw.WriteLine("PROMPTTHREEOPTIONS");
                            sw.WriteLine(options[2]);
                            sw.WriteLine("PROMPTTHREEOPTIONSEND");

                            sw.WriteLine("SCENETHREEOPTIONSND");
                        }

                        break;
                }

                break;
        }
    }

    public void WriteDefaultConsequence(DefaultPlayerClass pl_class, int sceneNumber)
    {
        List<string> consequences = new List<string>();
        for (int i = 0; i < GGlobals.defaultPromptLimit; i++)
        {
            Sys.WSMNL($"Enter consequence {i}:");
            Sys.WSM("> ");

            string consequence = Console.ReadLine();
            consequences.Add(consequence);
        }
        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneOneConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneOneConsequencesF))
                        {
                            sw.WriteLine("SCENEONECONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENEONECONSEQUENCESEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneTwoConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneTwoConsequencesF))
                        {
                            sw.WriteLine("SCENETWOCONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENETWOCONSEQUENCESEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerKnightStoryDataSceneThreeConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataSceneThreeConsequencesF))
                        {
                            sw.WriteLine("SCENETHREECONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENETHREECONSQUENCESEND");
                        }

                        break;
                }

                break;
            case DefaultPlayerClass.SORCERER:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneOneConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneOneConsequencesF))
                        {
                            sw.WriteLine("SCENEONECONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENEONECONSEQUENCESEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneTwoConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneTwoConsequencesF))
                        {
                            sw.WriteLine("SCENETWOCONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCES");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENETWOCONSEQUENCESEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerSorcererStoryDataSceneThreeConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataSceneThreeConsequencesF))
                        {
                            sw.WriteLine("SCENETHREECONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENETHREECONSEQUENCESEND");
                        }

                        break;
                }

                break;
            case DefaultPlayerClass.WARLOCK:
                switch (sceneNumber)
                {
                    case 1:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneOneConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneOneConsequencesF))
                        {
                            sw.WriteLine("SCENEONECONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENEONECONSEQUENCESEND");
                        }

                        break;
                    case 2:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneTwoConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneTwoConsequencesF))
                        {
                            sw.WriteLine("SCENETWOCONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENETWOCONSEQUENCESEND");
                        }

                        break;
                    case 3:
                        File.WriteAllText(GDirectories.playerWarlockStoryDataSceneThreeConsequencesF, string.Empty);
                        using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataSceneThreeConsequencesF))
                        {
                            sw.WriteLine("SCENETHREECONSEQUENCES");

                            sw.WriteLine("PROMPTONECONSEQUENCES");
                            sw.WriteLine(consequences[0]);
                            sw.WriteLine("PROMPTONECONSEQUENCESEND");

                            sw.WriteLine("PROMPTTWOCONSEQUENCES");
                            sw.WriteLine(consequences[1]);
                            sw.WriteLine("PROMPTTWOCONSEQUENCESEND");

                            sw.WriteLine("PROMPTTHREECONSEQUENCES");
                            sw.WriteLine(consequences[2]);
                            sw.WriteLine("PROMPTTHREECONSEQUENCESEND");

                            sw.WriteLine("SCENETHREECONSEQUENCESEND");
                        }

                        break;
                }

                break;
        }
    }

    public void WriteDefaultBeginning(DefaultPlayerClass pl_class)
    {
        Sys.WSMNL("Enter Beginning: ");
        Sys.WSM("> ");

        string beginning = Console.ReadLine();


        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                File.WriteAllText(GDirectories.playerKnightStoryDataBeginningF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerKnightStoryDataBeginningF))
                {
                    sw.WriteLine("BEGINNING");
                    sw.WriteLine(beginning);
                    sw.WriteLine("BEGINNINGEND");
                }
                break;
            case DefaultPlayerClass.SORCERER:
                File.WriteAllText(GDirectories.playerSorcererStoryDataBeginningF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerSorcererStoryDataBeginningF))
                {
                    sw.WriteLine("BEGINNING");
                    sw.WriteLine(beginning);
                    sw.WriteLine("BEGINNINGEND");
                }
                break;
            case DefaultPlayerClass.WARLOCK:
                File.WriteAllText(GDirectories.playerWarlockStoryDataBeginningF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerWarlockStoryDataBeginningF))
                {
                    sw.WriteLine("BEGINNING");
                    sw.WriteLine(beginning);
                    sw.WriteLine("BEGINNINGEND");
                }
                break;
        }
    }

    public void WriteClassDescription(DefaultPlayerClass pl_class)
    {
        Sys.WSMNL("Enter Description: ");
        Sys.WSM("> ");

        string description = Console.ReadLine();


        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                File.WriteAllText(GDirectories.playerClassKnightDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerClassKnightDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
            case DefaultPlayerClass.SORCERER:
                File.WriteAllText(GDirectories.playerClassSorcererDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerClassSorcererDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
            case DefaultPlayerClass.WARLOCK:
                File.WriteAllText(GDirectories.playerClassWarlockDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerClassWarlockData))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
        }
    }

    public void WriteRaceDescription(DefaultPlayerRace pl_race)
    {
        Sys.WSMNL("Enter Description: ");
        Sys.WSM("> ");

        string description = Console.ReadLine();
        
        switch (pl_race)
        {
            case DefaultPlayerRace.ELF:
                File.WriteAllText(GDirectories.playerRaceElfDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerRaceElfDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
            case DefaultPlayerRace.HUMAN:
                File.WriteAllText(GDirectories.playerRaceHumanDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerRaceHumanDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
            case DefaultPlayerRace.ORC:
                File.WriteAllText(GDirectories.playerRaceOrcDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerRaceOrcDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
        }
    }

    public void WriteAccoladeDescription(DefaultPlayerAccolade pl_accolade)
    {
                
        Sys.WSMNL("Enter Description: ");
        Sys.WSM("> ");

        string description = Console.ReadLine();
        
        switch (pl_accolade)
        {
            case DefaultPlayerAccolade.ACOLYTE:
                File.WriteAllText(GDirectories.playerAccoladeAcolyteDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerAccoladeAcolyteDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
            case DefaultPlayerAccolade.SCHOLAR:
                File.WriteAllText(GDirectories.playerAccoladeScholarDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerAccoladeScholarDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
            case DefaultPlayerAccolade.WARRIOR:
                File.WriteAllText(GDirectories.playerAccoladeWarriorDataF, string.Empty);
                using (StreamWriter sw = new StreamWriter(GDirectories.playerAccoladeWarriorDataF))
                {
                    sw.WriteLine("DESCRIPTION");
                    sw.WriteLine(description);
                    sw.WriteLine("DESCRIPTIONEND");
                }
                break;
        }
    }
    
}

public struct Scene
{
    public string sceneName;
    public int sceneNumber;
    public PlayerClass classAssociated;
    public List<string> prompts { get; set; }
    public List<string> options { get; set; }
    public List<string> consequences { get; set; }

    public Scene(string _sceneName, int _sceneNumber, PlayerClass _classAssociated, List<string> _prompts,
        List<string> _options, List<string> _consequences)
    {
        sceneName = _sceneName;
        sceneNumber = _sceneNumber;
        classAssociated = _classAssociated;
        prompts = _prompts;
        options = _options;
        consequences = _consequences;
    }

    public Scene(string _sceneName, int _sceneNumber, PlayerClass _classAssociated)
    {
        sceneName = _sceneName;
        sceneNumber = _sceneNumber;
        classAssociated = _classAssociated;
        prompts = new List<string>();
        options = new List<string>();
        consequences = new List<string>();
    }

    public Scene()
    {
        sceneName = "";
        sceneNumber = -1;
        classAssociated = new PlayerClass();
        prompts = new List<string>();
        options = new List<string>();
        consequences = new List<string>();
    }
}

public enum StoryType
{
    EMPTY,
    DEFAULT,
    CUSTOM
}