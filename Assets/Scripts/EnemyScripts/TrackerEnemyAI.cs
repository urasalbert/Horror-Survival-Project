using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CoilHeadEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Chase, Attack
    }

    [SerializeField] private EnemyState state = EnemyState.Idle;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Transform player;
    [SerializeField] private float catchDistance;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private float raycastDistance = 100f;
    private bool isMoving;

    private void Awake()
    {
        targetTransform = player;
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
        }

        float distance = Vector3.Distance(player.position, navMeshAgent.transform.position);
        if (distance <= catchDistance)
        {
            audioSource.Stop();
            if (isMoving)
            {
                state = EnemyState.Attack;
            }

        }

        HandleAudio();
    }

    private void HandleIdle()
    {
        navMeshAgent.speed = 0f;
    }

    private void HandleChase()
    {
        if (targetTransform != null)
        {
            if (!IsInLineOfSight())
            {
                isMoving = true;
                navMeshAgent.speed = 15f;
                navMeshAgent.SetDestination(targetTransform.position);
            }
            else
            {
                isMoving = false;
                navMeshAgent.speed = 0f;
                navMeshAgent.SetDestination(transform.position);
            }
        }
    }

    private bool IsInLineOfSight()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
        {
            return true;
        }
        return false;
    }

    public void AttackPlayer()
    {
        PlayerState.Instance.TakeDamage(100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isMoving == true)
            {
                AttackPlayer();
            }
            else
            {

            }

        }
    }
    private void HandleAudio()
    {
        if (navMeshAgent.speed != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void OnPlayerSighted()
    {
        if (state == EnemyState.Idle)
        {
            state = EnemyState.Chase;
            TrackerSightSound.Instance.PlaySightSound();
        }
    }
}
