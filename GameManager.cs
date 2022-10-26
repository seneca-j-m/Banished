using System.Diagnostics;
using System.Net;
using System.Linq;

namespace BanishedMain;

public static class GameManager
{
    public static string readBeginning(DefaultPlayerClass pl_class)
    {
        string[] fileContent = new String[]{""};
        
        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataBeginningF);
                break;
            case DefaultPlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataBeginningF);
                break;
            case DefaultPlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataBeginningF);
                break;
        }
        
        //string[] fileContent = File.ReadAllLines(GDirectories.DefaultPlayerClassKnightDataF);

        // LINQ query
        var beginningContent = fileContent.SkipWhile(s => s != "BEGINNING").Skip(1).TakeWhile(s => s != "BEGINNINGEND");
 
        string beginningString = String.Join("\n", beginningContent.ToArray());

        return beginningString;
    }

    // TODO: FINISH THIS
    public static string readDescription(DefaultPlayerClass pl_class)
    {
        string[] fileContent = new string[] { "" };
        
        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                fileContent = File.ReadAllLines(GDirectories.playerClassKnightDataF);
                break;
            case DefaultPlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerClassSorcererDataF);
                break;
            case DefaultPlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerClassWarlockDataF);
                break;
        }

        // LINQ query
        var descriptionContent = fileContent.SkipWhile(s => s != "DESCRIPTION").Skip(1).TakeWhile(s => s != "DESCRIPTIONEND");
        string descriptionString = String.Join("\n", descriptionContent.ToArray());

        return descriptionString;
    }

    public static string readPrompt(DefaultPlayerClass pl_class, string sceneNumber, string promptNumber) // TODO: FINISH, ADD CHECK FOR SCENE
    {
        string[] fileContent = new string[] { "" };

        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                if (string.Equals(sceneNumber, "ONE"))
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneOneF);
                }
                else if (string.Equals(sceneNumber, "TWO"))
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneTwoF);
                }
                else
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneThreeF);
                }
                break;
            case DefaultPlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataSceneOneOptionsF);
                break;
            case DefaultPlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataSceneOneOptionsF);
                break;
        }
        
        var promptContent = fileContent.SkipWhile(s => s != $"PROMPT{promptNumber.ToUpper()}").Skip(1)
            .TakeWhile(s => s != $"PROMPT{promptNumber.ToUpper()}END");
        
        string promptString = string.Join("\n", promptContent.ToArray());

        return promptString;
    }

    public static string readOptions(DefaultPlayerClass pl_class, string sceneNumber, string promptNumber)
    {
        string[] fileContent = new string[] { "" };

        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                if (string.Equals(sceneNumber, "ONE"))
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneOneOptionsF);
                }
                else if (string.Equals(sceneNumber, "TWO"))
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneTwoOptionsF);
                }
                else
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneThreeOptionsF);
                }
                break;
            case DefaultPlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataSceneOneOptionsF);
                break;
            case DefaultPlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataSceneOneOptionsF);
                break;
        }
        
        var optionsContent = fileContent.SkipWhile(s => s != $"PROMPT{promptNumber.ToUpper()}OPTIONS").Skip(1)
            .TakeWhile(s => s != $"PROMPT{promptNumber.ToUpper()}OPTIONSEND");
        
        string optionsString = string.Join("\n", optionsContent.ToArray());

        return optionsString;
    }

    public static string readConsequences(DefaultPlayerClass pl_class, string sceneNumber, string responseNumber)
    {
        
        string[] fileContent = new string[] { "" };

        switch (pl_class)
        {
            case DefaultPlayerClass.KNIGHT:
                if (string.Equals(sceneNumber, "ONE"))
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneOneConsequencesF);
                }
                else if (string.Equals(sceneNumber, "TWO"))
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneTwoConsequencesF);
                }
                else
                {
                    fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataSceneThreeConsequencesF);
                }
                break;
            case DefaultPlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataSceneOneConsequencesF);
                break;
            case DefaultPlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataSceneOneConsequencesF);
                break;
        }
        
        var consequencesContent = fileContent.SkipWhile(s => s != $"RESPONSE{responseNumber.ToUpper()}").Skip(1)
            .TakeWhile(s => s != $"RESPONSE{responseNumber.ToUpper()}END");
        
        string consequencesString = string.Join("\n", consequencesContent.ToArray());

        return consequencesString;
    }

    public static string ReadCustomBeginning(string className)
    {
        string[] fileContent = new String[]{""};


        string classBegPath = Path.Join(GDirectories.playerCustomClassPath, $"/{className}/Beginning/beginning.txt");
        fileContent = File.ReadAllLines(classBegPath);

        // LINQ query
        var beginningContent = fileContent.SkipWhile(s => s != "BEGINNING").Skip(1).TakeWhile(s => s != "BEGINNINGEND");
 
        string beginningString = String.Join("\n", beginningContent.ToArray());

        return beginningString;
    }

    public static string ReadCustomDescription(string className)
    {
        string[] fileContent = new String[]{""};


        string classBegPath = Path.Join(GDirectories.playerCustomClassPath, $"/{className}/Description/description.txt");
        fileContent = File.ReadAllLines(classBegPath);

        // LINQ query
        var descriptionContent = fileContent.SkipWhile(s => s != "DESCRIPTION").Skip(1).TakeWhile(s => s != "DESCRIPTIONEND");
 
        string descriptionString = String.Join("\n", descriptionContent.ToArray());

        return descriptionString;
    }

    public static string ReadCustomPrompt(string className, string sceneName, string sceneNumber, string promptNumber)
    {
        string[] fileContent = new string[] { "" };

        string scenePath = Path.Join(GDirectories.playerCustomStoryPath,
            $"/{className}/Scenes/{sceneName}/scene{sceneNumber}.txt");

        fileContent = File.ReadAllLines(scenePath);
        
        var promptContent = fileContent.SkipWhile(s => s != $"PROMPT{promptNumber.ToUpper()}").Skip(1)
            .TakeWhile(s => s != $"PROMPT{promptNumber.ToUpper()}END");
        
        string promptString = string.Join("\n", promptContent.ToArray());

        return promptString;
    }

    public static string ReadCustomOption(string className, string sceneName, string sceneNumber, string optionNumber)
    {
        string[] fileContent = new String[] { "" };

        string classOpPath = Path.Join(GDirectories.playerCustomStoryPath,
            $"/{className}/Scenes/{sceneName}/scene{sceneNumber}options.txt");

        fileContent = File.ReadAllLines(classOpPath);

        var optionsContent = fileContent.SkipWhile(s => s != $"OPTION{optionNumber}").Skip(1)
            .TakeWhile(s => s != $"OPTION{optionNumber}END");
        
        string optionsString = string.Join("\n", optionsContent.ToArray());

        return optionsString;
    }

    public static string ReadCustomConsequence(PlayerClass pl_class, Scene scene, string consequenceNumber)
    {
        string[] fileContent = new string[] { "" };

        string consequencePath = Path.Join(GDirectories.playerCustomStoryPath,
            $"/{pl_class.className}/Scenes/{scene.sceneName}/scene{scene.sceneNumber.ToString()}consequences.txt");

        File.ReadAllLines(consequencePath);

        var consequencesContent = fileContent.SkipWhile(s => s != $"CONSEQUENCE{consequenceNumber}").Skip(1)
            .TakeWhile(s => s != $"CONSEQUENCE{consequenceNumber}END");
        
        string consequencesString = string.Join("\n", consequencesContent.ToArray());

        return consequencesString;
    }
    

    public static string promptPlayer(string promptString, string optionString)
    {
        Debug.WDMNL(promptString);
        Debug.WDMNL("\n");

        Console.ReadLine(); // wait for input

        bool promptMenuExit = false;

        string userResponse = "";
        
        while (!promptMenuExit)
        {
            // print options
            Debug.WDMNL(optionString);
            Debug.WDMNL("\n");
            
            userResponse = Console.ReadLine().ToUpper(); // user input
            
            if (string.Equals(userResponse, "SAVE"))
            {
                Sys.WSMNL("SAVE INVOKED! SAVING...");
                promptMenuExit = true;
            }
            else if (!optionString.Contains(userResponse) || userResponse.Length > 1 || string.IsNullOrEmpty(userResponse))
            {
                Error.WEMNL("YOU STAY, CONFUSED. THAT IS NOT AN OPTION");
            }
            else
            {
                promptMenuExit = true;
            }
        }
        return userResponse;
    }

    public static string convertUserResponse(string userResponse)
    {
        string userResponseActual = "";
        
        switch (userResponse)
        {
            case "1":
                userResponseActual = "ONE";
                break;
            case "2":
                userResponseActual = "TWO";
                break;
            case "3":
                userResponseActual = "THREE";
                break;
            case "4":
                userResponseActual = "FOUR";
                break;
        }

        return userResponseActual;
    }

    public static void saveGame(Player player, string scene) //TODO: FINISH THIS 
    {
        Sys.WSMNL("S A V E ");
    }

    public static StoryType PreStoryValidate()
    {
        // get story title
        Debug.WDMNL("Initilise QUICKSTART...");
        Console.Clear();

        string defaultStoryTitle;
        string customStoryTitle;

        if (File.Exists(GDirectories.playerDataStoryNameFile) && File.Exists(GDirectories.playerCustomStoryTitleFile))
        {
            defaultStoryTitle = File.ReadAllLines(GDirectories.playerDataStoryNameFile).ToString();
            customStoryTitle = File.ReadAllLines(GDirectories.playerCustomStoryTitleFile).ToString();
            
            Debug.WDMNL("BOTH DEFAULT AND CUSTOM STORY EXISTS!");
            Debug.WDMNL("Choose which to run: ");

            bool userStorySelectionValid = false;

            while (!userStorySelectionValid)
            {
                Debug.WDMNL($"1. [DEFAULT] {defaultStoryTitle}");
                Debug.WDMNL($"2. [CUSTOM] {customStoryTitle}");
            
                Debug.WDM("> ");

                string userStorySelection = Console.ReadLine();

                switch (userStorySelection)
                {
                    case "1":
                        GGlobals.activeStoryTitle = defaultStoryTitle;
                        GGlobals.activeStoryType = StoryType.DEFAULT;
                        return GGlobals.activeStoryType;
                        break;
                    case "2":
                        GGlobals.activeStoryTitle = customStoryTitle;
                        GGlobals.activeStoryType = StoryType.CUSTOM;
                        return GGlobals.activeStoryType;
                        break;
                    default:
                        Error.WEMNL("NO VALID INPUT!");
                        break;
                }
            }
        }
        else if (File.Exists(GDirectories.playerDataStoryNameFile))
        {
            defaultStoryTitle = File.ReadAllLines(GDirectories.playerDataStoryNameFile).ToString();

            GGlobals.activeStoryTitle = defaultStoryTitle;
            GGlobals.activeStoryType = StoryType.DEFAULT;
            return GGlobals.activeStoryType;
        }
        else if (File.Exists(GDirectories.playerCustomStoryTitleFile))
        {
            customStoryTitle = File.ReadAllLines(GDirectories.playerCustomStoryTitleFile).ToString();
            
            GGlobals.activeStoryTitle = customStoryTitle;
            GGlobals.activeStoryType = StoryType.CUSTOM;
            return GGlobals.activeStoryType;
        }
        else
        {
            Error.WEMNL("NO STORY DETECTED! RETURN TO CREATE ONE!");
            return StoryType.EMPTY;
        }

        return StoryType.EMPTY;
    }

    public static void QUICKSTART()
    {
        List<DefaultPlayerClass> defaultPlayerClasses =
            Enum.GetValues(typeof(DefaultPlayerClass)).Cast<DefaultPlayerClass>().ToList();

        // print title
        Sys.WSMNL(GGlobals.activeStoryTitle);
            
        Sys.WSMNL(".............>");
        Sys.WSMNL("New Game Loading...");
        Console.ReadLine();
            
        Sys.WSMNL("Player: Select Class");

        // check which story is active
        if (GGlobals.activeStoryType == StoryType.DEFAULT)
        {
            int counter = 0;
            foreach (var className in defaultPlayerClasses)
            {
                Sys.WSMNL($"{counter}. {className}");
                counter++;
            }
        }
        else
        {
            // get class data
            string[] classNames = Directory.GetDirectories(GDirectories.playerCustomPersClassDataPath).Select(Path.GetFileName).ToArray();

            int counter = 0;
            foreach (var className in classNames)
            {
                Sys.WSMNL($"{counter}. {className}");
                counter++;
            }

            string selectedClassName = "";

            bool playerValidClassSelectionInputValid = false;

            while (!playerValidClassSelectionInputValid)
            {
                
                Sys.WSMNL("Select Class");
                Sys.WSM("> ");

                string userClassSelectionInput = Console.ReadLine(); 
                
                try
                {
                    int selectedClassInput = int.Parse(userClassSelectionInput);
                    selectedClassName = classNames[selectedClassInput];
                }
                catch (Exception e)
                {
                    Error.WEMNL("NO VALID INPUT!");
                    throw;
                }
            }
            
            // GAME START
            
            // get scene names
            string scenePath = Path.Join(GDirectories.playerCustomStoryPath, $"/{selectedClassName}/Scenes");
;           string[] scenes = Directory.GetDirectories(scenePath).Select(Path.GetFileName).ToArray();

            int promptCount = 1;
            int sceneCount = 0;
            int sceneNumber = 1;

            string prompt = "";
            string option = "";
            
            string beginning = ReadCustomBeginning(selectedClassName);
            string description = ReadCustomDescription(selectedClassName);
            
            Debug.WDMNL(description);
            Debug.WDMNL(beginning);

            
            for (int i = 0; i < scenes.Length; i++)
            {
                prompt = ReadCustomPrompt(selectedClassName, scenes[sceneCount], sceneNumber.ToString(), promptCount.ToString());
                option = ReadCustomOption(selectedClassName, scenes[sceneCount], sceneNumber.ToString(), promptCount.ToString());
                
                promptPlayer(prompt, option);
                promptCount++;
                sceneCount++;
                sceneNumber++;
            }
        }
    }
}