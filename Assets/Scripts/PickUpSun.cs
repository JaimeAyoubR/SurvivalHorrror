using System;
using TMPro;
using UnityEngine;

public class PickUpSun : MonoBehaviour
{
    public GameManager gameManager;
    public static Action sunUIEvent;
    public TextMeshProUGUI PickText;

    public bool canPickUp = false;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }
    }

    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickText.enabled = false;
                AudioManager.PlaySFX(SoundType.RECOLECTAR_ITEM);
                sunUIEvent?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PickText.enabled = true;
        PickText.text = "Pick Up: E";
        if (other.tag == "Player")
            canPickUp = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PickText.enabled = false;
        canPickUp = false;
    }
}