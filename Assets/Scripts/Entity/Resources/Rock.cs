using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rock : Entity
{
    public int stamina = 100;

    public Resource dropItem;

    public int resourceIDGiven;

    public override void Start()
    {
        base.Start();
    }

    public override bool gatherResource(int resourceID)
    {
        stamina -= 5;
        if(stamina <= 0)
        {
            alive = false;
            var temp = Instantiate(dropItem, transform.position, Quaternion.identity);
            this.currNode.parentGameNode.tileInv.inInv = temp;
            return true;
        }
        return false;
    }

    public override bool givesResources(int resourceID){ return resourceIDGiven == resourceID; }

}
