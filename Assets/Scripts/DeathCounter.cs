using UnityEngine;
using System.Collections;

public class DeathCounter : MonoBehaviour {
    public int counter;
    public GUIText textCounter;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        counter = 1;
        UpdateCounter();
    }
    public void Count(int c)
    {
        counter += c;
        UpdateCounter();
    }
    void UpdateCounter()
    {
        player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        textCounter.text = "Pokusaj: " + counter;
    }
}
