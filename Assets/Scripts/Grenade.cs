using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float boomBoomForce;
    public float boomBoomTime;
    public int boomBoomDamage;
    public float boomBoomRadius;
    public GameObject boomBoomParticles;

    float elapsedTime;
    public LayerMask enemyMask;
    bool boom;
    
    void Start()
    {
        elapsedTime = boomBoomTime;
    }

    void Update()
    {
        elapsedTime -= Time.deltaTime;

        if(elapsedTime <= 0 && !boom)
        {
            boom = true;
            BoomBoom();
        }
    }

    void BoomBoom()
    {
        GameObject bibgbe = Instantiate(boomBoomParticles, transform);
        bibgbe.transform.SetParent(null);

        Collider[] col = Physics.OverlapSphere(transform.position, boomBoomRadius, enemyMask);

        if(col.Length > 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                if(col[i].GetComponent<Enemy>() != null)
                {
                    col[i].GetComponent<Enemy>().hp -= boomBoomDamage;
                    
                    col[i].GetComponent<Rigidbody>().AddExplosionForce(boomBoomForce, transform.position, boomBoomRadius);
                }
                if(col[i].GetComponentInParent<Boss>() != null)
                {
                    col[i].GetComponentInParent<Boss>().hp -= boomBoomDamage;
                }
                if(col[i].GetComponent<PlayerShit>() != null)
                {
                    col[i].GetComponent<PlayerShit>().hp -= boomBoomDamage;
                }
            }
        }

        Destroy(bibgbe, 6);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == enemyMask || collision.gameObject.GetComponent<Enemy>() != null || collision.gameObject.GetComponent<PlayerShit>() != null || collision.gameObject.GetComponentInParent<Boss>() != null)
        {
            boom = true;
            BoomBoom();
        }
    }
}
