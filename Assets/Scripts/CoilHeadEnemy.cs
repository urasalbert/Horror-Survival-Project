using System;
using UnityEngine;
using UnityEngine.AI;
using static PlayerMovement;

public class CoilHeadEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Chase, Attack
    }
     private EnemyState state;
     private Transform targetTransform;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] Renderer renderer;
    [SerializeField] Transform player;

    private Animator animator;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetTransform = player.transform;
        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:

                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Attack:

                break;
        }
    }

    private void HandleChase()
    {
        if (targetTransform != null)
        {
            if (!IsInLineOfSight() || IsBehindWalls())
            {
                navMeshAgent.SetDestination(targetTransform.position);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
            }

        }
        else
        {
            Debug.LogError("No Player Reference");
        }
    }

    private bool IsBehindWalls()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = (targetTransform.position - transform.position).normalized;
        RaycastHit hit;
        float distance = Vector3.Distance(targetTransform.position, transform.position);

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider != null)
            {
                    return false;
            }
        }
        return true;
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
}
