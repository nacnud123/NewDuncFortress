using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Entity : MonoBehaviour
{
    public float x, y, r;
    public bool alive = true;

    public Node currNode;



    private void Awake()
    {
        x = transform.position.x;
        y = transform.position.y;
        currNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(this.transform.position)));
        r = 1;
    }

    public float distance(Entity e)
    {
        float xd = x - e.x;
        float yd = y - e.y;
        return xd * xd + yd * yd;
    }

    public virtual void tick() { }

    public bool collides(float ex, float ey, float er)
    {
        if (r < 0) return false;

        float xd = x - ex;
        float yd = y - ey;
        return (xd * xd + yd * yd) < (r * r + er * er);
    }

    public Entity getRandomTarget(float radius, float rnd, TargetFilter filter = null)
    {
        float xt = x + (Random.value * 2 - 1) * rnd;
        float yt = y + (Random.value * 2 - 1) * rnd;
        return GameManager.init.getEntityAt(xt, yt, radius, this, filter);

    }

    public virtual bool givesResources(int resourceID)
    {
        return false;
    }

    public virtual bool gatherResource(int resourceID) { return false; }

    public virtual bool acceptsResource(int resourceID) { return false; }

    public virtual bool submitResource(int resourceID) { return false; }
}
