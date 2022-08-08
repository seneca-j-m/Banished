namespace External;

// public interface Menu
// {
//     protected static Dictionary<int, Action> _menuItems;
//     protected static int sasdsasda;
//
//     protected void printMenu(Dictionary<int, Action> menuItems);
// }

public abstract class Menu
{
    protected abstract void PrintMenu();
    protected abstract void Setup(); // cosmetic

}

internal class StartMenu : Menu
{
    private static Dictionary<int, Action> _MenuItems = new Dictionary<int, Action>();

    internal StartMenu()
    {
        Setup();
    }

    protected override void Setup()
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