using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyHelp : MonoBehaviour
{
    public GameObject music;
    public GameObject tutorialMusic;
    public TextMeshProUGUI tip;
    bool jetPack;
    void Start()
    {
        
    }

    void Update()
    {
        if (!EnemySpawning.instance.started && Input.GetButtonDown("Fire2"))
        {
            EnemySpawning.instance.started = true;
            tutorialMusic.SetActive(false);
            music.SetActive(true);
            tip.gameObject.SetActive(false);
        }
        if (EnemySpawning.instance.bossOn && !jetPack)
        {
            jetPack = true;
            tip.text = "Use E and Q for your jetpack";
            tip.gameObject.SetActive(true);
        }
        if(jetPack && Input.GetKeyDown(KeyCode.E) || jetPack && Input.GetKeyDown(KeyCode.Q))
        {
            tip.gameObject.SetActive(false);
        }
    }
}
