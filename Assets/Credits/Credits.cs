using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    float eTime;
    void Update()
    {
        eTime += Time.deltaTime;

        if(eTime >= 74)
        {
            SceneManager.LoadScene(0);
        }
    }
}
