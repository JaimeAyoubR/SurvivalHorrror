using System;
using TMPro;
using UnityEngine;

public class BatteryConsume : MonoBehaviour
{
    public int numOfBatteries;
    public bool haveBatteries;
    public float LifeBattery;

    private void OnEnable()
    {
        BatteryPickUp.batteryUIEvent += AddBatteries;
    }

    private void OnDisable()
    {
        BatteryPickUp.batteryUIEvent -= AddBatteries;
    }

    void Start()
    {
        numOfBatteries = 5;
        haveBatteries = true;
    }

    void Update()
    {
        if (haveBatteries)
        {
            BatteriesConsumed();
        }
    }

    void BatteriesConsumed()
    {
        if ((Time.time - LifeBattery) >= 10)
        {
            RemoveBatteries();
            LifeBattery = Time.time;
        }

        if (numOfBatteries > 0)
        {
            haveBatteries = true;
        }
        else if (numOfBatteries <= 0)
        {
            haveBatteries = false;
        }
    }

   public void AddBatteries()
    {
        numOfBatteries++;
    }

    void RemoveBatteries()
    {
        if (numOfBatteries > 0)
        {
            numOfBatteries--;
        }
    }
}