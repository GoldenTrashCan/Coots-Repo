using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackRotationOffset : MonoBehaviour
{
    public float smoothTime;

    void Update()
    {
        Quaternion poopQ = PlayerController.instance.orientation.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, poopQ, Time.deltaTime * smoothTime);
    }
}
