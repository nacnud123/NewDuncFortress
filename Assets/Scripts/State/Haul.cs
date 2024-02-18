using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;


public enum HaulAction
{
    DumpMaterial,
    FindMaterial,
    PickupMaterial,
    DeliverMaterial,
    DropOffmaterial
}

public class Haul : State
{
    public int resourceID = 0;
    bool hasItem = false;
    Job job;
    Inventory tileInv;

    public Haul(int _id, Entity _target, Job _job, State _nextJob = null) : base("Haul", _nextJob) // Called first
    {
        target = _target;
        resourceID = _id;
        job = _job;
    }

    public override void init(Person _person) // Called second
    {
        base.init(_person);

        jobNode = target.currNode;
    }



    public override void tick()
    {
        HaulAction haulAction = nextAction();

        switch (haulAction)
        {
            case HaulAction.DumpMaterial:
                person.dropItem();
                hasItem = false;
                Finished();
                break;
            case HaulAction.FindMaterial:
                // Find a material somewhere
                target = getClosestInventory();
                jobNode = target.currNode;

                person.setJob(new Move(jobNode, this));
                break;
            case HaulAction.PickupMaterial:
                tileInv = GridManager.init.getNodeFromVec3(person.transform.position).parentGameNode.tileInv;
                person.pickUpItem((Resource)tileInv.getItemWithID(resourceID));
                hasItem = true;
                break;
            case HaulAction.DeliverMaterial:
                person.setJob(new Move(job.jobNode, this));
                break;
            case HaulAction.DropOffmaterial:
                tileInv = GridManager.init.getNodeFromVec3(person.transform.position).parentGameNode.tileInv;
                person.dropOffItem(tileInv);
                hasItem = false;
                break;
        }

        /*if (isAtLoc == false && jobNode != null)
        {
            person.setJob(new Move(jobNode, this)); // 1. Go to Location. / 4. Go to return location
        }
        if (isAtLoc)
        {
            this.arrived(); // Arrived at item
        }*/

    }

    public override int getCarried() { return hasItem ? resourceID : -1; }

    public override void arrived()
    {
        /* if (isAtLoc && hasItem)
         {

             return; // End the state because it is done;
         }
         if (isAtLoc)
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
                 if (fromStockpile)
                 {
                     person.pickUpItem((Resource)target.GetComponent<Inventory>().getItemWithID(resourceID)); //TODO: Typecast bad, can cause lots of errors, need to fix
                 }
                 else
                 {
                     person.pickUpItem((Resource)target); // 2.Pick up Item. TODO: Typecast bad, can cause lots of errors, need to fix
                 }

                 hasItem = true;

                 target = tempTarget;
                 jobNode = target.currNode; //3.5 set that new location
                 isAtLoc = false;
             }

         }*/

    }


    public Entity getClosestInventory() // Find closest inventory that accepts the resourced
    {
        TargetFilter inventoryFilter = new TargetFilter();
        if (job.canTakeFromStockpile)
        {
            inventoryFilter = new TargetFilter
            {
                Accepts = e => e.givesResources(resourceID) && e.GetComponent<Inventory>().isStockpile
            };
        }
        else
        {
            inventoryFilter = new TargetFilter
            {
                Accepts = e => e.acceptsResource(resourceID) && !e.GetComponent<Inventory>().isStockpile
            };
        }

        Entity e = GameManager.init.findClosestEntity(person, person, inventoryFilter);
        if (e is not null)
        {
            return e;
        }
        return null;
    }

    private HaulAction nextAction()
    {
        if(!isAtLoc && !hasItem)
        {
            return HaulAction.FindMaterial;
        }
        else if(isAtLoc && !hasItem)
        {
            isAtLoc = false;
            return HaulAction.PickupMaterial;
        }
        else if(!isAtLoc && hasItem)
        {
            return HaulAction.DeliverMaterial;
        }
        else if(isAtLoc && hasItem)
        {
            return HaulAction.DropOffmaterial;
        }
        else
        {
            return HaulAction.DumpMaterial;
        }

    }

}