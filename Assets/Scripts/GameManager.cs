using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public AudioManager audioManager;
    public PlayerMovement playerMovement;
    public EnemyMovement enemyMovement;


    public static Action<int> updateSunsUI;
    public static Action winGame;

    private int numOfSunes;

    public bool isPlayerSound;
    public bool isMoviiiiig;

    private void OnEnable()
    {
        PickUpSun.sunUIEvent += AddSun;
    }

    private void OnDisable()
    {
        PickUpSun.sunUIEvent -= AddSun;
    }

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

    private void Start()
    {
        updateSunsUI?.Invoke(numOfSunes);
        numOfSunes = 0;
    }

    void Update()
    {
        CheckPlayerMoving();
        if (numOfSunes >= 3)
        {
            winGame?.Invoke();
        }
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
            AudioManager.StopSFX();
            //audioManager.StopSFX(audioManager.PlayerStepsource);
            StopAllCoroutines();
            isPlayerSound = false;
        }
    }



    private IEnumerator PlayerWalkSound()
    {
        isPlayerSound = true;
        yield return new WaitForSeconds(0.4f);
        AudioManager.PlaySFXRandom(SoundType.PASOS, 0.40f, 0.55f);
        //audioManager.PlaySFXRandom(audioManager.PlayerStepsource, 0.40f, 0.55f);
        isPlayerSound = false;
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

    void AddSun()
    {
        numOfSunes++;
        updateSunsUI?.Invoke(numOfSunes);
    }
}