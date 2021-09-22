using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineReel : MonoBehaviour
{
    public SlotMachineMain mainMachine;
    public bool isRolling = false;
    [Header("SetByPlayer")]
    public Vector2 minMaxPos;
    private float reelRollSpeed;
    public int reelID;

    private void Start()
    {
        mainMachine = transform.parent.parent.GetComponent<SlotMachineMain>();
        SlotMachineEvents.instance.slotMachineStartedRolling += StartReel;
        SlotMachineEvents.instance.onReelStopRolling += CheckReel;
        foreach(Transform chld in transform)
        {
            chld.GetComponent<Symbol>().reelSlot = this;
        }
        reelRollSpeed = Random.Range(mainMachine.rollSpeed.x, mainMachine.rollSpeed.y);
    }
    
    private void Update()
    {
        if(isRolling)
        {
            transform.Translate(Vector2.down * reelRollSpeed * Time.deltaTime);
            if (this.transform.localPosition.y<=minMaxPos.x)
            {
                this.transform.localPosition = new Vector2(this.transform.localPosition.x, minMaxPos.y);
            }
        }
    }
    //Function Called by an event when player pulls
    public void StartReel()
    {
        isRolling = true;
    }

    public void StopReel()
    {
        isRolling = false;
        Debug.Log("stop Reel");
        float temp = this.transform.localPosition.y / 2; 
        /*
        if (Mathf.Round(temp) * 2 <= minMaxPos.x)
        {
            this.transform.localPosition = new Vector2(this.transform.localPosition.x, minMaxPos.y);
            temp = 4;
        }*/
        StartCoroutine("LerpStop", Mathf.Round(temp) * 2);
    }

    public void CheckReel(int eventIDs)
    {
        if(reelID-1==eventIDs)
        {
            StartCoroutine("DelayedStop", Random.Range(.5f,3f));
        }
    }

    IEnumerator DelayedStop(float sec)
    {
        yield return new WaitForSeconds(sec); 
        StopReel();
    }

    IEnumerator LerpStop(int dest)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        if (this.transform.position.y != dest)
        {
            this.transform.localPosition = new Vector2(this.transform.localPosition.x, Mathf.Lerp(this.transform.localPosition.y, dest, 0.1f));
            if (transform.localPosition.y >= dest - 0.01 && transform.localPosition.y <= dest + 0.01)
            {
                this.transform.localPosition = new Vector2(this.transform.localPosition.x, dest);
                if (reelID == 5)
                {
                    SlotMachineEvents.instance.RollsCompleted();
                    Debug.Log("Completed Reel");
                }
                else
                {
                    SlotMachineEvents.instance.OnReelStopRolling(reelID);
                }
            }
            else
            {
                StartCoroutine("LerpStop", dest);
            }
        }
    }
}
