using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Gather : Job
{
    bool hasResource = false;
    public int resourceID = 0;

    public Gather(int id, Entity _target, Job _nextJob = null):base("Gather", _nextJob)
    {
        priority = JobPriority.High;
        resourceID = id;
        target = _target;
        jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
    }

    public override void init(Person _person)
    {
        base.init(_person);
    }

    public override void tick()
    {
        Debug.Log("Gather Tick!");
        if (isAtLoc == false && target != null)
        {
            jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
            person.setJob(new Move(jobNode, this));
        }
        if (isAtLoc)
        {
            this.arrived();
        }
    }

    public override int getCarried() { return hasResource ? resourceID : -1; }

    public override bool isValidTarget(Entity e)
    {
        if (!hasResource && e.givesResources(resourceID))
        {
            return true;
        }
        if (hasResource && e.acceptsResource(resourceID)) // Need to fix
        {
            return true;
        }
        return false;
    }

    public override void arrived()
    {
        if (!hasResource && target != null && target.givesResources(resourceID))
        {
            if (target.gatherResource(resourceID))
            {
                hasResource = true;
                Finished();
            }
            //boreTime = 1000;
        }
    }
}
