using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Symbols
{
    seven,isaw,kwekwek,taho,balut
}

public class Symbol : MonoBehaviour
{
    [HideInInspector]public SlotMachineReel reelSlot;
    public Symbols symbol;
    public int column;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
