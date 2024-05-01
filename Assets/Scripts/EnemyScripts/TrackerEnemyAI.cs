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
    public Collider damageCollider;
    public Collider sightCollider;
    [SerializeField] private EnemyState state;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] Renderer renderer;
    [SerializeField] Transform player;
    [SerializeField] private float catchDistance;
    [SerializeField] private float chaseTimeMax, chaseTimeMin, chaseTime;

    private Animator animator;
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
    }

    private void HandleChase()
    {
        if (targetTransform != null)
        {
            if (!IsInLineOfSight())
            {
                navMeshAgent.SetDestination(targetTransform.position);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
            }

        }
    }

    /*private bool IsBehindWalls()
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
    }*/

    private bool IsInLineOfSight()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
        {
            return true;
        }
        return false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = EnemyState.Chase;

        }
    }
    public void AttackPlayer()
    {
        PlayerState.Instance.currentHealth -= 80;
    }

    /*IEnumerator dropTracking()
    {
    chaseTime = UnityEngine.Random.Range(chaseTimeMin, chaseTimeMax);
    yield return new WaitForSeconds(chaseTime);
    state = EnemyState.Idle;

    }*/
}
