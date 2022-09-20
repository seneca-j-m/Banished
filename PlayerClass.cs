namespace BanishedMain;

// public class PlayerClass
// {
//     
// }

public class Knight
{
    private const string name = "KNIGHT";
    private const string description = 
        @"
         
        ";
    
    public const int base_health = 100;
    public const int base_faith = 10;
    public const int base_agility = 40;
    public static void Info()
    {
        // print stuff
        CosmeticMenu.writeTitleCosmetics("THE KNIGHT");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"HEALTH: {base_health}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }

    public static void Beginning()
    {
        string beginning = GameManager.readBeginning(PlayerClass.KNIGHT);
        Debug.WDMNL(beginning);
        
        // wait for input
        Console.ReadLine();
    }

    public static void K_SCENE_ONE()
    {
        Debug.WDMNL("");
    }
}

public class Sorcerer
{
    private const string name = "SORCERER";
    private const string description = 
        @"
        
        ";
    
    public const int base_health = 80;
    public const int base_faith = 100;
    public const int base_agility = 20;
    
    public static void info()
    {
        // print stuff
        CosmeticMenu.writeTitleCosmetics("THE SORCERER");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"HEALTH: {base_health}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
    public static void Beginning()
    {
        string beginning = GameManager.readBeginning(PlayerClass.KNIGHT);
        Debug.WDMNL(beginning);
        
        // wait for input
        Console.ReadLine();
    }
}

public class Warlock
{
    public const string name = "WARLOCK";
    public const string description = 
        @"
        
        ";
    
    public const int base_health = 75;
    public const int base_faith = 80;
    public const int base_agility = 50;
    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics("THE WARLOCK");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"HEALTH: {base_health}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
    public static void Beginning()
    {
        string beginning = GameManager.readBeginning(PlayerClass.KNIGHT);
        Debug.WDMNL(beginning);

        // wait for input
        Console.ReadLine();
    }
}