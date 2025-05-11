using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioManager audioManager;
    public PlayerMovement playerMovement;

    public bool isSound = false;

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
            StopAllCoroutines();
            isSound = false;
        }
    }

    IEnumerator PlayWalkSound()
    {
        isSound = true;
        yield return new WaitForSeconds(0.4f);
        audioManager.PlaySFXRandom(audioManager.footStep, 0.25f, 0.5f);
        isSound = false;
    }

    void CheckComponents()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        else
        {
            Debug.LogWarning("No se encontro el AudioManager");
        }

        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }
        else
        {
            Debug.LogWarning("No se encontro el PlayerMovement");
        }
    }
}