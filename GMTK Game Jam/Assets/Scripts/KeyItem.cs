using TMPro;
using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    public string keyName = "GoldKey";
    public int requiredKeyAmount = 3; // Display target
    public TextMeshProUGUI keyCountText; // Assign in inspector

    public void Interact()
    {
        PlayerInventory.AddItem(keyName);

        int count = PlayerInventory.GetItemCount(keyName);
        if (keyCountText != null)
        {
            keyCountText.text = $"Keys: {count} / {requiredKeyAmount}";
            keyCountText.gameObject.SetActive(true);
        }

        Destroy(gameObject);
    }
}
