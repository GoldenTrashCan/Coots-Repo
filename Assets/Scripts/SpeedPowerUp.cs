using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
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
        man.gameObject.GetComponentInChildren<PlayerController>().groundSpeed *= multiplier;
    }
}
