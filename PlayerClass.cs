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
        A lowly knight, one serving a kingdom now lost to the sands of time. 
        ";
    
    public const int base_health = 100;
    public const int base_faith = 10;
    public const int base_agility = 40;

    public Dictionary<string, string> background;
    public Dictionary<string, string> traits;

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
    public const string name = "SORCERER";
    public const string description = 
        @"
        
        ";
    
    public static int base_health = 80;
    public static void info()
    {
        // print stuff
        CosmeticMenu.writeTitleCosmetics("THE SORCERER");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"HEALTH: {base_health}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
}

public class Warlock
{
    public const string name = "WARLOCK";
    public const string description = 
        @"
        
        ";
    
    public static int base_health = 75;
    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics("THE WARLOCK");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"HEALTH: {base_health}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
}