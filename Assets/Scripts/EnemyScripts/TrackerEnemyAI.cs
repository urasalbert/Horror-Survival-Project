using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;
using static PlayerMovement;

public class CoilHeadEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Chase, Attack
    }

    [SerializeField] private EnemyState state;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] Renderer renderer;
    [SerializeField] Transform player;
    [SerializeField] private float catchDistance;

    private void Awake()
    {
        targetTransform = player.transform;

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

        float distance = Vector3.Distance(player.position, navMeshAgent.transform.position);
        if (distance <= catchDistance)
        {
            AttackPlayer();
        }

        if(TrackerRaycast.Instance.isEnemySighted == true)
        {
            state = EnemyState.Chase;
        }
    }

    private void HandleChase()
    {
        if (targetTransform != null)
        {
            if (!IsInLineOfSight())
            {
                navMeshAgent.speed = 15f;
                navMeshAgent.SetDestination(targetTransform.position);
            }
            else
            {
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
}
