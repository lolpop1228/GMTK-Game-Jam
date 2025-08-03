using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour
{
    public GameObject doorIn;
    public GameObject doorOut;

    void Start()
    {
        if (doorIn != null && doorOut != null)
        {
            doorIn.SetActive(false);
            doorOut.SetActive(false);
        }
    }

    public void DoorActivate()
    {
        if (doorIn != null && doorOut != null)
        {
            doorIn.SetActive(true);
            doorOut.SetActive(true);
        }
    }
}
