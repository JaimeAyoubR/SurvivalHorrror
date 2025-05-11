using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public AudioManager audioManager;
    public PlayerMovement playerMovement;

    public bool isSound;

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
        CheckPLayerMoving();
    }


    void CheckPLayerMoving()
    {
        if (playerMovement.IsMove())
        {
            if (isSound == false)
            {
                StartCoroutine(PlayWalkSound());
            }
        }
        else 
        {
            audioManager.StopSFX(audioManager.PlayerStepsource);
            StopAllCoroutines();
            isSound = false;
        }
    }

    IEnumerator PlayWalkSound()
    {
        isSound = true;
        yield return new WaitForSeconds(0.4f);
        audioManager.PlaySFXRandom(audioManager.PlayerStepsource,audioManager.footStep, 0.40f, 0.55f);
        isSound = false;
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
    }
}