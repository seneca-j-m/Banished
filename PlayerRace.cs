namespace BanishedMain;

public static class Elf
{
    public const string name = "ELF";

    public const string description =
        @"
            
        ";
    
    public const DefaultRaceProminence raceProminence = DefaultRaceProminence.BOURGEOISIE;
    public const DefaultRaceReputation raceReputation = DefaultRaceReputation.FAMOUS;
    public const DefaultRaceProficiency raceProficiency = DefaultRaceProficiency.MAGIC;
    
    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics($"THE {name}");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
        Debug.WDMNL($"PROMINENCE: {raceProminence.ToString()}");
        Debug.WDMNL($"REPUTATION: {raceReputation.ToString()}");
        Debug.WDMNL($"PROFICIENCY: {raceProficiency.ToString()}");
    }
    
}

public static class Human 
{
    
    public const string name = "HUMAN";

    public const string description =
        @"

        ";
    
    public const DefaultRaceProminence raceProminence = DefaultRaceProminence.PETITE_BOURGEOISIE;
    public const DefaultRaceReputation raceReputation = DefaultRaceReputation.ORDINARY;
    public const DefaultRaceProficiency raceProficiency = DefaultRaceProficiency.COMBAT;
    
    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics($"THE {name}");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
        Debug.WDMNL($"PROMINENCE: {raceProminence.ToString()}");
        Debug.WDMNL($"REPUTATION: {raceReputation.ToString()}");
        Debug.WDMNL($"PROFICIENCY: {raceProficiency.ToString()}");
    }
    
}



public static class Orc
{
    public const string name = "ORC";

    public const string description =
        @"

        ";

    public const DefaultRaceProminence raceProminence = DefaultRaceProminence.PROLETARIAT;
    public const DefaultRaceReputation raceReputation = DefaultRaceReputation.INFAMOUS;
    public const DefaultRaceProficiency raceProficiency = DefaultRaceProficiency.BATTLING;

    public static void info()
    {
        CosmeticMenu.writeTitleCosmetics($"THE {name}");
        Debug.WDMNL($"NAME: {name}");
        Debug.WDMNL($"DESCRIPTION:");
        Debug.WDMNL(description);
        Debug.WDMNL($"PROMINENCE: {raceProminence.ToString()}");
        Debug.WDMNL($"REPUTATION: {raceReputation.ToString()}");
        Debug.WDMNL($"PROFICIENCY: {raceProficiency.ToString()}");
    }
}

public enum DefaultRaceProminence
{
    NONE,
    BOURGEOISIE,
    PETITE_BOURGEOISIE,
    PROLETARIAT,
}

public enum DefaultRaceReputation
{
    NONE,
    FAMOUS,
    ORDINARY,
    INFAMOUS
}

public enum DefaultRaceProficiency
{
    NONE,
    MAGIC,
    COMBAT,
    BATTLING
}

public class Bourgeoisie
{}

public class Petite_Bourgeoisie
{}


public struct RaceProminence
{
    public string raceProminenceName { get; }
    public string raceProminenceDescription { get; }

    public RaceProminence(string _raceProminenceName, string _raceProminenceDescription)
    {
        raceProminenceName = _raceProminenceName;
        raceProminenceDescription = _raceProminenceDescription;
    }
}

public struct RaceReputation
{}

public struct RaceProficiency
{
    public string raceProficiencyName { get; }
    public string raceProficiencyDescription { get; }

    public RaceProficiency(string _raceProficiencyName, string _raceProficiencyDescription)
    {
        raceProficiencyName = _raceProficiencyName;
        raceProficiencyDescription = _raceProficiencyDescription;
    }
}