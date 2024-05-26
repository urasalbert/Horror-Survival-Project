using System.Collections;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public static MoveForward Instance { get; private set; }

    public float speed = 5f;
    public float duration = 4f;
    private Rigidbody rb;
    public GameObject GameObject;

    private void Awake()
    {
        
        GameObject.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody>();
    }

    // Public method to start moving
    public void StartMoving()
    {
        if (rb != null)
        {
            StartCoroutine(MoveForwardForSeconds(duration));
        }
    }

    // Coroutine to move forward for a specified duration
    private IEnumerator MoveForwardForSeconds(float seconds)
    {
        float elapsedTime = 0f;
        while (elapsedTime < seconds)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the game object after the movement is completed
        Destroy(gameObject);
    }
}
