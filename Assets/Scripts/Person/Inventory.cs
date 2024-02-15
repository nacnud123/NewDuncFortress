using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Entity // may not need to be an entity, could just stick with Monobehavior 
{
    //public Dictionary<Resource, int> inInv = new Dictionary<Resource, int>();
    public List<Resource> inInv = new List<Resource>();
    public bool isStockpile;
    public bool isFull = false;


    public SpriteRenderer sr;

    public override void Start()
    {
        // Just do nothing;
    }

    public bool addItem(Resource item)
    {
        checkIfFull();
        if (inInv.Contains(item) && !isFull)  // If the item already exists in the inv, add to its ammount
        {
            inInv[inInv.IndexOf(item)].ammount += 1;
            item.alive = false;
            return true;
        }
        else if(!isFull) // If the item does not exist and the inv are not full.
        {
            inInv.Add(item);
            sr.sprite = item.itemSR.sprite;
            sr.color = item.itemSR.color;

            item.gameObject.transform.parent = this.transform;
            item.gameObject.transform.position = this.transform.position;
            return true;
        }
        else
        {
            return false; // If the inv is full do nothing. Should never reach this point but its good to have
        }
    }

    public void addItem(Inventory addInv)  // Adds items to this inventory from another inventory 
    {
        addInv.inInv.ForEach
            (
            x =>
            {
                this.addItem(x);
            }
            );
    }

    public void dropItem(Resource specificItem = null)
    {
        int temp = -1; // Used to hold the position for the loop

        for(int i = 0; i < inInv.Count; i++) // Refactor
        {
            if(specificItem != null && inInv[i] == specificItem)
            {
                inInv[i].transform.parent = null;
                temp = i;
            }
            else if(specificItem == null)
            {
                inInv[i].transform.parent = null;
            }
        }

        if (temp != -1)
        {
            inInv.RemoveAt(temp);
        }
        else
        {
            inInv.Clear();
        }
    }

    public bool checkIfFull()
    {
        if(inInv.Count == 0)
        {
            isFull = false;
        }
        else
        {
            inInv.ForEach(
            x =>
            {

                if (x.ammount > GameManager.init.maxPileSize[(int)x.itemType])
                { isFull = true; }
            }
            );
        }
        return isFull;

    }


    public override bool acceptsResource(int resourceID)
    {
        bool returnVal = false;
        if(inInv.Count == 0) { returnVal = true; }
        else
        {
            inInv.ForEach(
                x =>
                {
                    if ((int)x.itemType == resourceID) { returnVal = true; }
                }
                );
        }
        return returnVal;
    }

    public override bool givesResources(int resourceID)
    {
        bool returnVal = true;
        if(inInv.Count == 0) { returnVal = false; }
        else
        {
            inInv.ForEach(
                x =>
                {
                    if ((int)x.itemType == resourceID) { returnVal = true; }
                }
                );
        }
        return returnVal;
    }


    public Entity getItemWithID(int resourceID)
    {
        Entity e = null;
        inInv.ForEach(
            x =>
            {
                if ((int)x.itemType == resourceID) { e = x; }
            }
            );
        return e;
    }
}
