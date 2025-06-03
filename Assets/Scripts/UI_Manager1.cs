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
        GameManager.updateSunsUI += UpdateSuns;
        BatteryConsume.batteryUIEvent += UpdateBatterys;
    }

    private void OnDisable()
    {
        GameManager.updateSunsUI -= UpdateSuns;
        BatteryConsume.batteryUIEvent -= UpdateBatterys;
    }


    void UpdateSuns(int suns)
    {
        sunText.text = "X" + " " + suns.ToString();
    }

    void UpdateBatterys(int battery)
    {
        batteryText.text = "X" + " " + battery.ToString();
    }

}
