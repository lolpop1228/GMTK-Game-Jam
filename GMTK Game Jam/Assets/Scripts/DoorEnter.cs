using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour, IInteractable
{
    public Transform entrancePoint;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        Teleport();
    }

    private void Teleport()
    {
        if (player != null && entrancePoint != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                player.transform.position = entrancePoint.position;
                player.transform.rotation = entrancePoint.rotation;
                cc.enabled = true;
            }
        }
    }
}
