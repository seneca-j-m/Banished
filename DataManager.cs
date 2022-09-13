using System;
using System.Net;
using System.Reflection.Metadata;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace BanishedMain;

public class DataManager
{
    internal void InitFilesystem()
    {
        // verify directories
        try
        {
            // create user directory (dont need to check if it does or doesnt exist)
            Directory.CreateDirectory(GDirectories.userPath);
            Directory.CreateDirectory(GDirectories.playerPath);
            Directory.CreateDirectory(GDirectories.loggerPath);
            Directory.CreateDirectory(GDirectories.loggerBPath);
            Directory.CreateDirectory(GDirectories.dataPath);
            Directory.CreateDirectory(GDirectories.playerRaceData);
            Directory.CreateDirectory(GDirectories.playerClassData);
            Directory.CreateDirectory(GDirectories.playerAccoladeData);

            // files
            if (!File.Exists(GDirectories.userDBPath))
                File.Create(GDirectories.userDBPath);
            
            if (!File.Exists(GDirectories.playerDBPath))
                File.Create(GDirectories.playerDBPath);

            if (!File.Exists(GDirectories.loggerFPath))
                File.Create(GDirectories.loggerFPath);

            if (!File.Exists(GDirectories.loggerBFPath))
                File.Create(GDirectories.loggerBFPath);
            
            // player construction files
            
            // race data
            if (!File.Exists(GDirectories.playerRaceHumanDataF))
                File.Create(GDirectories.playerRaceHumanDataF);

            if (!File.Exists(GDirectories.playerRaceElfDataF))
                File.Create(GDirectories.playerRaceElfDataF);

            if (!File.Exists(GDirectories.playerRaceOrcDataF))
                File.Create(GDirectories.playerRaceOrcDataF);

            // class data
            if (!File.Exists(GDirectories.playerClassKnightDataF))
                File.Create(GDirectories.playerClassKnightDataF);

            if (!File.Exists(GDirectories.playerClassSorcererDataF))
                File.Create(GDirectories.playerClassSorcererDataF);

            if (!File.Exists(GDirectories.playerClassWarlockDataF))
                File.Create(GDirectories.playerClassWarlockDataF);
            
            // accolade data
            if (!File.Exists(GDirectories.playerAccoladeWarriorDataF))
                File.Create(GDirectories.playerAccoladeWarriorDataF);

            if (!File.Exists(GDirectories.playerAccoladeScholarDataF))
                File.Create(GDirectories.playerAccoladeScholarDataF);

            if (!File.Exists(GDirectories.playerAccoladeAcolyteDataF))
                File.Create(GDirectories.playerAccoladeAcolyteDataF);



        }
        catch (Exception e)
        {
            throw new SaveFileCreationFailed(e);
        }
    }
}

public class DBManager
{
    //protected SqliteConnection DBConnection = new SqliteConnection(GDirectories.userDBPath);
    internal DBManager()
    {
    }

    ~DBManager() // just in caseys
    {
        //DBConnection.Close();
    }

    internal void InitUserDB()
    {
        try
        {
            using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
            {
                conn.Open();
                Debug.WDMNL("Connection Initialised: userDB");
                var comm = conn.CreateCommand();
                comm.CommandText = // TODO: ADD USER ID SECTION
                    @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY,
                        firstname TEXT NOT NULL,
                        lastname TEXT NOT NULL,
                        age INTEGER NOT NULL,
                        password TEXT NOT NULL 
                    );
                    ";
                var readr = comm.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            throw new DatabaseConnectionFailed(e);
        }
    }

    internal void InitPlayerDB()
    {
        try
        {
            using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
            {
                conn.Open();
                Debug.WDMNL("Connection Initialsed: playerDB");
                var comm = conn.CreateCommand();
                comm.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS players (
                        id INTEGER PRIMARY KEY,
                        userid INTEGER NOT NULL,
                        playername TEXT NOT NULL,
                        playerrace TEXT NOT NULL,
                        playerclass TEXT NOT NULL
                    )
                    ";
                var readr = comm.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            throw new DatabaseConnectionFailed(e);
        }
    }

    internal List<string> GetUserDBData()
    {
        List<string> userDBData = new List<string>();

        using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                @"
                    SELECT * FROM users;
                ";
            using (var readr = comm.ExecuteReader())
            {
                while (readr.Read())
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        userDBData.Add(readr.GetValue(i).ToString());
                    }
                }
            }
        }

        return userDBData;
    }

    internal List<string> GetPlayerDBData()
    {
        List<string> playerDBData = new List<string>();

        using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                @"
                    SELECT * FROM players;
                ";
            using (var readr = comm.ExecuteReader())
            {
                while (readr.Read())
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        playerDBData.Add(readr.GetValue(i).ToString());
                    }
                }
            }
        }
        
        return playerDBData;
    }

    internal void WriteToUserDB(User user)
    {
        using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                $@"
                    INSERT INTO users (firstname, lastname, age, password) VALUES ('{user.firstname}', '{user.lastname}', '{user.age}', '{user.password}');
                ";
            using (var readr = comm.ExecuteReader()) ;
        }
    }
    
    internal void WriteToPlayerDB(Player player)
    {
        using (var conn = new SqliteConnection($@"Data Source={GDirectories.playerDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                $@"
                    INSERT INTO players (userid, playername, playerrace, playerclass) VALUES ('{player.userid}', '{player.playername}', '{player.playerrace}', '{player.playerclass}');
                ";
            using (var readr = comm.ExecuteReader()) ;
        }
    }
}

public static class GDirectories
{
    // user data and database paths
    public const string userPath = @"../../../User/";
    public const string userDBPath = @"../../../User/user.sqlite";
    
    // player data and database paths
    public const string playerPath = @"../../../Player/";
    public const string playerDBPath = @"../../../Player/player.sqlite";
    
    // logger paths
    public const string loggerPath = @"../../../Logger";
    public const string loggerFPath = @"../../../Logger/log.log";
    public const string loggerBPath = @"../../../Logger/Backup";
    public const string loggerBFPath = @"../../../Logger/Backup/log_backup.log"; // watch this chief
    
    // primary data path
    public const string dataPath = @"../../../Data/";
    
    // player construction data directories
    public const string playerRaceData = @"../../../Data/playerRaceData/";
    public const string playerClassData = @"../../../Data/playerClassData/";
    public const string playerAccoladeData = @"../../../Data/playerAccoladeData/";
        
    // player race data files
    public const string playerRaceHumanDataF = @"../../../Data/playerRaceData/human.txt";
    public const string playerRaceElfDataF = @"../../../Data/playerRaceData/elf.txt";
    public const string playerRaceOrcDataF = @"../../../Data/playerRaceData/orc.txt";

    // player class data files
    public const string playerClassKnightDataF = @"../../../Data/playerClassData/knight.txt";
    public const string playerClassSorcererDataF = @"../../../Data/playerClassData/sorcerer.txt";
    public const string playerClassWarlockDataF = @"../../../Data/playerClassData/warlock.txt";
    
    // player accolad data files
    public const string playerAccoladeWarriorDataF = @"../../../Data/playerAccoladeData/warrior.txt";
    public const string playerAccoladeScholarDataF = @"../../../Data/playerAccoladeData/scholar.txt";
    public const string playerAccoladeAcolyteDataF = @"../../../Data/playerAccoladeData/acolyte.txt";
}