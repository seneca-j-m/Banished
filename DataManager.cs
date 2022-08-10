using System;
using System.Net;
using System.Reflection.Metadata;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace External;

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

            // create user db
            if (!File.Exists(GDirectories.userDBPath))
                File.Create(GDirectories.userDBPath);
            
            if (!File.Exists(GDirectories.playerDBPath))
                File.Create(GDirectories.playerDBPath);

            if (!File.Exists(GDirectories.loggerFPath))
                File.Create(GDirectories.loggerFPath);

            if (!File.Exists(GDirectories.loggerBFPath))
                File.Create(GDirectories.loggerBFPath);

            // using (var userFileStream = File.Create(GDirectories.userDBPath)) ;
            // using (var playerFileStream = File.Create(GDirectories.playerPath)) ;
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
                comm.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMRARY KEY,
                        firstname TEXT NOT NULL,
                        lastname TEXT NOT NULL,
                        age INTEGER NOT NULL 
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
            using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
            {
                Debug.WDMNL("Connection Initialsed: playerDB");
                var comm = conn.CreateCommand();
                comm.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS players (
                        id INTEGER PRIMARY KEY,
                        userid INTEGER NOT NULL,
                        playername TEXT NOT NULL,
                        playerclass TEXT NOT NULL,
                    )
                    ";
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
                    userDBData.Add(readr.GetString(0));
                }
            }
        }

        return userDBData;
    }

    internal string GetPlayerDBData()
    {
        return "";
    }

    internal void WriteToUserDB(User user)
    {
        using (var conn = new SqliteConnection($@"Data Source={GDirectories.userDBPath}"))
        {
            conn.Open();
            var comm = conn.CreateCommand();
            comm.CommandText =
                $@"
                    INSERT INTO users (firstname, lastname, age) VALUES ({user.firstname}, {user.lastname}, {user.age});
                ";
            using (var readr = comm.ExecuteReader()) ;
        }
    }
}

public static class GDirectories
{
    public const string userPath = @"../../../User/";
    public const string userDBPath = @"../../../User/user.sqlite";
    public const string playerPath = @"../../../Player/";
    public const string playerDBPath = @"../../../Player/player.sqlite";
    public const string loggerPath = @"../../../Logger";
    public const string loggerFPath = @"../../../Logger/log.log";
    public const string loggerBPath = @"../../../Logger/";
    public const string loggerBFPath = @"../../../Logger/log_backup.log";
}