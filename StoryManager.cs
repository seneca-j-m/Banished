namespace BanishedMain;

public class StoryManager
{
    private List<PlayerRace> _playerRaces;
    private readonly List<PlayerClass> _playerClasses;
    private List<PlayerAccolade> _playerAccolades;


    public StoryManager()
    {
        // populate lists
        _playerRaces = Enum.GetValues(typeof(PlayerRace)).Cast<PlayerRace>().ToList();
        _playerClasses = Enum.GetValues(typeof(PlayerClass)).Cast<PlayerClass>().ToList();
        _playerAccolades = Enum.GetValues(typeof(PlayerAccolade)).Cast<PlayerAccolade>().ToList();
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
            foreach (var playerClass in _playerClasses)
            {
                Sys.WSMNL($"{counter}. {playerClass}");
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