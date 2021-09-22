using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SymbolRow
{
    top,middle,bottom
}

public class SymbolDetector : MonoBehaviour
{
    public List<Symbol> sym;

    private void Start()
    {
        SlotMachineEvents.instance.slotMachineRollingCompleted += DetectSymbols;
    }

    private void DetectSymbols()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine("CheckIfCompleted");
    }
    //Sorts the list so it can make the evaluation easier
    public void SortSymbolsByReel()
    {
        for (int s = 0; s < sym.Count - 1; s++)
        {
            for (int x = 0; x < sym.Count - s - 1; x++)
            {
                if(sym[x].reelSlot.reelID > sym[x+1].reelSlot.reelID)
                {
                    Symbol tempsym = sym[x];
                    sym[x] = sym[x + 1];
                    sym[x + 1] = tempsym;
                }
            }
        }
    }

    IEnumerator CheckIfCompleted()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if(sym.Count==5)
        {
            SortSymbolsByReel();
        }
        else
        {
            StartCoroutine("CheckIfCompleted");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reel")
        {
            sym.Add(collision.gameObject.GetComponent<Symbol>());
        }
    }
}
