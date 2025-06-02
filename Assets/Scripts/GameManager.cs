using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public AudioManager audioManager;
    public PlayerMovement playerMovement;
    public EnemyMovement enemyMovement;

    public bool isPlayerSound;
    public bool isEnemySound;
<<<<<<< Updated upstream
=======
    public bool isMoviiiiig;

    public int numbersOfSuns;
>>>>>>> Stashed changes

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CheckComponents();
    }


    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        CheckPlayerMoving();
        CheckEnemyMove();
=======
       CheckPlayerMoving();
       CheckEnemyMove();
>>>>>>> Stashed changes
    }


    void CheckPlayerMoving()
    {
        if (playerMovement.IsMove())
        {
            if (isPlayerSound == false)
            {
                StartCoroutine(PlayerWalkSound());
            }
        }
        else 
        {
            audioManager.StopSFX(audioManager.PlayerStepsource);
            StopAllCoroutines();
            isPlayerSound = false;
        }
    }

    void CheckEnemyMove()
    {
        if (enemyMovement.returnMove())
        {
            if (isEnemySound == false)
            {
                StartCoroutine(EnemyWalkSound());
            }
        }
        else 
        {
            audioManager.StopSFX(audioManager.EnemyStepsource);
            StopAllCoroutines();
            isEnemySound = false;
        }
    }

    private IEnumerator PlayerWalkSound()
    {
        isPlayerSound = true;
        yield return new WaitForSeconds(0.4f);
        audioManager.PlaySFXRandom(audioManager.PlayerStepsource,audioManager.footStep, 0.40f, 0.55f);
        isPlayerSound = false;
    }

    IEnumerator EnemyWalkSound()
    {
        isEnemySound = true;
        yield return new WaitForSeconds(0.4f);
<<<<<<< Updated upstream
        audioManager.PlaySFXRandom(audioManager.EnemyStepsource,audioManager.enemyFootStep, 0.40f, 0.55f);
=======
        AudioManager.instance.PlaySFXRandom(SoundType.PASOS_ENEMY, 0.40f, 0.55f);
>>>>>>> Stashed changes
        isEnemySound = false;
    }

    void CheckComponents()
    {
        if (audioManager == null)
        {
            audioManager = FindAnyObjectByType<AudioManager>();
        }
        else
        {
            Debug.LogWarning("No se encontro el AudioManager");
        }

        if (playerMovement == null)
        {
            playerMovement = FindAnyObjectByType<PlayerMovement>();
        }
        else
        {
            Debug.LogWarning("No se encontro el PlayerMovement");
        }

        if (enemyMovement == null)
        {
            enemyMovement = FindAnyObjectByType<EnemyMovement>();
        }
        else
        {
            Debug.LogWarning("No se encontro el EnemyMovement");
        }
    }
}