using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniJumpScareTrigger : MonoBehaviour
{
    public static MiniJumpScareTrigger Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the character
        if (other.CompareTag("Player"))
        {
            // Start the MoveForward singleton instance
            if (MoveForward.Instance != null)
            {
                MoveForward.Instance.GameObject.SetActive(true);
                MoveForward.Instance.StartMoving();
            }
        }
    }
}
