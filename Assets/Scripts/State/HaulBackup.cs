/*using System.Collections;
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

    public Haul(int _id, Entity _target, Job _nextJob = null, bool _takeFromStockpile = false) : base("Haul", _nextJob, _target) // Called first
    {
        takeFromStockpile = _takeFromStockpile;
        if (takeFromStockpile)
        {
            returnTo = _target;
        }
        else
        {
            target = _target;
        }

        resourceID = _id;

    }

    public override void init(Person _person) // Called second
    {
        base.init(_person);
        if (takeFromStockpile)
        {
            target = getClosestStockpile();
        }
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

                //Find location or go back to original
                if (returnTo != null)
                {

                    person.pickUpItem(target.GetComponent<Inventory>().inInv); // 2.Pick up Item.
                    target.GetComponent<Inventory>().inInv = null;

                    hasItem = true;

                    target = returnTo;
                    jobNode = target.currNode;

                    isAtLoc = false;
                }
                else
                {
                    var tempTarget = getClosestStockpile(); //3. Find return location.  Right now only works with stockpiles, need to make work with other locations like jobsites. Held in temp var to make sure it is not null (if no stockpiles are available).

                    if (tempTarget == null) // If no open stockpiles
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
                break;
            case HaulActions.dropOffItem:
                person.dropOffItem(target.GetComponent<Inventory>()); // 5. Drop off item. Right now only works with stockpiles, need to make work with other locations like jobsites.

                if (nextJob != null) // Refactor maybe
                {
                    nextJob.isAtLoc = true;
                }

                Finished(); // Set the person's state to the next state
                break;
        }
    }

    public override int getCarried() { return hasItem ? resourceID : -1; }

    /*public override void arrived()
    {
        if(isAtLoc && hasItem)
        {
            person.dropOffItem(target.GetComponent<Inventory>()); // 5. Drop off item. Right now only works with stockpiles, need to make work with other locations like jobsites.


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
                person.pickUpItem(target); // 2.Pick up Item.
                hasItem = true;

                target = tempTarget;
                jobNode = target.currNode; //3.5 set that new location
                isAtLoc = false;
            }
            
        }

    }


    public HaulActions NextAction()
    {
        if (!isAtLoc && !hasItem)
        {
            return HaulActions.goToItem;
        }
        else if (isAtLoc && !hasItem)
        {
            return HaulActions.pickUpItem;
        }
        else if (!isAtLoc && hasItem)
        {
            return HaulActions.goToItem;
        }
        else
        {
            return HaulActions.dropOffItem;
        }

    }

    public Entity getClosestInventory() // Find closest stockpile that accepts the resource
    {
        TargetFilter stockFilter = new TargetFilter
        {
            Accepts = e => e.GetComponent<Inventory>() && !e.GetComponent<Inventory>().isFull()
        };

        Entity e = GameManager.init.findClosestEntity(person, person, stockFilter);
        if (e is not null)
        {
            return e;
        }
        return null;
    }

    public Entity getClosestStockpile() // Find closest stockpile that accepts the resource
    {
        TargetFilter stockFilter;
        if (takeFromStockpile)
        {
            stockFilter = new TargetFilter
            {
                Accepts = e => e.GetComponent<Inventory>() && e.GetComponent<Inventory>().isStockpile && e.GetComponent<Inventory>().givesResources(resourceID)
            };

        }
        else
        {
            stockFilter = new TargetFilter
            {
                Accepts = e => e.GetComponent<Inventory>() && e.GetComponent<Inventory>().isStockpile
            };
        }


        Entity e = GameManager.init.findClosestEntity(person, person, stockFilter);
        if (e is StockPile)
        {
            return e;
        }
        return null;
    }

}
*/