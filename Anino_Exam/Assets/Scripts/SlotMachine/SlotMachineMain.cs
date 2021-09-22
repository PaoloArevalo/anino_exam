using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineMain : MonoBehaviour
{
    [Header("Status")]
    private bool isRolling = false;
    private bool isStopping = false;
    public Vector2 rollSpeed;
    [Header("SlotMachineData")]
    public List<PayoutLines> payoutLines;
    public SymbolPayout symbolPayout;
    public SymbolDetector topDetector;
    public SymbolDetector middleDetector;
    public SymbolDetector bottomDetector;
    public List<PayoutLines> compiledWinningLines;
    public int compWinLinIndex =0;
    [Header("PlayersBet")]
    public Vector2 minMaxBet;
    public int betIncrease;
    private int playersBet;
    private float totalPrize;
    [Header("References")]
    public Animator knob;
    public SlotMachineReel firstReel;
    public Player player;
    public LineRenderer lineRenderer;

    private void Start()
    {
        SlotMachineEvents.instance.slotMachineRollingCompleted += EvaluatePayoutLines;
        SlotMachineEvents.instance.evaluationComplete += EvaluateBet;
        ModifyBet(0);
        UIScripts.instance.ChangeTotalWinText(0);
    }
    #region Betting
    public void ModifyBet(int i)
    {
        if(player.currentMoney>=playersBet+i*betIncrease)
        playersBet += i * betIncrease;
        playersBet = (int)Mathf.Clamp(playersBet, minMaxBet.x, minMaxBet.y);
        UIScripts.instance.ChangeBetText((int)playersBet);
    }
    #endregion
    #region SlotMachineStatus
    public void SpinButton()
    {
        if(!isRolling)
        {
            StartRolling();
        }
        else if(!isStopping)
        {
            StartStoppingReels();
            StopCoroutine("AutomaticDelayedStop");
        }
    }

    public void StartStoppingReels()
    {
        if(!isStopping)
        {
            knob.SetTrigger("Roll");
            isStopping = true;
            firstReel.StopReel();
        }
    }

    public void StartRolling()
    {
        if (player.HasEnoughMoney(playersBet))
        {
            ResetLineRenderer();
            StartCoroutine("AutomaticDelayedStop");
            knob.SetTrigger("Roll");
            ClearPayoutLinesTally();
            totalPrize = 0;
            UIScripts.instance.ChangeTotalWinText(0);
            isRolling = true;
            SlotMachineEvents.instance.StartedToRoll();
        }
    }

    private void EndStatus()
    {
        isStopping = false;
        isRolling = false;
        topDetector.GetComponent<BoxCollider2D>().enabled = false;
        topDetector.sym.Clear();
        middleDetector.GetComponent<BoxCollider2D>().enabled = false;
        middleDetector.sym.Clear();
        bottomDetector.GetComponent<BoxCollider2D>().enabled = false;
        bottomDetector.sym.Clear();
        UIScripts.instance.ChangeTotalWinText((int)totalPrize);
        player.GetPrizeMoney((int)totalPrize);
    }

    IEnumerator AutomaticDelayedStop()
    {
        yield return new WaitForSeconds(3f);
        StartStoppingReels();
    }

    #endregion
    #region EvaluateBetAndFinish
    private void EvaluateBet()
    {
        for(int b = 0; b<payoutLines.Count;b++)
        {
            if(payoutLines[b].tally[0].isWin&&!payoutLines[b].tally[0].streakBroken)
            {
                compiledWinningLines.Add(payoutLines[b]);
                totalPrize += CheckSymbolPayout(payoutLines[b].tally[0]);
            }
        }
        EndStatus();
        StartCoroutine("CycleWinningLines");
    }

    private int CheckSymbolPayout(Tally symTally)
    {
        for(int s = 0;s<symbolPayout.symbolPayouts.Count;s++)
        {
            if(symbolPayout.symbolPayouts[s].symbol == symTally.symbol)
            {
                float bet = playersBet / payoutLines.Count;
                float prize;
                switch(symTally.count)
                {
                    case 3:
                        prize = bet * symbolPayout.symbolPayouts[s].threeOfaKind;
                        return (int)prize;
                    case 4:
                        prize = bet * symbolPayout.symbolPayouts[s].fourOfaKind;
                        return (int)prize;
                    case 5:
                        prize = bet * symbolPayout.symbolPayouts[s].FiveOfaKind;
                        return (int)prize;
                }
            }
        }
        return 0;
    }

    private void ResetLineRenderer()
    {
        StopCoroutine("CycleWinningLines");
        lineRenderer.gameObject.SetActive(false);
        compWinLinIndex = 0;
        compiledWinningLines.Clear();
    }

    IEnumerator CycleWinningLines()
    {
        if(compiledWinningLines.Count<=0)
        {
            StopCoroutine("CycleWinningLines");
        }
        else
        {
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPositions(compiledWinningLines[compWinLinIndex].points);
        }
        yield return new WaitForSeconds(1f);
        if(compiledWinningLines.Count != 1)
        {
            if (compWinLinIndex >= compiledWinningLines.Count - 1)
            {
                compWinLinIndex = 0;
            }
            else
            {
                compWinLinIndex++;
            }
            StartCoroutine("CycleWinningLines");
        }
    }
    #endregion
    #region EvaluateLines
    private void EvaluatePayoutLines()
    {
        StartCoroutine("DelayedEvaluate");
    }
    IEnumerator DelayedEvaluate()
    {
        yield return new WaitForSeconds(.3f);
        CheckPayoutLines();
    }
    
    public void CheckPayoutLines()
    {
        for(int p = 0; p< payoutLines.Count; p++)
        {
            payoutLines[p].CheckPayoutLine(topDetector,middleDetector,bottomDetector);
        }
        SlotMachineEvents.instance.CompletedEvaluation();
    }
    #endregion
    public void ClearPayoutLinesTally()
    {
        for (int p = 0; p < payoutLines.Count; p++)
        {
            payoutLines[p].tally.Clear();
        }
    }

    private void OnApplicationQuit()
    {
        ClearPayoutLinesTally();
    }

}


