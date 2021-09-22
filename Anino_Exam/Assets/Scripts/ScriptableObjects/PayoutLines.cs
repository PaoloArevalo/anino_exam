using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PayoutLines")]
public class PayoutLines : ScriptableObject
{
    public List<Line> lines;
    public List<Tally> tally;
    public Vector3[] points = {new Vector3(0,0,0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    
    //Checks the payout line from the list of lines
    public void CheckPayoutLine(SymbolDetector top, SymbolDetector middle, SymbolDetector bottom)
    {
        for(int l = 0;l<lines.Count;l++)
        {
            switch (lines[l].row)
            {
                case SymbolRow.top:
                    TallyUp(top.sym[lines[l].rowPos-1].symbol);
                    Vector2 pointTop = new Vector3(top.sym[lines[l].rowPos - 1].gameObject.transform.position.x, top.sym[lines[l].rowPos - 1].gameObject.transform.position.y,5);
                    points[l] = pointTop;
                    break;
                case SymbolRow.middle:
                    TallyUp(middle.sym[lines[l].rowPos-1].symbol);
                    Vector2 pointMiddle = new Vector3(middle.sym[lines[l].rowPos - 1].gameObject.transform.position.x, middle.sym[lines[l].rowPos - 1].gameObject.transform.position.y,5);
                    points[l] = pointMiddle;
                    break;
                case SymbolRow.bottom:
                    TallyUp(bottom.sym[lines[l].rowPos-1].symbol);
                    Vector2 pointBottom = new Vector3(bottom.sym[lines[l].rowPos - 1].gameObject.transform.position.x, bottom.sym[lines[l].rowPos - 1].gameObject.transform.position.y,5);
                    points[l] = pointBottom;
                    break;
            }
        }
    }

    //Tallies the symbols
    private void TallyUp(Symbols sym)
    {
        for (int t = 0; t < tally.Count; t++)
        {
            if (tally[t].symbol == sym)
            {
                if(sym == tally[0].symbol&&!tally[0].streakBroken)
                {
                    tally[t].count++;
                    if (tally[t].count >= 3&&!tally[t].streakBroken)
                    {
                        tally[t].isWin = true;
                    }
                    return;
                }
                else
                {
                    tally[t].count++;
                    return;
                }
            }
        }
        Tally tal = new Tally();
        tal.symbol = sym;
        tal.count++;
        tally.Add(tal);
        if(tally.Count>1&&tally.Count<=2)
        {
            if(tal.symbol != tally[0].symbol&&!tally[0].isWin)
            {
                tally[0].streakBroken = true;
            }
        }
    }
}

[System.Serializable]
public class Line
{
    public SymbolRow row;
    [Range(1,5)]public int rowPos;
}

[System.Serializable]
public class Tally
{
    public Symbols symbol;
    public int count;
    public bool isWin = false;
    public bool streakBroken = false;
}

