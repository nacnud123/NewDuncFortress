using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Haul : Job
{
    public int resourceID = 0;
    bool hasItem = false;

    public override void init(Person _person) // Called second
    {
        base.init(_person);
        jobNode = target.currNode;
    }

    public Haul(int _id, Entity _target, Job _nextJob = null) : base("Haul", _nextJob) // Called first
    {
        target = _target;
        resourceID = _id;
    }


    public override void tick()
    {
        if(isAtLoc == false && jobNode != null)
        {
            person.setJob(new Move(jobNode, this)); // 1. Go to Location. / 4. Go to return location
        }
        if (isAtLoc)
        {
            this.arrived(); // Arrived at item
        }

    }

    public override int getCarried() { return hasItem ? resourceID : -1; }

    public override void arrived()
    {
        if(isAtLoc && hasItem)
        {
            person.dropOffItem((StockPile)target); // 5. Drop off item. Right now only works with stockpiles, need to make work with other locations like jobsites.


            Finished(); // Set the person's state to the next state
            return; // End the state because it is done;
        }
        if (isAtLoc)
        {
            var tempTarget = getClosestStockpile(); //3. Find return location.  Right now only works with stockpiles, need to make work with other locations like jobsites. Held in temp var to make sure it is not null (if no stockpiles are available).

            if(tempTarget == null) // If no open stockpiles
            {
                JobQueue.init.waitingJobs.Add(new Haul(resourceID, target));  // Add a new haul job to the waiting queue because there are no stockpiles available.
                Finished(); // Set the person's state to null or to the next state.
                return; // End the state because there are not stockpiles.
            }
            else // There are stockpiles so we are good to pick up the item and move it to the stockpile.
            {
                person.pickUpItem(target); // 2.Pick up Item.
                hasItem = true;

                target = tempTarget;
                jobNode = target.currNode; //3.5 set that new location
                isAtLoc = false;
            }
            
        }

    }


    public Entity getClosestStockpile() // Find closest stockpile that accepts the resource
    {
        TargetFilter stockFilter = new TargetFilter
        {
            Accepts = e => e.acceptsResource(resourceID)  && !e.GetComponent<StockPile>().isFull
        };

        Entity e = GameManager.init.findClosestEntity(person, person, stockFilter);
        if (e is StockPile)
        {
            return e;
        }
        return null;
    }

}
