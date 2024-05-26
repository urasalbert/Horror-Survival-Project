using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredRun : MonoBehaviour
{
    public float speed = 5f; // �leri hareket h�z�
    public float duration = 4f; // Hareket s�resi

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rb != null && animator != null)
        {
            StartCoroutine(MoveForwardForSeconds(duration));
        }
        else
        {
            Debug.LogError("Rigidbody or Animator component is missing.");
        }
    }

    private IEnumerator MoveForwardForSeconds(float seconds)
    {
        // Animasyonu ba�lat
        animator.SetBool("isWalking", true);

        float elapsedTime = 0f;
        while (elapsedTime < seconds)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Animasyonu durdur
        animator.SetBool("isWalking", false);
    }
}