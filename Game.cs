using System.Net.Security;

namespace BanishedMain;

public class Game
{
    private Player player { get; set; }
    
    // divison into seperate variables for readability
    private PlayerClass pl_class;
    private PlayerRace pl_Race;
    private PlayerAccolade pl_accolade;

    private string playerName;
    private int playerHealth;
    private int playerFaith;
    private int playerAgility;

    public Game(Player _player)
    {
        player = _player;
        pl_class = player.playerclass;
        pl_Race = player.playerrace;
        pl_accolade = player.playeraccolade;
        playerName = player.playername;
        
        // get health, faith, agility
        playerHealth = player.playerHealth;
        playerFaith = player.playerFaith;
        playerAgility = player.playerAgility;

    }

    public void Info()
    {
        Sys.WSMNL("INFORMATION: ");
        Sys.WSMNL("1. Story Narration is given in BLUE (Debug.WSMNL())");
        Sys.WSMNL("2. Dialouge is given in YELLOW (Warn.WWMNL())");
        Sys.WSMNL("3. Combat is given in RED (Error.WEMNL())");
        Sys.WSMNL("\n ENTER TO BEGIN...");

        Console.ReadLine(); // wait for user input
        
        Sys.WSM("\n");
    }

    public void Start()
    {
        // start based on selections made
        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
                Knight.Beginning();
                break;
            case PlayerClass.SORCERER:
                Sorcerer.Beginning();
                break;
            case PlayerClass.WARLOCK:
                Warlock.Beginning();
                break;
        }
    }

    public void GAME()
    {
        SCENE_ONE();
        SCENE_TWO();
        SCENE_THREE();
    }

    private void SCENE_ONE()
    {
        switch (pl_class)
        {
            case PlayerClass.KNIGHT:
                Knight.K_SCENE_ONE();
                break;
            case PlayerClass.SORCERER: // TODO: IMPLEMENT
                break;
            case PlayerClass.WARLOCK:
                break;
        }
    }
    private void SCENE_TWO()
    {}
    private void SCENE_THREE()
    {}

}