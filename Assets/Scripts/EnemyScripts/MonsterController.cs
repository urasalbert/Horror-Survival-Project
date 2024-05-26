using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10.0f;
    public float waitTimeBeforeAttack = 10.0f;
    public AudioClip idleSound;
    public AudioClip runSound;
    public AudioClip attackSound;
    public AudioSource audioSource;

    private NavMeshAgent agent;
    private Animator animator;
    private float waitTimer;
    private bool isPlayerDetected = false;
    private bool isPlayerInAttackRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        waitTimer = 0;
        agent.isStopped = true;
        PlaySound(idleSound, true);
    }

    void Update()
    {
        if (PlayerState.Instance.currentHealth == 0)
        {
            StartCoroutine(StopAllSounds());
            return; 
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
            waitTimer = 0; // Reset timer while no player nearby
        }

        if (isPlayerDetected)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTimeBeforeAttack)
            {
                agent.isStopped = false; // Start Move
                agent.SetDestination(player.position);
                animator.SetBool("isRunning", true);

                if (audioSource.clip != runSound || !audioSource.isPlaying)
                {
                    PlaySound(runSound, true);
                }
            }
            else
            {
                agent.isStopped = true; // Stop move
                animator.SetBool("isRunning", false);

                if (audioSource.clip != idleSound || !audioSource.isPlaying)
                {
                    PlaySound(idleSound, true);
                }
            }
        }
        else
        {
            agent.isStopped = true; // Stop move
            animator.SetBool("isRunning", false);

            if (audioSource.clip != idleSound || !audioSource.isPlaying)
            {
                PlaySound(idleSound, true);
            }
        }

        if (isPlayerInAttackRange && waitTimer >= waitTimeBeforeAttack)
        {
            Debug.Log("Monster is attacking");
            AttackPlayer();
            waitTimer = 0.0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInAttackRange = true;
            Debug.Log("Player entered attack range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInAttackRange = false;
            Debug.Log("Player exited attack range");
        }
    }

    void AttackPlayer()
    {
        Debug.Log("AttackPlayer called");
        animator.SetTrigger("Attack");
        PlayOneShot(attackSound);
        PlayerState.Instance.currentHealth = 0;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack"))
        {
            Debug.Log("Attack animation is playing");
        }
        else
        {
            Debug.Log("Attack animation is NOT playing");
        }
    }

    void PlaySound(AudioClip clip, bool loop)
    {
        if (audioSource.clip != clip || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }

    void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

   IEnumerator StopAllSounds()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.Stop();
    }
}
