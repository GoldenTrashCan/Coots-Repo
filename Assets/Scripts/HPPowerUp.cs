using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPowerUp : PowerUp
{
    public float multiplier;

    void Start()
    {

    }

    public override void Update()
    {
        base.Update();
    }

    public override void DoShit(GameObject man)
    {
        man.gameObject.GetComponent<PlayerShit>().maxHp = (int)(man.gameObject.GetComponent<PlayerShit>().maxHp * multiplier);
        man.gameObject.GetComponent<PlayerShit>().hp = man.gameObject.GetComponent<PlayerShit>().maxHp;
    }
}
