using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public GameObject Dialogue;
    public Dialogue dialogue;

    public void Interact()
    {
        if (Dialogue != null && dialogue != null)
        {
            Dialogue.SetActive(true);
            dialogue.StartDialogue();
        }
    }
}
