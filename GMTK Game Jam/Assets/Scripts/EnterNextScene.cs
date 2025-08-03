using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextScene : MonoBehaviour, IInteractable

{
    public string sceneToLoad;

    public void Interact()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}

