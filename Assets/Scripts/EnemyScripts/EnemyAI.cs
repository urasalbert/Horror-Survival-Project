using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animation anim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;
    public GameObject PlayerObject;
    public float attackInterval = 3f;
    private bool isAttacking = false;
    private bool hasAttacked = false;
    [SerializeField] private CanvasGroup HurtCanvas;

    void Start()
    {
        HurtCanvas.alpha = 0;
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (!isAttacking && hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
            }
        }
        if (hasAttacked == true)
        {
            walking = false;
            chasing = false;
            StopCoroutine("stayIdle");
            StartCoroutine("stayIdle");
            anim.Play("Idle");
            ai.speed = 0;

        }
        if (chasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            anim.Play("Run");

            float distance = Vector3.Distance(player.position, ai.transform.position);
            if (distance <= catchDistance)
            {
                player.gameObject.SetActive(false);
                chasing = false;
            }
        }
        if (walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            anim.Play("Walk");

            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                anim.Play("Idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
    public void AttackPlayer()//attack and reset hasattacked for anim and positions
    {
        isAttacking = true;

        if (!hasAttacked)
        {
            hasAttacked = true;
            PlayerState.Instance.TakeDamage(50);//ghoul damage value
            StartCoroutine(HurtFlash());
            StartCoroutine(ResetHasAttacked());
        }
    }

    IEnumerator HurtFlash()
    {
        Fader.Instance.ShowIU();
        yield return new WaitForSeconds(1f);
        Fader.Instance.HideUI();
    }
    IEnumerator ResetHasAttacked()
    {
        yield return new WaitForSeconds(5f);
        hasAttacked = false;
        
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    /*IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }*/
}