using UnityEngine;
using UnityEngine.AI;

public class ClownBehaviour : MonoBehaviour
{
    public static ClownBehaviour Instance { get; private set; }

    public Transform playerTransform;
    public AudioSource idleAudioSource;
    public AudioSource footstepAudioSource;
    public AudioSource attackAudioSource;
    public float attackRange = 4f;
    public Animator animator;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private bool isChasing = false;
    [SerializeField] public bool isAttacking = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator.SetBool("isIdle", true);

        if (idleAudioSource != null)
        {
            idleAudioSource.loop = true;
            idleAudioSource.Play();
        }
    }

    private void Update()
    {
        if (PlayerState.Instance.currentHealth <= 0)
        {
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
            return;
        }

        if (isChasing && playerTransform != null)
        {
            navMeshAgent.speed = 5;

            navMeshAgent.SetDestination(playerTransform.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
            animator.SetBool("isIdle", false);

            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
                idleAudioSource.Stop();
            }

            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
            {
                StartAttack();
            }
        }
        else
        {
            if (!animator.GetBool("isIdle"))
            {
                animator.SetBool("isIdle", true);

                if (!idleAudioSource.isPlaying)
                {
                    idleAudioSource.Play();
                    footstepAudioSource.Stop();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && FlashlightHandler.Instance.isFlashlightOn)
        {
            isChasing = true;
        }
    }

    private void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            Debug.Log("Attacking");

            if (attackAudioSource != null && !attackAudioSource.isPlaying)
            {
                attackAudioSource.Play();
            }
            PlayerState.Instance.currentHealth = 0;
        }
    }
}
