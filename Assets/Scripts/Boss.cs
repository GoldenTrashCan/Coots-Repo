using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    public int hp;
    public int maxHp;
    public GameObject deathParticles;

    public Slider healthBar;

    public bool dead;

    public AudioSource audioSource;
    public AudioClip grenadeLauncherClip;
    public GameObject grenadePrefab;
    public GameObject bulletSpawnPoint;
    public float bulletSpeed;

    public float cooldownAttack;

    public float elapsedTime;

    void Start()
    {
        instance = this;
        healthBar.maxValue = maxHp;
    }

    void Update()
    {
        if (PlayerController.instance.bossMode)
        {
            elapsedTime -= Time.deltaTime;
        }

        healthBar.value = hp;

        if(hp <= 0 && !dead)
        {
            dead = true;
            GameObject fart = Instantiate(deathParticles, transform);
            fart.transform.SetParent(null);
            EnemySpawning.instance.BossDead();
        }

        if (!dead && !EnemySpawning.instance.cutscenePlaying && PlayerController.instance.bossMode)
        {
            if(elapsedTime <= 0)
            {
                elapsedTime = cooldownAttack;
                Grenade();
            }

            Quaternion target = Quaternion.LookRotation(PlayerController.instance.transform.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 45);

            Quaternion fat = Quaternion.LookRotation(PlayerController.instance.transform.transform.position - bulletSpawnPoint.transform.position);
            bulletSpawnPoint.transform.rotation = Quaternion.Slerp(bulletSpawnPoint.transform.rotation, fat, Time.deltaTime * 45);
        }
    }

    void Grenade()
    {
        audioSource.PlayOneShot(grenadeLauncherClip);
        GameObject bullet = Instantiate(grenadePrefab, bulletSpawnPoint.transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.transform.forward * bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Grenade>().boomBoomParticles.transform.localScale = new Vector3(10, 10, 10);
        bullet.transform.SetParent(null);
    }
}
