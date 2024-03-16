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
        base.init(_person);
    }

    public override void tick()
    {
        float needPercent = 0;
        Need bigestNeed = null;
        
        foreach(Need ne in person.needs)
        {
            ne.Update();
            //Debug.Log($"{ne.Name} - Ammount: {ne.Amount}");
            if (ne.Amount > 50 && ne.Amount < 100 && ne.restoreNeedFurn != null && ne.inQueue == false)
            {
                JobQueue.init.Enqueue(new Move(ne.restoreNeedFurn.currNode));
                ne.inQueue = true;
            }

        }

        
    }
}
