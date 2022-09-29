namespace BanishedMain;

public static class Help
{ }

public static class ClassHelp
{
    public static void ClassOptions()
    {
        // print every item in the enum list
        var classOptions = Enum.GetValues(typeof(DefaultPlayerClass)).Cast<DefaultPlayerClass>();
        
        // remove 'empty'
        List<DefaultPlayerClass> classList = classOptions.ToList();
        int emptyIndex = classList.IndexOf(DefaultPlayerClass.EMPTY);
        classList.RemoveAt(emptyIndex);

        CosmeticMenu.writeTitleCosmetics("CLASS HELP:");
        Debug.WDMNL("CLASS OPTIONS: ");
        uint optionIncrament = 1;
        foreach (var classOption in classList)
        {
            Debug.WDMNL($"{optionIncrament} :'{classOption.ToString()}'");
            optionIncrament++;
        }

        bool quit = false;

        while (!quit)
        {
            Debug.WDMNL("[q to quit]");
            string userClassHelpSelection = Console.ReadLine();

            switch (userClassHelpSelection)
            {
                case "q": 
                case "Q":
                    quit = true;
                    break;
                case "1":
                case "KNIGHT":
                case "Knight":
                    KnightHelp();
                    break;
                case "2":
                case "SORCERER":
                case "Sorcerer":
                    SorcererHelp();
                    break;
                case "3":
                case "WARLOCK":
                case "Warlock":
                    WarlockHelp();
                    break;
            }
        }

    }

    private static void KnightHelp()
    {}
    private static void SorcererHelp()
    {}
    private static void WarlockHelp()
    {}
    
}

public static class RaceHelp // TODO: PROCESS DEFAULTS
{
    public static void RaceOptions()
    {
        // print every item in the enum list
        var raceOptions = Enum.GetValues(typeof(DefaultPlayerRace)).Cast<DefaultPlayerRace>();
        
        // remove 'empty'
        List<DefaultPlayerRace> raceList = raceOptions.ToList();
        int emptyIndex = raceList.IndexOf(DefaultPlayerRace.EMPTY);
        raceList.RemoveAt(emptyIndex);

        CosmeticMenu.writeTitleCosmetics("RACE HELP:");
        Debug.WDMNL("RACE OPTIONS: ");
        uint optionIncrament = 1;
        foreach (var raceOption in raceList)
        {
            Debug.WDMNL($"{optionIncrament} :'{raceOption.ToString()}'");
            optionIncrament++;
        }

        bool quit = false;

        while (!quit)
        {
            Debug.WDMNL("[q to quit]");
            string userRaceHelpSelection = Console.ReadLine();

            switch (userRaceHelpSelection)
            {
                case "q":
                case "Q":
                    quit = true;
                    break;
                case "1": // elf
                case "ELF":
                case "elf":
                    ElfHelp();
                    break;
                case "2": // human
                case "HUMAN":
                case "Human":
                    HumanHelp();
                    break;
                case "3": // orc
                case "ORC":
                case "orc":
                    OrcHelp();
                    break;
            }
        }
    }
    private static void ElfHelp()
    {}
    private static void HumanHelp()
    {}
    private static void OrcHelp()
    {}
}

public static class AccoladeHelp
{
    public static void AccoladeOptions()
    {
        // print every item in the enum list
        var accoladeOptions = Enum.GetValues(typeof(DefaultPlayerAccolade)).Cast<DefaultPlayerAccolade>();
        
        // remove 'empty'
        List<DefaultPlayerAccolade> accoladeList = accoladeOptions.ToList();
        int emptyIndex = accoladeList.IndexOf(DefaultPlayerAccolade.EMPTY);
        accoladeList.RemoveAt(emptyIndex);

        CosmeticMenu.writeTitleCosmetics("ACCOLADE HELP:");
        Debug.WDMNL("ACCOLADE OPTIONS: ");
        uint optionIncrament = 1;
        foreach (var accoladeOption in accoladeList)
        {
            Debug.WDMNL($"{optionIncrament} :'{accoladeOption.ToString()}'");
            optionIncrament++;
        }

        bool quit = false;

        while (!quit)
        {
            Debug.WDMNL("[q to quit]");
            string userRaceHelpSelection = Console.ReadLine();

            switch (userRaceHelpSelection)
            {
                case "q":
                case "Q":
                    quit = true;
                    break;
                case "1": // warrior
                case "WARRIOR":
                case "ELF":
                    WarriorHelp();
                    break;
                case "2": // scholar
                case "SCHOLAR":
                case "Scholar":
                    ScholarHelp();
                    break;
                case "3": // acolyte
                case "ACOLYTE":
                case "acolyte":
                    AcolyteHelp();
                    break;
            }
        }
    }

    private static void WarriorHelp()
    {}

    private static void ScholarHelp()
    {}
    
    private static void AcolyteHelp()
    {}
}