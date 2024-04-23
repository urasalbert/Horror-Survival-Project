using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FacePlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            rotation.x = 0;
            rotation.z = 0;

            transform.rotation = rotation;
        }
    }
}
