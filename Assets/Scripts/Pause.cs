using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    public Rect windowRect;
    bool isPaused = false;
    AudioSource audio;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
    void Start() {
        windowRect = new Rect((Screen.width-60)*0.5f, (Screen.height-60)*0.5f, 120, 170);
        audio = GetComponent<AudioSource>();
    }
    void OnGUI()
    {
        if (isPaused)
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "Pauza");
    }
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Nastavi"))
        {
            isPaused = false;
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(10, 50, 100, 20), "Glavni Meni"))
        {
            Time.timeScale = 1;
            Application.LoadLevel(0);
        }
        if (GUI.Button(new Rect(10, 80, 100, 20), "Restart"))
        {
            Time.timeScale = 1;
            Application.LoadLevel(Application.loadedLevel);
        }
        if (GUI.Button(new Rect(10, 110, 100, 20), "Izadji"))
        {
            Application.Quit();
        }
        if (GUI.Button(new Rect(60, 140, 50, 20), "Zvuk"))
        {
            if (AudioListener.volume!=0)
                AudioListener.volume = 0;
            else
                AudioListener.volume = 0.5F;
        }

    }
}
