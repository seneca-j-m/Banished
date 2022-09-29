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

    public void CreateClasses(bool useDefaults = false)
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
                Sys.WSMNL("Specify name of class: ");
                Sys.WSM("> ");
                string userClassName = Console.ReadLine().ToUpper();
                        
                Sys.WSMNL("\n");
                bool defaultsUsed = false;
                bool userValidHealthInput = false;
                while (!userValidHealthInput)
                {
                    Sys.WSMNL("Specify health multiplyer [default 100]");
                    Sys.WSM("> ");
                    string userClassHealthMultiplyer = Console.ReadLine();

                    try
                    {
                        int userClassHealthMultiplyerFinal = int.Parse(userClassHealthMultiplyer);

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
                            defaultsUsed = true;
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



            }


        }
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
    { }
    
    public static void CreateClassDescription()
    { }
    
    public static void CreateAccoladeDescription()
    { }
    
    public static void CreateScene()
    { }
    
    public static void CreatePrompt()
    { }
    
    public static void PurgeAll()
    { }

    public static void OverhaulRace()
    { }
    
    public static void OverhaulClass()
    { }
    
    public static void OverhaulAccolade()
    { }
}