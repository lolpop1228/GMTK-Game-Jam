using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToPlay;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneToPlay);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("quitted");
    }
}
