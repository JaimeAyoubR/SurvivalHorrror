using System;
using TMPro;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    public bool CanPickUp = false;
    public static Action batteryUIEvent;

    void Start()
    {

    }

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
            CanPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanPickUp = false;
        }
    }

    void PickUp()
    {
        batteryUIEvent?.Invoke();
        Destroy(this.gameObject);
    }
}