using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float speed;
    float deltaTimeCompensation = 300;
    public Rigidbody rb;
    public GameObject deathParticles;
    public int damage;
    public float targetSpeed = 45;

    void Start()
    {
        
    }

    void Update()
    {
        if(hp <= 0 || Boss.instance.dead || EnemySpawning.instance.cutscenePlaying)
        {
            if (!Boss.instance.dead && !EnemySpawning.instance.cutscenePlaying)
            {
                GameObject fart = Instantiate(deathParticles, transform);
                fart.transform.SetParent(null);
                Destroy(fart, 2);
            }

            Destroy(gameObject);
        }

        Quaternion target = Quaternion.LookRotation(PlayerController.instance.transform.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * targetSpeed);

        rb.AddForce(transform.forward * speed * deltaTimeCompensation * Time.deltaTime, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerShit>() != null)
        {
            collision.gameObject.GetComponent<PlayerShit>().hp -= damage;

            GameObject fart = Instantiate(deathParticles, transform);
            fart.transform.SetParent(null);
            Destroy(fart, 2);
            Destroy(gameObject);
        }
    }
}
