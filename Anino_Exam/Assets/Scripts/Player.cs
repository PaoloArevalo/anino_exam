using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentMoney;

    private void Start()
    {
        UIScripts.instance.ChangeCurrentGoldText(currentMoney);
    }

    public bool HasEnoughMoney(int bill)
    {
        if(bill> currentMoney)
        {
            return false;
        }
        else
        {
            currentMoney -= bill;
            UIScripts.instance.ChangeCurrentGoldText(currentMoney);
            return true;
        }
    }

    public void GetPrizeMoney(int wonPrize)
    {
        currentMoney += wonPrize;
        UIScripts.instance.ChangeCurrentGoldText(currentMoney);
    }    

}
