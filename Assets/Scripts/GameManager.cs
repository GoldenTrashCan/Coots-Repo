using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource meowSource;
    public AudioClip meowClip;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Meow();
        }
    }

    public void Meow()
    {
        meowSource.pitch = Random.Range(0.96f, 1.06f);
        meowSource.PlayOneShot(meowClip);
    }
}
