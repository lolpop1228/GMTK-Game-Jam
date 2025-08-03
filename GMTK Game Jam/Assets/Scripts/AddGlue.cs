using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddGlue : MonoBehaviour
{
    public string keyName = "Glue";
    public string textTag = "PickUpText";
    public TextMeshProUGUI itemPickupText;
    public float messageDuration = 2f;

    void Start()
    {
        GameObject textObj = GameObject.FindWithTag(textTag);
        if (textObj != null)
        {
            itemPickupText = textObj.GetComponent<TextMeshProUGUI>();
        }
    }

    public void GlueAdd()
    {
        PlayerInventory.AddItem(keyName);
        if (itemPickupText != null)
        {
            int count = PlayerInventory.GetItemCount(keyName);
            itemPickupText.text = $"Glue Acquried!";
            itemPickupText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterDelay());
        }
    }

    private System.Collections.IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        itemPickupText.gameObject.SetActive(false);
    }
}
