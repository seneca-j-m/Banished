namespace BanishedMain;

public abstract class Menu
{
    protected abstract void PrintMenu();
    protected abstract void Setup(); // cosmetic

}

public class CosmeticMenu
{
    protected void OnBootCosmetics()
    {
        
    }

    internal static void writeTitleCosmetics(string titleMessage)
    {
        Console.Clear();
        Sys.WSMNL("<------------------------------------------->");
        Sys.WSMNL(titleMessage.ToUpper());
        Sys.WSMNL("<------------------------------------------->");
        Console.WriteLine("\n\n\n");
    }
}

internal class StartMenu : Menu
{
    private static Dictionary<int, Action> _MenuItems = new Dictionary<int, Action>();

    internal StartMenu() // constructor
    {
        Setup();
    }

    protected override void Setup() // initial setup
    {
        Sys.WSM("Booting");
        Sys.WSMDNL("...");
    }

    protected override void PrintMenu()
    {
        throw new NotImplementedException();
    }
}

internal class DebugMenu : Menu
{
    private static Dictionary<int, Action> _menuItems = new Dictionary<int, Action>()
    {
        // { 1, () => DebugMenu() }
    };
    internal DebugMenu()
    {
        Debug.WDM("DEBUG MENU INVOKED!!");
    }

    protected override void PrintMenu()
    {
        throw new NotImplementedException();
    }

    protected override void Setup()
    {
        throw new NotImplementedException();
    }
}