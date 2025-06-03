using System;
using TMPro;
using UnityEngine;


public class UI_Manager1 : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI sunText;
    public int numOfBatteries;
    public int numOfSunes;

    
    private void OnEnable()
    {
        GameManager.updateUI += UpdateSuns;
        BatteryConsume.batteryUIEvent += UpdateBatterys;
    }

    private void OnDisable()
    {
        GameManager.updateUI -= UpdateSuns;
        BatteryConsume.batteryUIEvent -= UpdateBatterys;
    }


    void UpdateSuns(int suns)
    {
        batteryText.text = suns.ToString();
    }

    void UpdateBatterys(int battery)
    {
        batteryText.text = battery.ToString();
    }
    public void WinGame()
    {
       //WIN WIN 
    }

}
