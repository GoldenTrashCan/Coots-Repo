using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnBoss : MonoBehaviour
{
    public void BOSSMODEON()
    {
        EnemySpawning.instance.BossOn();
    }
}
