namespace BanishedMain;

public class Player
{
    // database data
    public string playername { get; }
    public DefaultPlayerRace playerrace { get; }
    public DefaultPlayerClass playerclass { get; }
    public DefaultPlayerAccolade playeraccolade { get; }

    public int playerHealth;
    public int playerFaith;
    public int playerAgility;
    
    public int playerMorality;
    public int playerHonour;
    public int playerDishonour;
    public int playerGrace;

    public int playerFunds; // gold

    public Player(string _playername, DefaultPlayerRace _playerrace, DefaultPlayerClass _playerclass, DefaultPlayerAccolade _playeraccolade)
    {
        playername = _playername;
        playerrace = _playerrace;
        playerclass = _playerclass;
        playeraccolade = _playeraccolade;

        // TODO: MAKE SPECIFIC FOR EACH CLASS
        playerMorality = 50; // neutral
        playerHonour = 100;
        playerDishonour = 0;
        playerGrace = 60; // blessed

        playerFunds = 100;
        
        // assign critical values
        switch (playerclass)
        {
            case DefaultPlayerClass.KNIGHT:
                playerHealth = Knight.base_health;
                playerFaith = Knight.base_faith;
                playerAgility = Knight.base_agility;
                break;
            case DefaultPlayerClass.SORCERER:
                playerHealth = Sorcerer.base_health;
                playerFaith = Sorcerer.base_faith;
                playerAgility = Sorcerer.base_agility;
                break;
            case DefaultPlayerClass.WARLOCK:
                playerHealth = Warlock.base_health;
                playerFaith = Warlock.base_faith;
                playerAgility = Warlock.base_agility;
                break;
        }
    }

    public Player()
    {
        playername = String.Empty;
        playerrace = DefaultPlayerRace.EMPTY;
        playerclass = DefaultPlayerClass.EMPTY;
        playeraccolade = DefaultPlayerAccolade.EMPTY;
        
        playerHealth = 0;
        playerFaith = 0;
        playerAgility = 0;

        playerMorality = 0;
        playerHonour = 0;
        playerDishonour = 0;
        playerGrace = 0;
    }
}


// player enums

public enum DefaultPlayerRace
{
    EMPTY,
    ELF,
    HUMAN,
    ORC,
    
}

public enum DefaultPlayerClass
{
    EMPTY,
    KNIGHT,
    SORCERER,
    WARLOCK
}

public enum DefaultPlayerAccolade
{
    EMPTY,
    WARRIOR,
    SCHOLAR,
    ACOLYTE
}

public struct PlayerClass
{
    public string className { get; }
    public int classHealth { get; }
    public int classFaith { get; }
    public int classAgility { get; }
    public string classDescription { get; }
    

    public PlayerClass(string _className, int _classHealth, int _classFaith, int _classAgility, string _classDescription)
    {
        className = _className;
        classHealth = _classHealth;
        classFaith = _classFaith;
        classAgility = _classAgility;
        classDescription = _classDescription;
    }

}

public struct PlayerRace
{
    public string raceName { get; }
    public string raceDescription { get; }
    public List<RaceProficiency> raceProficencies { get; }
    public List<DefaultRaceProficiency> draceProficencies { get; }

    public PlayerRace(string _raceName, string _raceDescription, List<RaceProficiency> _raceProficencies)
    {
        raceName = _raceName;
        raceDescription = _raceDescription;
        raceProficencies = _raceProficencies;

        draceProficencies = new List<DefaultRaceProficiency>();
    }

    public PlayerRace(string _raceName, string _raceDescription, List<DefaultRaceProficiency> _draceProficencies)
    {
        raceName = _raceName;
        raceDescription = _raceDescription;
        draceProficencies = _draceProficencies;

        raceProficencies = new List<RaceProficiency>();
    }

    public PlayerRace()
    {
        raceName = "";
        raceDescription = "";
        raceProficencies = new List<RaceProficiency>();
        draceProficencies = new List<DefaultRaceProficiency>();
    }
}

