using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : Job
{
    int testing;
    public Entity returnTo;
    public bool hasItem;

    public Build(Entity _target, Entity _returnTo, Job _nextJob = null) : base("Building", _nextJob)
    {
        priority = JobPriority.Medium;
        target = _target;
        jobNode = target.currNode;
        returnTo = _returnTo;
    }

    public override void init(Person _person)
    {
        base.init(_person);
    }

    public override void tick()
    {
        if (isAtLoc == false && jobNode != null)
        {
            person.setJob(new Move(jobNode, this));
        }
        if (isAtLoc)
        {
            this.arrived();
        }
    }

    public override void arrived()
    {
        if (isAtLoc && target != null && hasItem)
        {
            if (((Furniture)target).build())
            {
                person.dropOffItem();

                Finished();
            }
        }
        else if(isAtLoc && target != null)
        {
            if (target.GetComponent<StockPile>())
            {
                var tempTarget = target.GetComponent<StockPile>().inventory.inInv;

                person.pickUpItem(tempTarget);

                target.GetComponent<StockPile>().isFull = false;
                target.GetComponent<StockPile>().inventory.inInv = null;
            }

            hasItem = true;
            target = returnTo;
            jobNode = target.currNode;
            isAtLoc = false;
        }
    }

}
