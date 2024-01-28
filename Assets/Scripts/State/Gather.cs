using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Gather : Job
{
    bool hasResource = false;
    public int resourceID = 0;

    public Gather(int id, Entity _target)
    {
        name = "Gather";
        resourceID = id;
        target = _target;
        jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
    }

    public override void tick()
    {
        Debug.Log("Gather Tick!");
        if (isAtLoc == false && target != null)
        {
            jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
            person.setJob(new Move(this));
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
                target = getClosestStockpile(50,50);
                isAtLoc = false;
            }
            boreTime = 1000;
        }
        else if (hasResource && target != null && target.acceptsResource(resourceID))
        {
            hasResource = false;
            target = null;
            GameManager.init.woodAvail += 1;
            person.setJob(null);
        }
    }

    public Entity getClosestStockpile(float r, float s)
    {
        TargetFilter stockFilter = new TargetFilter
        {
            Accepts = e => e.acceptsResource(resourceID)
        };

        Entity e = GameManager.init.findClosestEntity(person, person, stockFilter);
        if(e is StockPile)
        {
            return e;
        }
        return null;
    }
}
