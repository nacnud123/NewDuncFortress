using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Haul : Job
{
    public int resourceID = 0;
    bool hasItem = false;
    public bool takeFromStockpile;

    public Entity returnTo;

    public enum HaulActions
    {
        pickUpItem, // Also find item?
        goToItem,
        dropOffItem
    }

    public Haul(int _id, Entity _target, Job _nextJob = null, bool _takeFromStockpile = false) : base("Haul", _nextJob) // Called first
    {
        takeFromStockpile = _takeFromStockpile;

        if(!takeFromStockpile)
            target = _target;

        resourceID = _id;
    }

    public override void init(Person _person) // Called second
    {
        base.init(_person);

        if (takeFromStockpile)
            target = getClosestInventory();

        if(target != null)
            jobNode = target.currNode;
    }

    public override void tick()
    {
        HaulActions nextAction = NextAction();

        switch (nextAction)
        {
            case HaulActions.goToItem:
                person.setJob(new Move(jobNode, this));

                break;
            case HaulActions.pickUpItem:
                if(nextJob != null)
                {
                    
                    person.pickUpItem(target.currNode.parentGameNode.tileInv.inInv); // 2.Pick up Item.
                    hasItem = true;

                    target.currNode.parentGameNode.tileInv.inInv = null;

                    target = nextJob.target;
                    jobNode = target.currNode; //3.5 set that new location
                    isAtLoc = false;
                }
                else
                {
                    var tempTarget = getClosestInventory(); //3. Find return location.  Right now only works with stockpiles, need to make work with other locations like jobsites. Held in temp var to make sure it is not null (if no stockpiles are available).

                    if (tempTarget == null) // If no open stockpiles
                    {
                        JobQueue.init.waitingJobs.Add(new Haul(resourceID, target));  // Add a new haul job to the waiting queue because there are no stockpiles available.
                        Finished(); // Set the person's state to null or to the next state.
                        return; // End the state because there are not stockpiles.
                    }
                    else // There are stockpiles so we are good to pick up the item and move it to the stockpile.
                    {
                        person.pickUpItem(target.currNode.parentGameNode.tileInv.inInv); // 2.Pick up Item.

                        hasItem = true;

                        target.currNode.parentGameNode.tileInv.inInv = null;

                        target = tempTarget;
                        jobNode = target.currNode; //3.5 set that new location
                        isAtLoc = false;
                    }
                }

                

                break;
            case HaulActions.dropOffItem:
                if (nextJob != null)
                    target = nextJob.target;

                person.dropOffItem(target.currNode.parentGameNode.tileInv); // 5. Drop off item. Right now only works with stockpiles, need to make work with other locations like jobsites.

                if(nextJob != null)
                {
                    nextJob.isAtLoc = true;
                }

                Finished(); // Set the person's state to the next state
                break;
        }
    }

    public override int getCarried() { return hasItem ? resourceID : -1; }


    public HaulActions NextAction()
    {
        if(!isAtLoc && !hasItem)
        {
            return HaulActions.goToItem;
        }
        else if(isAtLoc && !hasItem)
        {
            return HaulActions.pickUpItem;
        }
        else if(!isAtLoc && hasItem)
        {
            return HaulActions.goToItem;
        }
        else
        {
            return HaulActions.dropOffItem;
        }
        
    }

    public Entity getClosestInventory() // Find closest stockpile that accepts the resource TODO: Make so it looks through all tiles to find the item it needs
    {
        TargetFilter stockFilter;
        if (takeFromStockpile)
        {
            stockFilter = new TargetFilter
            {
                Accepts = e => e.currNode.parentGameNode.tileInv.isStockpile && e.currNode.parentGameNode.tileInv.givesResources(resourceID)
            };
        }
        else
        {
            stockFilter = new TargetFilter
            {
                Accepts = e => e.currNode.parentGameNode.tileInv.isStockpile && !e.currNode.parentGameNode.tileInv.isFull()
            };
        }

        Entity e = GameManager.init.findClosestEntity(person, person, stockFilter);
        if (e is not null)
        {
            return e;
        }
        return null;
    }

}
