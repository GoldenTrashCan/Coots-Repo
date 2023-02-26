using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject coolAssParticles;

    void Start()
    {
    }

    public virtual void Update()
    {
        bool check = Physics.CheckSphere(transform.position, 4, playerLayer);

        if (check)
        {
            DoShit(Physics.OverlapSphere(transform.position, 4, playerLayer)[0].gameObject);
            GameObject parcle = Instantiate(coolAssParticles, transform);
            parcle.transform.SetParent(null);
            Destroy(gameObject);
            Destroy(parcle, 5);
        }
    }

    public virtual void DoShit(GameObject man)
    {

    }
}
