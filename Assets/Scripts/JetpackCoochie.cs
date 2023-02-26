using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackCoochie : MonoBehaviour
{
    public float smoothTime;
    public float pitchThreshold;
    public float tiltAmount;

    public AudioSource jetpackAudio;

    void Update()
    {
        Vector3 moveDir = PlayerController.instance.moveDirection;
        Vector3 poopAngle = moveDir.z * transform.right * tiltAmount + moveDir.x * transform.forward * tiltAmount;
        Quaternion poopQ = new Quaternion(poopAngle.x, 0, -poopAngle.z, transform.rotation.w);

        transform.rotation = Quaternion.Lerp(transform.rotation, poopQ, Time.deltaTime * smoothTime);

        jetpackAudio.pitch = 1 + (PlayerController.instance.rb.velocity.magnitude / pitchThreshold);
    }
}
