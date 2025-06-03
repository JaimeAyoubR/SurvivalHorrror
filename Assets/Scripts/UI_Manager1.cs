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
        GameManager.updateUI += CheckWin;
    }

    private void OnDisable()
    {
        GameManager.updateUI -= CheckWin;
    }

    private void CheckWin(int battery, int suns)
    {
        numOfBatteries += battery;
        batteryText.text = numOfBatteries.ToString();

        numOfSunes += suns;
        sunText.text = numOfSunes.ToString();
    }

    public void WinGame()
    {
       //WIN WIN 
    }

}
