using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockText : MonoBehaviour
{
    void OnEnable()
    {
        Destroy(gameObject, 2f);
    }
}
