using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineEvents : MonoBehaviour
{
    public static SlotMachineEvents instance;


    public event Action slotMachineStartedRolling;
    public event Action slotMachineRollingCompleted;
    public event Action evaluationComplete;

    public delegate void ReelStopRolling(int reelID);
    public event ReelStopRolling onReelStopRolling;

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

    public void StartedToRoll()
    {
        if (slotMachineStartedRolling != null)
        {
            slotMachineStartedRolling();
        }
    }
    public void CompletedEvaluation()
    {
        if (evaluationComplete != null)
        {
            evaluationComplete();
        }
    }

    public void RollsCompleted()
    {
        if (slotMachineRollingCompleted != null)
        {
            slotMachineRollingCompleted();
        }
    }
    public void OnReelStopRolling(int id)
    {
        if (onReelStopRolling != null)
        {
            onReelStopRolling(id);
        }
    }
}
