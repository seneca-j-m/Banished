namespace BanishedMain;

// public class PlayerClass
// {
//     
// }

public class Knight
{
    public const string name = "KNIGHT";
    public const string description = 
        @"
        
        ";
    
    public static int base_health = 0;

    public Dictionary<string, string> background;
    public Dictionary<string, string> traits;
    
    // populate data
    public void populuate()
    {
    }


    public static void info()
    {
        // print stuff
        CosmeticMenu.writeTitleCosmetics("THE KNIGHT");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"HEALTH: {base_health}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
}

public class Sorcerer
{
    
}

public class Warlock
{
    
}