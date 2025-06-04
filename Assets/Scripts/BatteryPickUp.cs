using System;
using TMPro;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    public bool CanPickUp = false;
    public static Action batteryUIEvent;
    public TextMeshProUGUI PickText;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanPickUp == true)
        {
            PickText.enabled = false;
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickText.enabled = true;
            PickText.text = "Pick Up: E";
            CanPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PickText.enabled = false;
            CanPickUp = false;
        }
    }

    void PickUp()
    {
        AudioManager.PlaySFX(SoundType.RECOLECTAR_ITEM);
        batteryUIEvent?.Invoke();
        Destroy(this.gameObject);
    }
}