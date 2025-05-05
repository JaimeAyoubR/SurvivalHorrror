using TMPro;
using UnityEngine;

public class BatteryConsume : MonoBehaviour
{
    public int numOfBatteries;
    public bool haveBatteries;
    public float LifeBattery;
    public TextMeshProUGUI batteryText;

    void Start()
    {
        numOfBatteries = 5;
        haveBatteries = true;
        UpdateText();
    }

    // Update is called once per frame
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
        UpdateText();
    }

    void RemoveBatteries()
    {
        if (numOfBatteries > 0)
        {
            numOfBatteries--;
            UpdateText();
        }
    }

    void UpdateText()
    {
        batteryText.text = numOfBatteries.ToString();
    }
}