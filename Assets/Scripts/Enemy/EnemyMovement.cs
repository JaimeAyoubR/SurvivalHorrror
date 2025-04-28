using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Vars de patrol")]
    public Transform[] waypoints;
    public float patrolSpeed = 3f;
    public float waypointWaitTime = 5f;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    [Header("Vars de chase")]
    public float followSpeed = 5f;
    public float detectionRadius = 5f;
    public float followRange = 7f;
    private Transform player;

    private NavMeshAgent agent;
    private bool isFollowing = false;

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
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
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
}