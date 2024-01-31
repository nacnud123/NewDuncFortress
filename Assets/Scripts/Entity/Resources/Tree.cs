using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tree : Entity
{
    public int stamina = 100;

    public Resource dropItem;

    private void Awake()
    {
        r = .1f;
    }

    public override bool gatherResource(int resourceID)
    {
        stamina -= 5;
        if(stamina <= 0)
        {
            alive = false;
            Instantiate(dropItem, transform.position, Quaternion.identity);
            return true;
        }
        return false;
    }

    public override bool givesResources(int resourceID){ return true; }

}
