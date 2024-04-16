using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Inventory : Entity
{
    public SpriteRenderer displaySpr;

    public bool isStockpile = false;

    public string Type { get; private set; }

    public int maxStackSize { get; set; }

    public string category { get; private set; }


    public override void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        currNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(this.transform.position))); // TODO: Change so it dosen't make a new node.
    }

    public Entity inInv = null;

    public bool isFull()
    {
        return inInv != null;
    }

    public void dropOffItem(Entity item)
    {
        inInv = item;
    }


    public override bool givesResources(int resourceID)
    {
        if (inInv == null)
            return false;
        else
            return (int)((Resource)inInv).itemType == resourceID;
    }

}
