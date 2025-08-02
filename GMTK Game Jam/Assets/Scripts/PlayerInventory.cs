using System.Collections.Generic;

public static class PlayerInventory
{
    private static HashSet<string> collectedItems = new HashSet<string>();

    public static void AddItem(string itemName)
    {
        collectedItems.Add(itemName);
        UnityEngine.Debug.Log("Collected: " + itemName);
    }

    public static bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }
}
