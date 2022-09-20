namespace BanishedMain;

public class Elf
{
    public const string name = "ELF";

    public const string description =
        @"
            
        ";

    public Dictionary<string, string> background;
    public Dictionary<string, string> traits;

    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics($"THE {name}");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
    
}

public class Human 
{
    
    public const string name = "HUMAN";

    public const string description =
        @"

        ";
    
    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics($"THE {name}");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
    
    // populate background and traits
}



public class Orc
{
    public const string name = "ORC";

    public const string description =
        @"

        ";

    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics($"THE {name}");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
    }
}


// player races