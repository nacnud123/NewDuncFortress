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
            GameObject tempObj = Instantiate(Resources.Load<GameObject>($"Resources/{dropItem.name}"), this.transform.position, Quaternion.identity);
            currNode.parentGameNode.tileInv.addItem(tempObj.GetComponent<Resource>());
            return true;
        }
        return false;
    }

    public override bool givesResources(int resourceID){ return true; }

}
