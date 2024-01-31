using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Haul : Job
{
    public int resourceID = 0;
    bool hasItem = false;

    public override void init(Person _person)
    {
        base.init(_person);
    }

    public Haul(int id, Entity _target)
    {
        priority = JobPriority.Low;
        target = _target; // first target, an item
        name = "Haul";
        resourceID = id; // the item type
        jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
    }


    public override void tick()
    {
        if(isAtLoc == false && target != null)
        {
            jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
            person.setJob(new Move(this)); // Move to the item
        }
        if (isAtLoc)
        {
            this.arrived(); // Arrived at item
        }

    }

    public override void arrived()
    {
        if(!hasItem && target != null && (int)target.GetComponent<Resource>().itemType == resourceID) // Pick up
        {
            var tempTarget = getClosestStockpile();
            if(tempTarget == null)
            {
                person.setJob(null);
            }
            else if(target != null)
            {
                person.pickUpItem(target);
                hasItem = true;
                target = tempTarget; // Find closest stockpile that accepts this resource
                target.GetComponent<StockPile>().isFull = true;
            }
            isAtLoc = false;
        }
        else if(hasItem && target != null && (target.acceptsResource(resourceID) || target.gameObject.transform.parent.GetComponent<Entity>().acceptsResource(resourceID))) // Drop off
        {
            person.dropOffItem(target.GetComponent<StockPile>());
            hasItem = false;
            
            target = null;
            GameManager.init.woodAvail += 1;
            
            person.setJob(null);
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
