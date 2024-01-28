using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Job
{
    protected Person person;

    public float xTarget, yTarget, targetDist;
    protected Entity target;
    protected int bonusRadius = 2;
    protected int boreTime = 500;

    public ArrayList pathArray;
    public int index = 0;
    public Vector3 nextPos;

    public Node jobNode;

    public bool isAtLoc = false;

    public bool isAssigned = false;

    public string name { get; protected set; }

    public virtual void init(Person _person)
    {
        this.person = _person;
    }

    public virtual void tick()
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
        jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
        targetDist = target.r + bonusRadius;
        return true;
    }


    public virtual void setTarget(Entity e) { target = e; jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position))); }

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
        name = "Goto";
        this._target = target_;
        bonusRadius = 15;
    }

    public override bool isValidTarget(Entity e) { return e == target; }

    public override void arrived() { person.setJob(null); }

}