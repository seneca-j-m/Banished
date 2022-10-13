using System.Reflection.Metadata;

namespace BanishedMain;

public class StoryManager
{
    private readonly List<DefaultPlayerRace> _defaultPlayerRaces;
    private readonly List<DefaultPlayerClass> _defaultPlayerClasses;
    private readonly List<DefaultPlayerAccolade> _defaultPlayerAccolades;

    private List<PlayerClass> userMadeClasses = new List<PlayerClass>();


    public StoryManager()
    {
        // populate lists
        _defaultPlayerRaces = Enum.GetValues(typeof(DefaultPlayerRace)).Cast<DefaultPlayerRace>().ToList();
        _defaultPlayerClasses = Enum.GetValues(typeof(DefaultPlayerClass)).Cast<DefaultPlayerClass>().ToList();
        _defaultPlayerAccolades = Enum.GetValues(typeof(DefaultPlayerAccolade)).Cast<DefaultPlayerAccolade>().ToList();
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
        }
        
        return userMadeClasses;
    }

    public void CreateBeginning()
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

                string userClassSelection = Console.ReadLine();

                try
                {
                    Enum.TryParse(userClassSelection, out DefaultPlayerClass pl_class);
                    if (_defaultPlayerClasses.IndexOf(pl_class) == -1)
                    {
                        Error.WEMNL("NO VALID INPUT");
                    }
                    else
                    {
                    }
                }
                catch (Exception e)
                {
                    Error.WEMNL("BAD INPUT!");
                }
            }
            else
            {
                foreach (var playerClass in userMadeClasses)
                {
                    Sys.WSMNL($"{counter}. {playerClass}");
                }

                string userClassSelection = Console.ReadLine();

                try
                {
                    Enum.TryParse(userClassSelection, out PlayerClass pl_class);
                    if (userMadeClasses.IndexOf(pl_class) == -1)
                    {
                        Error.WEMNL("NO VALID INPUT");
                    }
                    else
                    {
                    }
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

    public static List<PlayerRace> CreateRaces(bool useDefaults = false)
    {
        List<PlayerRace> userMadeRaces = new List<PlayerRace>();

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

                bool userRaceReputationInputValid = false;
                bool defaultRaceReputationUsed = false;
                while (!userRaceReputationInputValid)
                {
                    Sys.WSMNL("Create new reputation [Y/N]: ");
                    
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
            }
        }
        return userMadeRaces;
    }

    public static List<RaceProficiency> CreateRaceProficiencies(bool useDefaults = false)
    {
        List<RaceProficiency> userRaceProficiencies = new List<RaceProficiency>();
        
        if (useDefaults)
        {
            Sys.WSMNL("...");
            Sys.WSMNL("Using default proficiencies...");
            Sys.WSM("> ");
            Console.ReadLine();
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

        return userRaceProficiencies;
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

    public static void CreateScene()
    {
    }

    public static void CreatePrompt()
    {
    }

    public static void PurgeAll()
    {
    }

    public static void OverhaulRace()
    {
    }

    public static void OverhaulClass()
    {
    }

    public static void OverhaulAccolade()
    {
    }
}