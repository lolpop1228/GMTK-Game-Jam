using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnterRoom : MonoBehaviour
{
    public Transform inRoomPoint;
    public GameObject npcPrefab;

    public void EnterRoom()
    {
        if (npcPrefab != null)
        {
            Instantiate(npcPrefab, inRoomPoint.position, inRoomPoint.rotation);
        }
    }
}
