using System.Net;
using UnityEngine;
using UnityEngine.AI;


/*
 * Este enemy es un prefab, para que funcione con el nav mesh necesitan agregar un componente llamado NavMeshSurface al
 * terreno del nivel y darle BAKE, el enemy tiene otro componente llamado NavMeshAgent que deber�a funcionar con eso,
 * si el terreno es muy desigual habr� que modificar los valores en el NavMeshAgent o llendo a Window -> AI -> Navigation
 */

public class EnemyMovement : MonoBehaviour
{
    [Header("Vars de patrol")] public Transform[] waypoints;
    public float patrolSpeed = 3f;
    public float waypointWaitTime = 5f;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    [Header("Vars de chase")] public float followSpeed = 5f;
    public float detectionRadius = 5f;
    public float followRange = 7f;
    private Transform player;

    [Header("Vars de ambush")] public Transform[] ambushPoints;
    public float ambushWaitTime = 3f;
    private bool isAmbushing = false;

    private NavMeshAgent agent;
    private bool isFollowing = false;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.autoBraking = true;
        agent.stoppingDistance = 0.5f;

        agent.speed = patrolSpeed;
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (isWaiting)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (isFollowing)
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) > followRange)
            {
                StopFollowing();
            }
        }
        else
        {
            if (!isFollowing && !isWaiting)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    isWaiting = true;
                    Invoke("MoveToNextWaypoint", waypointWaitTime);
                }
            }
        }
    }


    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        isWaiting = false;

        if (!isFollowing && ambushPoints.Length > 0 && Random.value > 0.7f)
        {
            isAmbushing = true;
            int ambushIndex = Random.Range(0, ambushPoints.Length);
            agent.SetDestination(ambushPoints[ambushIndex].position);
            Invoke("EndAmbush", ambushWaitTime);
            return;
        }

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void EndAmbush()
    {
        isAmbushing = false;
        MoveToNextWaypoint();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartFollowing();
        }
    }

    void StartFollowing()
    {
        isFollowing = true;
        agent.speed = followSpeed;
        CancelInvoke("MoveToNextWaypoint");
    }

    void StopFollowing()
    {
        isFollowing = false;
        agent.speed = patrolSpeed;
        MoveToNextWaypoint();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public bool returnMove()
    {
        return isMoving;
    }
}