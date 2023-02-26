using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerShit : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public bool dead;

    public Image hpRadial;
    public TextMeshProUGUI healthText;
    public PlayerController playerController;

    public PlayableDirector deadSequnce;
    public PlayableDirector tryaGainSequence;

    public List<GameObject> funShit = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        hpRadial.fillAmount = (((float)hp) / ((float)maxHp));
        healthText.text = hp.ToString();

        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp < 0)
        {
            hp = 0;
        }
        if(hp == 0 && !dead)
        {
            Cursor.lockState = CursorLockMode.None;
            dead = true;
            deadSequnce.Play();
            playerController.rb.useGravity = true;

            foreach (var item in funShit)
            {
                item.AddComponent<Rigidbody>();
                MeshCollider meshCol = item.AddComponent<MeshCollider>();
                meshCol.sharedMesh = item.GetComponent<MeshFilter>().mesh;
                meshCol.convex = true;
                item.transform.SetParent(null);
            }


            Collider[] col = Physics.OverlapSphere(transform.position, 25);

            if (col.Length > 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    if(col[i].GetComponent<Rigidbody>() != null)
                    {
                        col[i].GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 25);
                    }
                }
            }
        }
    }

    public void TryAgain()
    {
        tryaGainSequence.Play();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
}
