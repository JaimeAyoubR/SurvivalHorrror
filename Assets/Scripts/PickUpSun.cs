using System;
using TMPro;
using UnityEngine;

public class PickUpSun : MonoBehaviour
{
    public static Action sunUIEvent;
    public TextMeshProUGUI PickText;

    public bool canPickUp = false;

    void Start()
    {
        if (PickText == null)
        {
            //PickText = GameObject.FindGameObjectWithTag("PickUpText")
        }

    }

    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.PlaySFX(SoundType.RECOLECTAR_ITEM);
                PickText.enabled = false;
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