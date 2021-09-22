using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScripts : MonoBehaviour
{
    public static UIScripts instance;

    public SlotMachineMain slotMachine;

    public TextMeshProUGUI currentGold;
    public TextMeshProUGUI betGold;
    public TextMeshProUGUI totalWinGold;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBetText(int bet)
    {
        betGold.text = bet.ToString();
    }
    public void ChangeCurrentGoldText(int curGold)
    {
        currentGold.text = curGold.ToString();
    }
    public void ChangeTotalWinText(int totWin)
    {
        totalWinGold.text = totWin.ToString();
    }
}
