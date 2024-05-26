using UnityEngine;
using System.Collections;

public class TeleportObject : MonoBehaviour
{
    public GameObject objectToTeleport;
    public Transform teleportLocation;
    private Vector3 originalPosition;
    private bool hasTeleported = false;

    private void Start()
    {
        if (objectToTeleport != null)
        {
            originalPosition = objectToTeleport.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTeleported)
        {
            hasTeleported = true;
            StartCoroutine(TeleportForOneSecond());
        }
    }

    private IEnumerator TeleportForOneSecond()
    {
        if (objectToTeleport != null)
        {
            objectToTeleport.transform.position = teleportLocation.position;
            yield return new WaitForSeconds(1.0f);
            objectToTeleport.transform.position = originalPosition;
        }
    }
}
