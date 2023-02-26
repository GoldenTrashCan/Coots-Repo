using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EnemySpawning : MonoBehaviour
{
    public bool testing;

    public static EnemySpawning instance;

    public List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> bossSpawnPoints = new List<Transform>();
    public AnimationCurve spawnRateCurve;
    public AnimationCurve speedRangeCurve;
    public AnimationCurve healthRangeCurve;
    public AnimationCurve spawnAmountCurve;

    public GameObject missilePrefab;
    public GameObject enemyPrefab;
    public GameObject bossEnemyPrefab;
    public GameObject missleBoomParcle;

    public List<GameObject> powerUps;
    public float powerUpChance;

    public float spawnElapsedTime;
    public float elapsedTime;
    public float endTime;
    public int hpRandom;
    public float speedRandom;
    public bool bossOn;
    public bool started;

    public AudioSource music;
    public AudioClip musica;
    public TextMeshProUGUI time;

    public bool cutscenePlaying;

    bool movePlayer;

    public PlayableDirector gayAssCutscenePart1;
    public PlayableDirector gayAssCutscenePart2;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (started)
        {
            if (!Boss.instance.dead && !testing)
            {
                spawnElapsedTime -= Time.deltaTime;
                elapsedTime += Time.deltaTime;
        
                if(elapsedTime < endTime)
                {
                    time.text = ((int)(120f - elapsedTime)).ToString();
                }

                if (spawnElapsedTime <= 0 && elapsedTime < endTime)
                {
                    spawnElapsedTime = spawnRateCurve.Evaluate(elapsedTime);
                    Spawn();
                }

                if(elapsedTime >= endTime && !cutscenePlaying && !bossOn)
                {
                    cutscenePlaying = true;
                    gayAssCutscenePart1.Play();

                }

                if(elapsedTime >= endTime + 10 && !movePlayer)
                {
                    print("balls shit");
                    movePlayer = true;
                    PlayerController.instance.playerShit.gameObject.transform.position = new Vector3(0, 4.02f, 67.75f);
                    PlayerController.instance.orientation.eulerAngles = new Vector3(0, 0, 0);
                    PlayerController.instance.moveDirection = Vector3.zero;
                    PlayerController.instance.rb.velocity = Vector3.zero;
                    PlayerController.instance.mainCat.SetActive(false);
                    PlayerController.instance.cootFly.SetActive(true);
                }


                if(elapsedTime >= endTime && bossOn)
                {
                    if (!music.isPlaying)
                    {
                        music.clip = musica;
                        music.loop = true;
                        music.Play();
                    }
                    if(spawnElapsedTime <= 0)
                    {
                        spawnElapsedTime = spawnRateCurve.Evaluate(elapsedTime);
                        BossSpawn();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                BossOff();
            }
        }
    }

    void Spawn()
    {
        for (int i = 0; i < spawnAmountCurve.Evaluate(elapsedTime); i++)
        {
            if(Random.Range(1, 100) <= powerUpChance)
            {
                GameObject farrt = Instantiate(powerUps[Random.Range(0, powerUps.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)]);
                farrt.transform.SetParent(null);
                continue;
            }

            GameObject fopokkofko = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)]);
            fopokkofko.transform.SetParent(null);
            fopokkofko.GetComponent<Enemy>().hp = (int)healthRangeCurve.Evaluate(elapsedTime) + Random.Range(-hpRandom, hpRandom);
            fopokkofko.GetComponent<Enemy>().speed = speedRangeCurve.Evaluate(elapsedTime) + Random.Range(-speedRandom, speedRandom);
        }
    }

    void BossSpawn()
    {
        for (int i = 0; i < spawnAmountCurve.Evaluate(elapsedTime); i++)
        {
            if(Random.Range(1, 100) <= powerUpChance)
            {
                GameObject farrt = Instantiate(powerUps[Random.Range(0, powerUps.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)]);
                farrt.transform.SetParent(null);
                continue;
            }
            if(Random.Range(0, 29) < 2)
            {
                GameObject misl = Instantiate(missilePrefab, bossSpawnPoints[Random.Range(0, bossSpawnPoints.Count)]);
                misl.transform.SetParent(null);
                misl.GetComponent<Enemy>().hp = 1;
                misl.GetComponent<Enemy>().deathParticles = missleBoomParcle;
            }

            GameObject fopokkofko = Instantiate(bossEnemyPrefab, bossSpawnPoints[Random.Range(0, bossSpawnPoints.Count)]);
            fopokkofko.transform.SetParent(null);
            fopokkofko.GetComponent<Enemy>().hp = (int)healthRangeCurve.Evaluate(elapsedTime) + Random.Range(-hpRandom, hpRandom);
            fopokkofko.GetComponent<Enemy>().speed = speedRangeCurve.Evaluate(elapsedTime) + Random.Range(-speedRandom, speedRandom);

        }
    }

    public void BossOn()
    {
        bossOn = true;
        cutscenePlaying = false;
        PlayerController.instance.bossMode = true;
        PlayerController.instance.rb.useGravity = false;
        PlayerController.instance.rb.drag = 10;
    }

    public void BossOff()
    {
        music.pitch = 1;
        PlayerController.instance.rb.useGravity = true;
        PlayerController.instance.rb.drag = 4;
    }

    public void BossDead()
    {
        gayAssCutscenePart2.Play();
        BossOff();
    }

    public void Win()
    {
        SceneManager.LoadScene(2);
    }
}
