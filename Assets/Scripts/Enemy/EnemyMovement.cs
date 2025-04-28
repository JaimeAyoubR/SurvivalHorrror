using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Vars de patrol")]
    public Transform[] waypoints;
    public float patrolSpeed = 3f;
    public float waypointWaitTime = 1f;
    private int currentWaypointIndex = 0;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Configuración inicial
        agent.speed = patrolSpeed;
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (isFollowing)
        {
            agent.SetDestination(player.position);

            // Verificar si el jugador se alejó demasiado
            if (Vector3.Distance(transform.position, player.position) > followRange)
            {
                StopFollowing();
            }
        }
        else
        {
            // Verificar si llegó al waypoint
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Invoke("MoveToNextWaypoint", waypointWaitTime);
            }
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

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

    // Visualizar radio de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}