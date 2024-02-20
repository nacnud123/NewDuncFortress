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
        priority = JobPriority.High;
        resourceID = id;
        target = _target;
        
    }

    public override void init(Person _person)
    {
        base.init(_person);
        Debug.Log("Build Init!");
        jobNode = target.currNode;
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




}
