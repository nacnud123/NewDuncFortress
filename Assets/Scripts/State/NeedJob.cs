using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedJob : Job
{
    Need currNeed;
    public NeedJob(Job _nextJob = null) : base("Need", _nextJob)
    {
    }

    public override void init(Person _person)
    {
        this.JobTime = 10;
        base.init(_person);
    }

    public override void tick()
    {
        if (!isAtLoc)
        {
            foreach (Need ne in person.needs)
            {
                ne.Update();
                //Debug.Log($"{ne.Name} - Ammount: {ne.Amount}");
                if (ne.Amount > 50 && ne.Amount < 100 && ne.restoreNeedFurn != null && ne.inQueue == false)
                {
                    JobQueue.init.Enqueue(new Move(ne.restoreNeedFurn.currNode, this));
                    ne.inQueue = true;
                    currNeed = ne;
                }

            }
        }
        else
        {
            currNeed.beingWorkedOn = true;
           //Debug.Log($"JobTime: {JobTime}. {currNeed.Name} - Ammount: {currNeed.Amount}");
            currNeed.Amount -= Time.deltaTime * 5;
            base.tick();
            if(this.JobTime <= 0)
            {
                isAtLoc = false;
                currNeed.inQueue = false;
                currNeed.beingWorkedOn = false;
            }

        }
        float needPercent = 0;
        Need bigestNeed = null;
        
        

        
    }
}
