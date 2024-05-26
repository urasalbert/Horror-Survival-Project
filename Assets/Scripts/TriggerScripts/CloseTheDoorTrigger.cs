using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTheDoorTrigger : MonoBehaviour
{
    public AudioSource AudioSource;
    public bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            AudioSource.Play();
            DoorController.Instance.CloseDoor();
            isTriggered = true;
        }
    }
}

