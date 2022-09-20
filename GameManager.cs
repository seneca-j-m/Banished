namespace BanishedMain;

public static class GameManager
{
    public static string readBeginning(PlayerClass pl_class)
    {
        string[] fileContent = File.ReadAllLines(GDirectories.playerClassKnightDataF);

        // LINQ query
        var beginningContent = fileContent.SkipWhile(s => s != "BEGINNING").TakeWhile(s => s != "BEGINNINGEND");
        string beginningString = String.Join("\n", beginningContent.ToArray());

        return beginningString;
    }
    
    // TODO: FINISH THIS
    public static string readDescription(PlayerClass pl_class)
    {
        string[] fileContent = File.ReadAllLines(GDirectories.playerClassKnightDataF);

        // LINQ query
        var beginningContent = fileContent.SkipWhile(s => s != "BEGINNING").TakeWhile(s => s != "BEGINNINGEND");
        string beginningString = String.Join("\n", beginningContent.ToArray());

        return beginningString;
    }
    
}