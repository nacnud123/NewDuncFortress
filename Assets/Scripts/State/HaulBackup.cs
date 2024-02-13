/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Haul : Job
{
    public int resourceID = 0;
    bool hasItem = false;
    bool toStockpile = false;
    bool pickUpItem = false;

    public Haul(int _id, Entity _target, Job _nextJob = null, bool _toStockpile = false) : base("Haul", _nextJob) // Called first
    {
        resourceID = _id;
        toStockpile = _toStockpile;
        target = _target;
    }

    public override void init(Person _person) // Called second
    {
        base.init(_person);

        if(target == null)
        {
            target = getClosestInventory();
        }

        jobNode = target.currNode;
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

            person.dropOffItem((Inventory)target); // 5. Drop off item. Right now only works with stockpiles, need to make work with other locations like jobsites.
            person.dropItem();
            // TODO: Above - Typecast bad, can cause lots of errors, need to refactor.

            if (nextJob != null) { nextJob.isAtLoc = true; }

            Finished(); // Set the person's state to the next state

           
            return; // End the state because it is done;
        }
        if (isAtLoc)
        {
            var tempTarget = getClosestInventory(); //3. Find return location.  Right now only works with stockpiles, need to make work with other locations like jobsites. Held in temp var to make sure it is not null (if no stockpiles are available).

            if(tempTarget == null) // If no open stockpiles
            {
                JobQueue.init.waitingJobs.Add(new Haul(resourceID, target));  // Add a new haul job to the waiting queue because there are no stockpiles available.
                Finished(); // Set the person's state to null or to the next state.
                return; // End the state because there are not stockpiles.
            }
            else // There are stockpiles so we are good to pick up the item and move it to the stockpile.
            {
                person.pickUpItem((Resource)target); // 2.Pick up Item. TODO: Typecast bad, can cause lots of errors, need to fix
                hasItem = true;

                target = tempTarget;
                jobNode = target.currNode; //3.5 set that new location
                isAtLoc = false;
            }
            
        }

    }


    public Entity getClosestInventory() // Find closest inventory that accepts the resourced
    {
    TargetFilter inventoryFilter = new TargetFilter();
    if (nextJob != null && nextJob.name == "Building") // May be redundent
    {
    Debug.Log("Building Haul!");
    if (toStockpile)
    {
        inventoryFilter = new TargetFilter
        {
            Accepts = e => e.givesResources(resourceID) && e.GetComponent<StockPile>()
        };
    }
    else
    {
        inventoryFilter = new TargetFilter
        {
            Accepts = e => e.givesResources(resourceID) && !e.GetComponent<Inventory>()
        };
    }
}
else
{
    Debug.Log("Regular Haul!");
    if (toStockpile)
    {
        inventoryFilter = new TargetFilter
        {
            Accepts = e => e.acceptsResource(resourceID) && e.GetComponent<StockPile>()
        };
    }
    else
    {
        inventoryFilter = new TargetFilter
        {
            Accepts = e => e.acceptsResource(resourceID) && !e.GetComponent<Inventory>()
        };
    }
}






Entity e = GameManager.init.findClosestEntity(person, person, inventoryFilter);
if (e is not null)
{
    return e;
}
return null;
    }

}

*/
