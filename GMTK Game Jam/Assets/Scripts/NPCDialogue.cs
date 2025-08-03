using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public GameObject Dialogue;
    public Dialogue dialogue;

    bool hasInteracted = false;

    public void Interact()
    {
        if (hasInteracted) return;

        if (Dialogue != null && dialogue != null)
        {
            Dialogue.SetActive(true);
            dialogue.StartDialogue();
            hasInteracted = true;
        }
    }
}
