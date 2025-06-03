using System;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using System.Collections;

/*
 * TODO Poner trigger en el enemy
 */

public class Enemy_Scriptv2 : MonoBehaviour
{
    public static Action<Transform> attackEvent;

    [Header("Estados")]
    public EnemyState currentState = EnemyState.Patrol;

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

    [Header("Vars de ambush")]
    public Transform[] ambushPoints;
    public float ambushWaitTime = 3f;
    private bool isAmbushing = false;

    [Header("Vars de ataque")]
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    // Componentes
    private NavMeshAgent agent;
    private Animator animator;

    // Variables de estado
    private bool isFollowing = false;
    private bool isMoving = false;
    private float stateTimer = 0f;
    
    private PlayableDirector director;

    // Enum para los estados
    public enum EnemyState
    {
        Patrol,
        Waiting,
        Chase,
        Ambush,
        Attack,
        ReturnToPatrol
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        director = GetComponent<PlayableDirector>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.autoBraking = true;
        agent.stoppingDistance = 0.5f;
        agent.speed = patrolSpeed;

        ChangeState(EnemyState.Patrol);
    }

    void Update()
    {
        stateTimer += Time.deltaTime;

        // Actualizar animator
        animator.SetBool("isWaiting", isWaiting);
      //  animator.SetBool("isMoving", agent.velocity.magnitude > 0.1f);

        // Ejecutar logica del estado actual
        switch (currentState)
        {
            case EnemyState.Patrol:
                HandlePatrolState();
                break;
            case EnemyState.Waiting:
                HandleWaitingState();
                break;
            case EnemyState.Chase:
                HandleChaseState();
                break;
            case EnemyState.Ambush:
                HandleAmbushState();
                break;
            case EnemyState.Attack:
                HandleAttackState();
                break;
            case EnemyState.ReturnToPatrol:
                HandleReturnToPatrolState();
                break;
        }

        // Verificar deteccion del jugador (solo si no esta siguiendo)
        if (!isFollowing && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius && currentState != EnemyState.Chase && currentState != EnemyState.Attack)
            {
                ChangeState(EnemyState.Chase);
            }
        }
    }

    #region State Handlers

    void HandlePatrolState()
    {
        if (waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            ChangeState(EnemyState.Waiting);
        }
    }

    void HandleWaitingState()
    {
        if (stateTimer >= waypointWaitTime)
        {
            // Posibilidad de emboscada
            if (ambushPoints.Length > 0 && UnityEngine.Random.value > 0.7f)
            {
                ChangeState(EnemyState.Ambush);
            }
            else
            {
                MoveToNextWaypoint();
                ChangeState(EnemyState.Patrol);
            }
        }
    }

    void HandleChaseState()
    {
        if (player == null)
        {
            ChangeState(EnemyState.ReturnToPatrol);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si esta en rango de ataque
        if (distanceToPlayer <= attackRange)
        {
           ChangeState(EnemyState.Attack);
           
            return;
        }

        // Si el jugador se aleja mucho
        if (distanceToPlayer > followRange)
        {
            ChangeState(EnemyState.ReturnToPatrol);
            return;
        }

        // Seguir al jugador
        agent.SetDestination(player.position);
    }

    void HandleAmbushState()
    {
        if (stateTimer >= ambushWaitTime)
        {
            ChangeState(EnemyState.Patrol);
            MoveToNextWaypoint();
        }
    }

    void HandleAttackState()
    {
        if (player == null)
        {
            ChangeState(EnemyState.ReturnToPatrol);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Parar movimiento y mirar al jugador
        agent.SetDestination(transform.position);
        Vector3 lookDirection = (player.position - transform.position).normalized;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        // Atacar si ha pasado el cooldown
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            StartCoroutine(DelayedAttackCoroutine());
            lastAttackTime = Time.time;
        }

        // Si el jugador se aleja, volver a perseguir
        if (distanceToPlayer > attackRange * 1.2f)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    void HandleReturnToPatrolState()
    {
        // Ir al waypoint mas cercano
        Transform closestWaypoint = GetClosestWaypoint();
        if (closestWaypoint != null)
        {
            agent.SetDestination(closestWaypoint.position);

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                ChangeState(EnemyState.Patrol);
            }
        }
        else
        {
            ChangeState(EnemyState.Patrol);
        }
    }

    #endregion

    #region State Management

    void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        // Salir del estado anterior
        OnExitState(currentState);

        // Cambiar al nuevo estado
        EnemyState previousState = currentState;
        currentState = newState;
        stateTimer = 0f;

        // Entrar al nuevo estado
        OnEnterState(newState);

        Debug.Log($"Enemy: {previousState} -> {newState}");
    }

    void OnEnterState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Patrol:
                agent.speed = patrolSpeed;
                isFollowing = false;
                isWaiting = false;
                break;

            case EnemyState.Waiting:
                isWaiting = true;
                break;

            case EnemyState.Chase:
                agent.speed = followSpeed;
                isFollowing = true;
                isWaiting = false;
                break;

            case EnemyState.Ambush:
                isAmbushing = true;
                int ambushIndex = UnityEngine.Random.Range(0, ambushPoints.Length);
                agent.SetDestination(ambushPoints[ambushIndex].position);
                break;

            case EnemyState.Attack:
                agent.speed = 0f;
                break;

            case EnemyState.ReturnToPatrol:
                agent.speed = patrolSpeed;
                isFollowing = false;
                break;
        }
    }

    void OnExitState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Waiting:
                isWaiting = false;
                break;

            case EnemyState.Ambush:
                isAmbushing = false;
                break;
        }
    }

    #endregion

    #region Metodos de utilities

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    Transform GetClosestWaypoint()
    {
        if (waypoints.Length == 0) return null;

        Transform closest = waypoints[0];
        float closestDistance = Vector3.Distance(transform.position, closest.position);

        for (int i = 1; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, waypoints[i].position);
            if (distance < closestDistance)
            {
                closest = waypoints[i];
                closestDistance = distance;
                currentWaypointIndex = i;
            }
        }

        return closest;
    }

    void PerformAttack()
    {
        agent.velocity = Vector3.zero;
        director.Play();
        attackEvent?.Invoke(transform);
    }


    private IEnumerator DelayedAttackCoroutine()
    {
        yield return new WaitForSeconds(2f);

        PerformAttack();

    }

    #endregion

    #region Trigger Events

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentState == EnemyState.Patrol || currentState == EnemyState.Waiting)
            {
                ChangeState(EnemyState.Chase);
            }
        }
    }

    #endregion


    #region Debug

    void OnDrawGizmosSelected()
    {
        // Radio de detecciOn
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Rango de chase
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);

        // Rango de ataque
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #endregion
}