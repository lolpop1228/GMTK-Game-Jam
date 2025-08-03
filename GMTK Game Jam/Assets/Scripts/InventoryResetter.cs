using UnityEngine;

public class InventoryResetter : MonoBehaviour
{
    void Start()
    {
        PlayerInventory.ClearInventory();
    }
}
