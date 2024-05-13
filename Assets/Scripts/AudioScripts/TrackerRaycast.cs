using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class TrackerRaycast : MonoBehaviour
{
    public static TrackerRaycast Instance { get; private set; }

    public float raycastDistance = 100f;
    public bool isEnemySighted;
     public List<GameObject> trackedEnemies = new List<GameObject>();
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        isEnemySighted = false;
    }
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, ~LayerMask.GetMask("Trigger")))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Tracker")
                {
                    isEnemySighted = true;
                    TrackerSightSound.Instance.PlaySightSound();
                }
            }
        }
    }
}