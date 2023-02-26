using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectileRotation : MonoBehaviour
{
    public Rigidbody rb;
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rb.velocity, Vector3.up), 5);
    }
}
