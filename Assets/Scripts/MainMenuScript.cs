using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public PlayableDirector start;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Play()
    {
        start.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
