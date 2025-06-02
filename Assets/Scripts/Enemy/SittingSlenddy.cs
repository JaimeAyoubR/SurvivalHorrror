using UnityEngine;

public class SittingSlenddy : MonoBehaviour
{
    [Header("Puntos de tp")]
    [SerializeField] private Transform[] waypoints;

    [Header("Configuracion")]
    [Tooltip("Referencia al jugador")]
    public Transform player;

    public float rotationSpeed = 5f;

    [Tooltip("Solo rotar cuando el jugador este cerca")]
    public bool useRange = false;

    [Tooltip("Distancia maxima para empezar a rotar")]
    public float maxDistance = 10f;

    [Tooltip("Solo rotar cuando el jugador este en el campo de vision")]
    public bool useFieldOfView = false;

    [Tooltip("angulo del campo de vision")]
    public float fieldOfViewAngle = 90f;

    [Header("Debug")]
    public bool showDebugLines = true;

    private void OnEnable()
    {
        PlayerRayCast.seeEvent += TpSlendie;
    }

    private void OnDisable()
    {
        PlayerRayCast.seeEvent -= TpSlendie;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (!ShouldRotate()) return;

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;

        if (directionToPlayer.magnitude < 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        if (rotationSpeed <= 0)
        {
            transform.rotation = targetRotation;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    bool ShouldRotate()
    {
        if (useRange)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > maxDistance)
                return false;
        }

        if (useFieldOfView)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0;

            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle > fieldOfViewAngle / 2f)
                return false;
        }

        return true;
    }

    void TpSlendie()
    {
        int randPoint = Random.Range(0, waypoints.Length);
        transform.position = waypoints[randPoint].position;
    }

}
