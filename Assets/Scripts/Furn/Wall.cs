using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Furniture
{


    Wall():base("Wall", 20)
    {

    }

    public override void Start()
    {
        base.Start();

        JobQueue.init.Enqueue(new Build(this, (int)GameManager.ITEMTYPE.WOOD));
    }

    /*public Entity getClosestStockpile()
    {
        //TODO: Fix
        TargetFilter stockFilter = new TargetFilter
        {
            Accepts = e => e.GetComponent<StockPile>() && e.GetComponent<StockPile>().isFull && e.GetComponent<StockPile>().inventory.inInv.GetComponent<Resource>().itemType == Resource.ItemType.WOOD
        };

        Entity e = GameManager.init.findClosestEntity(this, this, stockFilter);
        if (e is StockPile)
        {
            return e;
        }

        return null;
    }*/

}
