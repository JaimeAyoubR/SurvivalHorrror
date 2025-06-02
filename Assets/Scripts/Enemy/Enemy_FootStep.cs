using UnityEngine;
using UnityEngine.AI;

public class EnemyFootsteps : MonoBehaviour
{
    [Header("Audio Vars")]
    public AudioClip[] footstepSounds; // Array de sonidos de pasos
    public AudioSource audioSource;

    [Header("Distance Vars")]
    public float maxHearingDistance = 15f; // Distancia maxima para escuchar pasos
    public float minHearingDistance = 2f;  // Distancia manima (volumen maximo)

    [Header("Footstep Vars")]
    public float stepInterval = 0.5f; // Intervalo entre pasos cuando camina
    public float runningStepInterval = 0.3f; // Intervalo cuando corre

    [Header("Volume Vars")]
    public float maxVolume = 1f;
    public float minVolume = 0.1f;

    // Referencias privadas
    private Transform player;
    private float stepTimer;
    private bool isMoving;
    private bool isRunning;
    private float currentSpeed;

    // Para detectar movimiento
    private Vector3 lastPosition;
    private Rigidbody enemyRigidbody;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Buscar el jugador por tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Obtener componentes
        enemyRigidbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Si no hay AudioSource, crear uno
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configurar AudioSource
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.maxDistance = maxHearingDistance;
        audioSource.minDistance = minHearingDistance;

        lastPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        // Detectar si el enemigo se esta moviendo
        DetectMovement();

        // Si se esta moviendo, manejar los pasos
        if (isMoving)
        {
            HandleFootsteps();
        }
    }

    void DetectMovement()
    {
        // Calcular velocidad actual
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            currentSpeed = navMeshAgent.velocity.magnitude;
        }
        else if (enemyRigidbody != null)
        {
            currentSpeed = enemyRigidbody.linearVelocity.magnitude;
        }
        else
        {
            currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        }

        // Determinar si se esta moviendo y si esta corriendo
        isMoving = currentSpeed > 0.1f;
        isRunning = currentSpeed > 3f; // Ajusta este valor segun tu juego

        lastPosition = transform.position;
    }

    void HandleFootsteps()
    {
        if (player == null || footstepSounds.Length == 0) return;

        // Calcular distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador esta fuera del rango de audician, no reproducir sonidos
        if (distanceToPlayer > maxHearingDistance) return;

        // Actualizar timer
        float currentStepInterval = isRunning ? runningStepInterval : stepInterval;
        stepTimer += Time.deltaTime;

        // Si es tiempo de dar un paso
        if (stepTimer >= currentStepInterval)
        {
            PlayFootstepSound(distanceToPlayer);
            stepTimer = 0f;
        }
    }

    void PlayFootstepSound(float distanceToPlayer)
    {
        // Seleccionar sonido aleatorio
        AudioClip stepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Calcular volumen basado en distancia
        float volumeMultiplier = CalculateVolumeByDistance(distanceToPlayer);

        // Configurar y reproducir sonido
        audioSource.clip = stepSound;
        audioSource.volume = volumeMultiplier;
        audioSource.pitch = Random.Range(0.9f, 1.1f); // Variacion ligera en pitch
        audioSource.Play();
    }

    float CalculateVolumeByDistance(float distance)
    {
        // Normalizar la distancia entre 0 y 1
        float normalizedDistance = Mathf.Clamp01((distance - minHearingDistance) / (maxHearingDistance - minHearingDistance));

        // Invertir para que cerca = volumen alto
        float volumeMultiplier = Mathf.Lerp(maxVolume, minVolume, normalizedDistance);

        return volumeMultiplier;
    }

    public void ForceFootstep()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= maxHearingDistance)
        {
            PlayFootstepSound(distanceToPlayer);
        }
    }

    //  Pausar/reanudar pasos
    public void SetFootstepsEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (!enabled && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Cambiar la velocidad de pasos
    public void SetStepIntervals(float walkInterval, float runInterval)
    {
        stepInterval = walkInterval;
        runningStepInterval = runInterval;
    }


    void OnDrawGizmosSelected()
    {
        // Dibujar rango de audicion
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxHearingDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minHearingDistance);
    }
}