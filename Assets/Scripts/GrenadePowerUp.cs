using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePowerUp : PowerUp
{
    public int count;
    public Vector3 multiplier;

    void Start()
    {

    }

    public override void Update()
    {
        base.Update();
    }

    public override void DoShit(GameObject man)
    {
        man.gameObject.GetComponentInChildren<Gun>().grenadeCount += count;
        man.gameObject.GetComponentInChildren<Gun>().grenadePrefab.transform.localScale = multiplier;
    }
}
