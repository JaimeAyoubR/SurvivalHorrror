using System;
using TMPro;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    public TextMeshProUGUI PickUpText;
    public bool CanPickUp = false;
    public BatteryConsume battery;

    void Start()
    {
        PickUpText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanPickUp == true)
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUpText.enabled = true;
            CanPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUpText.enabled = false;
            CanPickUp = false;
        }
    }

    void PickUp()
    {
        battery.AddBatteries();
        PickUpText.enabled = false;
        Destroy(this.gameObject);
    }
}