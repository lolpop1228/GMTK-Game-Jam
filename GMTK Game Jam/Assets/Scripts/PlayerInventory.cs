using System.Collections.Generic;
using UnityEngine;

public static class PlayerInventory
{
    private static Dictionary<string, int> items = new Dictionary<string, int>();

    public static void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items[itemName] = 1;
    }

    public static bool HasItem(string itemName, int requiredAmount = 1)
    {
        return items.ContainsKey(itemName) && items[itemName] >= requiredAmount;
    }

    public static int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    public static void RemoveItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] -= amount;
            if (items[itemName] <= 0)
                items.Remove(itemName);
        }
    }
}
