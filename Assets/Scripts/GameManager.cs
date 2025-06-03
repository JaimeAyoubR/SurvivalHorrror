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


    public static Action<int, int> updateUI;
    public static Action winGame;

    private int numOfSunes;
    private int numOfBatterys;

    public bool isPlayerSound;
    public bool isEnemySound;
    public bool isMoviiiiig;

    private void OnEnable()
    {
        PickUpSun.sunUIEvent += AddSun;
        BatteryPickUp.batteryUIEvent += AddBattery;
    }

    private void OnDisable()
    {
        PickUpSun.sunUIEvent -= AddSun;
        BatteryPickUp.batteryUIEvent -= AddBattery;
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

    void Update()
    {
        CheckPlayerMoving();
        CheckEnemyMove();
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
            AudioManager.StopSFX();
            //audioManager.StopSFX(audioManager.EnemyStepsource);
            StopAllCoroutines();
            isEnemySound = false;
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

    IEnumerator EnemyWalkSound()
    {
        isEnemySound = true;
        yield return new WaitForSeconds(0.4f);
        AudioManager.PlaySFXRandom(SoundType.PASOS_ENEMY, 0.40f, 0.55f);
        //audioManager.PlaySFXRandom(audioManager.EnemyStepsource,audioManager.enemyFootStep, 0.40f, 0.55f);

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

    void AddSun()
    {
        numOfSunes++;
        UpdateUI();
    }

    void AddBattery()
    {
        numOfBatterys++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        updateUI?.Invoke(numOfBatterys, numOfSunes);
    }
}