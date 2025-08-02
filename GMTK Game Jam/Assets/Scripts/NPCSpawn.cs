using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (npcPrefab != null && spawnPoint != null)
        {
            Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject);
        }
    }
}
