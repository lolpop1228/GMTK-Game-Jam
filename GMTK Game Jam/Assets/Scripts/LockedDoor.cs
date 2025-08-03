using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public bool isLocked = true;

    public string requiredKeyName = "GoldKey";
    public int requiredKeyAmount = 3;

    public string requiredGlueName = "Glue";
    public int requiredGlueAmount = 1;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        bool hasEnoughKeys = PlayerInventory.HasItem(requiredKeyName, requiredKeyAmount);
        bool hasEnoughGlue = PlayerInventory.HasItem(requiredGlueName, requiredGlueAmount);

        if (hasEnoughKeys && hasEnoughGlue)
        {
            Debug.Log("Door unlocked!");
            isLocked = false;
            PlayerInventory.RemoveItem(requiredKeyName, requiredKeyAmount);
            PlayerInventory.RemoveItem(requiredGlueName, requiredGlueAmount);
            OpenDoor();
        }
        else
        {
            int currentKeys = PlayerInventory.GetItemCount(requiredKeyName);
            int currentGlue = PlayerInventory.GetItemCount(requiredGlueName);

            Debug.Log($"It's locked. You need {requiredKeyAmount} GoldKeys and {requiredGlueAmount} Glue.");
            Debug.Log($"You have {currentKeys} GoldKeys and {currentGlue} Glue.");
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
            // You can also show this with a UI popup instead
        }
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false); // Replace with animation if needed
    }
}
