using System.Diagnostics;
using System.Net;

namespace BanishedMain;

public static class GameManager
{
    public static string readBeginning(PlayerClass pl_class)
    {
        string[] fileContent = new String[]{""};
        
        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
                fileContent = File.ReadAllLines(GDirectories.playerKnightStoryDataBeginningF);
                break;
            case PlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataBeginningF);
                break;
            case PlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataBeginningF);
                break;
        }
        
        //string[] fileContent = File.ReadAllLines(GDirectories.playerClassKnightDataF);

        // LINQ query
        var beginningContent = fileContent.SkipWhile(s => s != "BEGINNING").Skip(1).TakeWhile(s => s != "BEGINNINGEND");
 
        string beginningString = String.Join("\n", beginningContent.ToArray());

        return beginningString;
    }

    // TODO: FINISH THIS
    public static string readDescription(PlayerClass pl_class)
    {
        string[] fileContent = new string[] { "" };
        
        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
                fileContent = File.ReadAllLines(GDirectories.playerClassKnightDataF);
                break;
            case PlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerClassSorcererDataF);
                break;
            case PlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerClassWarlockDataF);
                break;
        }

        // LINQ query
        var descriptionContent = fileContent.SkipWhile(s => s != "DESCRIPTION").Skip(1).TakeWhile(s => s != "DESCRIPTIONEND");
        string descriptionString = String.Join("\n", descriptionContent.ToArray());

        return descriptionString;
    }

    public static string readPrompt(PlayerClass pl_class, string sceneNumber, string promptNumber) // TODO: FINISH, ADD CHECK FOR SCENE
    {
        string[] fileContent = new string[] { "" };

        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
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
            case PlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataSceneOneOptionsF);
                break;
            case PlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataSceneOneOptionsF);
                break;
        }
        
        var promptContent = fileContent.SkipWhile(s => s != $"PROMPT{promptNumber.ToUpper()}").Skip(1)
            .TakeWhile(s => s != $"PROMPT{promptNumber.ToUpper()}END");
        
        string promptString = string.Join("\n", promptContent.ToArray());

        return promptString;
    }

    public static string readOptions(PlayerClass pl_class, string sceneNumber, string promptNumber)
    {
        string[] fileContent = new string[] { "" };

        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
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
            case PlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataSceneOneOptionsF);
                break;
            case PlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataSceneOneOptionsF);
                break;
        }
        
        var optionsContent = fileContent.SkipWhile(s => s != $"PROMPT{promptNumber.ToUpper()}OPTIONS").Skip(1)
            .TakeWhile(s => s != $"PROMPT{promptNumber.ToUpper()}OPTIONSEND");
        
        string optionsString = string.Join("\n", optionsContent.ToArray());

        return optionsString;
    }

    public static string readConsequences(PlayerClass pl_class, string sceneNumber, string responseNumber)
    {
        
        string[] fileContent = new string[] { "" };

        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
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
            case PlayerClass.SORCERER:
                fileContent = File.ReadAllLines(GDirectories.playerSorcererStoryDataSceneOneConsequencesF);
                break;
            case PlayerClass.WARLOCK:
                fileContent = File.ReadAllLines(GDirectories.playerWarlockStoryDataSceneOneConsequencesF);
                break;
        }
        
        var consequencesContent = fileContent.SkipWhile(s => s != $"RESPONSE{responseNumber.ToUpper()}").Skip(1)
            .TakeWhile(s => s != $"RESPONSE{responseNumber.ToUpper()}END");
        
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

}