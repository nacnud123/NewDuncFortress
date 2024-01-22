using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
    protected Person person;

    public float xTarget, yTarget, targetDist;
    protected Entity target;
    protected int bonusRadius = 2;
    protected int boreTime = 500;

    public void init(Person _person)
    {
        this.person = _person;
    }

    public void tick()
    {
        if(boreTime > 0)
        {
            if (--boreTime == 0) person.setJob(null);
        }
    }

    public virtual bool isValidTarget(Entity e) { return false; }

    public virtual bool hasTarget()
    {
        Entity e = person.getRandomTarget(5, 60);
        if(e != null && isValidTarget(e))
        {
            if(target == null || e.distance(person) < target.distance(person))
            {
                setTarget(e);
            }
        }

        if(target != null && !target.alive) { target = null; }
        if(target == null) { return false; }

        xTarget = target.transform.position.x; // Change to just target.x same for y
        yTarget = target.transform.position.y;
        targetDist = target.r + bonusRadius;
        return true;
    }


    public virtual void setTarget(Entity e) { target = e; }

    public virtual void arrived() { }

    public virtual void cantReach()
    {
        if(Random.value < .1)
        {
            target = null;
        }
    }

    public virtual int getCarried() { return -1; }

    public virtual void collide(Entity e)
    {
        if (isValidTarget(e))
        {
            setTarget(e);
        }
        else
        {
            cantReach();
        }
    }

}
public class Goto : Job
{
    private GameObject _target;
    public Goto(GameObject target_)
    {
        this._target = target_;
        bonusRadius = 15;
    }

    public override bool isValidTarget(Entity e) { return e == target; }

    public override void arrived() { person.setJob(null); }

}

public class Gather: Job
{
    bool hasResource = false;
    public int resourceID = 0;

    private Entity returnTo;

    public Gather(int id, Entity _returnTo)
    {
        resourceID = id;
        this.returnTo = _returnTo;
    }

    public override int getCarried() { return hasResource ? resourceID : -1; }

    public override bool isValidTarget(Entity e)
    {
        if(!hasResource && e.givesResources(resourceID))
        {
            return true;
        }
        if(hasResource && e.acceptsResource(resourceID)) // Need to fix
        {
            return true;
        }
        return false;
    }

    public override void arrived()
    {
        if(!hasResource && target != null && target.givesResources(resourceID))
        {
            if (target.gatherResource(resourceID))
            {
                hasResource = true;
                target = returnTo;
            }
            boreTime = 1000;
        }
        else if(hasResource && target != null && target.acceptsResource(resourceID))
        {
            hasResource = false;
            target = null;
            GameManager.init.woodAvail += 1;
            person.setJob(null);
        }
    }
}
