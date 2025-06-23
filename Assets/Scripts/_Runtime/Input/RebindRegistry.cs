using System.Collections.Generic;

public static class RebindRegistry
{
    public static List<RebindUI> AllRebinds = new();

    public static void Register(RebindUI ui)
    {
        if (!AllRebinds.Contains(ui))
            AllRebinds.Add(ui);
    }

    public static void Unregister(RebindUI ui)
    {
        AllRebinds.Remove(ui);
    }
}