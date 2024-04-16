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
        if (inInv == null)
            return false;

        return inInv.GetComponent<Resource>().currentStackSize >= inInv.GetComponent<Resource>().maxItemStack;
    }

    public void dropOffItem(Entity item, Person parent, Inventory place)
    {
        if(inInv != null)
        {
            if(item.GetComponent<Resource>())
            {
                int remaining = inInv.GetComponent<Resource>().addToStack(item.GetComponent<Resource>());
                if (remaining != -1)
                {
                    parent.inventory.inInv.transform.parent = null;

                    ArrayList neighbors = new ArrayList();
                    GridManager.init.GetNeighbours(place.currNode, neighbors);
                    Node temp = (Node)neighbors[Random.Range(0, neighbors.Count)];
                    temp.parentGameNode.tileInv.inInv = item;

                    parent.inventory.inInv.transform.position = temp.parentGameNode.tileInv.displaySpr.gameObject.transform.position;
                    // Add to stack with some remaining
                    item.GetComponent<Resource>().currentStackSize -= remaining;
                }
                else
                {
                    // Add to stack with no remaining
                    parent.inventory.inInv.transform.parent = place.displaySpr.gameObject.transform;
                    parent.inventory.inInv.transform.position = place.displaySpr.gameObject.transform.position;
                    Debug.Log("dropOffItem not -1");
                }
            }
        }
        else
        {
            parent.inventory.inInv.transform.parent = place.displaySpr.gameObject.transform;
            parent.inventory.inInv.transform.position = place.displaySpr.gameObject.transform.position;
            inInv = item;
        }
    }


    public override bool givesResources(int resourceID)
    {
        if (inInv == null)
            return false;
        else
            return (int)((Resource)inInv).itemType == resourceID;
    }

}
