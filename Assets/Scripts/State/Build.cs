using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : State
{
    int testing;
    public Entity returnTo;
    public bool canBuild;

    // TODO: Switch to haul
    public Build(Entity _target, int _resourceID, State _nextJob = null) : base("Building", _nextJob)
    {
        priority = JobPriority.Medium;

        testing = _resourceID;
        target = _target;
        jobNode = target.currNode;
    }

    public override void init(Person _person)
    {
        base.init(_person);
    }

    public override void tick()
    {
        if (isAtLoc == false)
        {
            person.setJob(new Haul(testing, target, this));
        }
        if (isAtLoc)
        {
            this.arrived();
        }
    }

    public override void arrived()
    {


        /*if (isAtLoc && target != null && hasItem)
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
        }*/
    }

}
