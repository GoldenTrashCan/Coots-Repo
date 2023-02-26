using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().hp -= damage;
        }
        if(collision.gameObject.GetComponentInParent<Boss>() != null)
        {
            collision.gameObject.GetComponentInParent<Boss>().hp -= damage;
        }

        Destroy(gameObject);
    }
}
