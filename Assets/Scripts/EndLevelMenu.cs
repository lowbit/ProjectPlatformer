using UnityEngine;
using System.Collections;

public class EndLevelMenu : MonoBehaviour
{
    public Rect windowRect;
    bool lvlEnding = false;
    void Start()
    {
        windowRect = new Rect((Screen.width - 60) * 0.5f, (Screen.height - 60) * 0.5f, 120, 120);
    }
    void Update()
    {
        if (lvlEnding)
            Time.timeScale = 0;
    }
    void OnGUI()
    {
        if (lvlEnding)
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "Kraj levela");
    }
    void DoMyWindow(int windowID)
    {

        if (Application.loadedLevel<Application.levelCount-1)
        {
            if (GUI.Button(new Rect(10, 20, 100, 20), "Sljedeci level"))
            {
                lvlEnding = false;
                Time.timeScale = 1;
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }
        else
        {
            GUI.Label(new Rect(15, 20, 140, 24), "Igra zavrsena!");  
        }
        if (GUI.Button(new Rect(10, 50, 100, 20), "Restart"))
        {
            lvlEnding = false;
            Time.timeScale = 1;
            Application.LoadLevel(Application.loadedLevel);
        }
        if (GUI.Button(new Rect(10, 80, 100, 20), "Izadji"))
        {
            Application.Quit();
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lvlEnding = true;
        }
    }
}