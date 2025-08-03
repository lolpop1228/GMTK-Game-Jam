using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour, IInteractable
{
    public Transform entrancePoint;
    public GameObject player;
    public AudioClip teleportSound; // Sound to play when teleporting
    private AudioSource audioSource; // AudioSource to play the sound

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>(); // Assumes the player has an AudioSource component
    }

    public void Interact()
    {
        Teleport();
        PlayTeleportSound();
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

    private void PlayTeleportSound()
    {
        if (teleportSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }
    }
}
