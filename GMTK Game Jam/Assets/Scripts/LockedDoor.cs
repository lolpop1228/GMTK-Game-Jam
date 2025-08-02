using System.Collections;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public bool isLocked = true;
    public string requiredKeyName = "GoldKey";

    public void Interact()
    {
        if (PlayerInventory.HasItem(requiredKeyName))
        {
            Debug.Log("Door unlocked!");
            isLocked = false;
            OpenDoor();
        }
        else
        {
            Debug.Log("It's locked. You need a key.");
            // You can show UI here instead
        }
    }

    private void OpenDoor()
    {
        // Your door opening logic here, e.g.:
        gameObject.SetActive(false); // Simple "open" behavior
    }
}
