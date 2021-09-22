using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SymbolPayout")]
public class SymbolPayout : ScriptableObject
{
    public List<SymbolPayoutSRLZB> symbolPayouts;
}

[System.Serializable]
public class SymbolPayoutSRLZB
{
    public Symbols symbol;
    public int threeOfaKind;
    public int fourOfaKind;
    public int FiveOfaKind;
}
