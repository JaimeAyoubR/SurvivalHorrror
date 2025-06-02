using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
   
    public PlayerMovement playerMovement;
    public EnemyMovement enemyMovement;
    public AudioManager audioManager;

    public bool isPlayerSound;
    public bool isEnemySound ;
    public bool isMoviiiiig;

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
        CheckPlayerMoving();
        CheckEnemyMove();
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
         //   audioManager.StopSFX(audioManager.PlayerStepsource);
            StopCoroutine(PlayerWalkSound());
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
            //audioManager.StopSFX(audioManager.EnemyStepsource);
            StopCoroutine(EnemyWalkSound());
            isEnemySound = false;
        }
    }

    private IEnumerator PlayerWalkSound()
    {
        isPlayerSound = true;
        yield return new WaitForSeconds(0.4f);
        AudioManager.instance.PlaySFXRandom(SoundType.PASOS, 0.40f, 0.55f);
        isPlayerSound = false;
    }

    IEnumerator EnemyWalkSound()
    {
        isEnemySound = true;
        yield return new WaitForSeconds(0.4f);
        AudioManager.instance.PlaySFXRandom(SoundType.PASOSENEMY, 0.40f, 0.55f);
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