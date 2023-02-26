using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject bulletSpawnPoint;

    public GameObject bulletPrefab;
    public GameObject grenadePrefab;

    public AudioSource audioSource;
    public AudioClip bulletClip;
    public AudioClip grenadeLauncherClip;

    float elapsedTime;
    float G_ElapsedTime;

    public float bulletSpeed;
    public float fireRate;
    public int grenadeCount;

    public float fireRateMax;
    public float bulletSpeedMax;

    public float bulletSpeedBase;
    public float fireRateBase;
    public float grenadeCooldown = 15;

    public TextMeshProUGUI nadeCount;

    public PlayerShit playerShit;

    void Start()
    {
        fireRate = fireRateBase;
        bulletSpeed = bulletSpeedBase;
        grenadeCount = 3;
        elapsedTime = fireRate;
    }

    void Update()
    {
        if (!Boss.instance.dead && !playerShit.dead)
        {
            nadeCount.text = grenadeCount.ToString();

            elapsedTime -= Time.deltaTime;

            G_ElapsedTime -= Time.deltaTime;

            if (Input.GetButton("Fire1") && elapsedTime <= 0)
            {
                elapsedTime = fireRate;
                Shoot();
            }
            if (Input.GetButtonDown("Fire2") && grenadeCount > 0)
            {
                Grenade();
            }

            if (fireRate < fireRateMax)
            {
                fireRate = fireRateMax;
            }
            if (bulletSpeed > bulletSpeedMax)
            {
                bulletSpeed = bulletSpeedMax;
            }

            if (G_ElapsedTime <= 0)
            {
                G_ElapsedTime = grenadeCooldown;
                grenadeCount++;
            }


            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            if(Physics.Raycast(ray, out hit))
            {
                Quaternion target = Quaternion.LookRotation(hit.point - bulletSpawnPoint.transform.position);
                bulletSpawnPoint.transform.rotation = Quaternion.Slerp(bulletSpawnPoint.transform.rotation, target, Time.deltaTime * 45);
            }
        }
    }

    public void Shoot()
    {
        audioSource.pitch = Random.Range(0.97f, 1.03f);
        audioSource.PlayOneShot(bulletClip);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.transform.forward * bulletSpeed, ForceMode.Impulse);
        bullet.transform.SetParent(null);
    }

    public void Grenade()
    {
        grenadeCount--;
        audioSource.pitch = Random.Range(0.97f, 1.03f);
        audioSource.PlayOneShot(bulletClip);
        audioSource.PlayOneShot(grenadeLauncherClip);
        GameObject bullet = Instantiate(grenadePrefab, bulletSpawnPoint.transform);

        if (PlayerController.instance.bossMode)
        {
            bullet.GetComponent<Grenade>().boomBoomDamage *= 7;
        }

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.transform.forward * bulletSpeed * 2, ForceMode.Impulse);
        bullet.transform.SetParent(null);
    }
}
