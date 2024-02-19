using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Entity : MonoBehaviour
{
    public float x, y, r;
    public bool alive = true;

    public Node currNode;



    public virtual void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        currNode = GridManager.init.getNodeFromVec3(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(this.transform.position)));
        r = 1;
        GameManager.init.entities.Add(this);
    }

    public virtual void tick() { }

    public virtual bool givesResources(int resourceID)
    {
        return false;
    }

    public virtual bool gatherResource(int resourceID) { return false; }

    public virtual bool acceptsResource(int resourceID) { return false; }

    public virtual bool submitResource(int resourceID) { return false; }
}
