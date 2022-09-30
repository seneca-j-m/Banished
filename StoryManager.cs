using System.Reflection.Metadata;

namespace BanishedMain;

public class StoryManager
{
    private readonly List<DefaultPlayerRace> _defaultPlayerRaces;
    private readonly List<DefaultPlayerClass> _defaultPlayerClasses;
    private readonly List<DefaultPlayerAccolade> _defaultPlayerAccolades;


    public StoryManager()
    {
        // populate lists
        _defaultPlayerRaces = Enum.GetValues(typeof(DefaultPlayerRace)).Cast<DefaultPlayerRace>().ToList();
        _defaultPlayerClasses = Enum.GetValues(typeof(DefaultPlayerClass)).Cast<DefaultPlayerClass>().ToList();
        _defaultPlayerAccolades = Enum.GetValues(typeof(DefaultPlayerAccolade)).Cast<DefaultPlayerAccolade>().ToList();
    }

    public List<PlayerClass> CreateClasses(bool useDefaults = false)
    {
        List<PlayerClass> userMadeClasses = new List<PlayerClass>();
        
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

                Sys.WSMNL("\n");
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
                        else if (string.IsNullOrEmpty(userClassHealthMultiplyer))
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
                    Sys.WSMNL("Specify faith multiplyer [default 40] ");
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
                        else if (string.IsNullOrEmpty(userClassFaithMultiplyer))
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
                        else if (string.IsNullOrEmpty(userClassAgilityMultiplyer))
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