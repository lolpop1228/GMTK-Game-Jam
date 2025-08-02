using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    public string keyName = "GoldKey";

    public void Interact()
    {
        PlayerInventory.AddItem(keyName);
        Destroy(gameObject); // Remove key from the scene
    }
}
