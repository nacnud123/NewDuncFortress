using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Build : Job
{
    bool hasResource = false;
    public int resourceID = 0;

    public Build(int id, Entity _target, Job _nextJob = null) : base("Build", _nextJob)
    {
        adjacent = true;
        priority = JobPriority.High;
        resourceID = id;
        target = _target;
    }

    public override void init(Person _person)
    {
        base.init(_person);
        Debug.Log("Build Init!");
        jobNode = target.currNode;

        if (!GameManager.init.isInvWithItem(resourceID))
        {
            WaitingJob waiting = new WaitingJob(this);
            waiting.updateAction += jobWaitUpdate;
            JobQueue.init.waitingJobs.Add(waiting);
        }
    }

    public override void tick()
    {
        if (isAtLoc)
        {
            arrived();
        }
        else
        {
            person.setJob(new Haul(resourceID, target, this, true));
        }
    }

    public override void arrived()
    {
        if (target.GetComponent<Furniture>().build())
        {
            Finished();
            base.arrived();
        }
    }


    public bool jobWaitUpdate()
    {
        return GameManager.init.isInvWithItem(resourceID);
    }



}
