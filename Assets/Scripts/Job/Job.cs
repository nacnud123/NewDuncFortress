using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Job
{

    public enum JobPriority // Job Priority stuff
    {
        High,
        Medium,
        Low
    }


    public JobPriority priority;
    protected Person person;
    protected Entity target;

    public ArrayList pathArray;
    public int index = 0;
    public Vector3 nextPos;
    public Node jobNode;

    public bool isAtLoc = false;
    public bool isAssigned = false;


    public string name { get; protected set; }
    public Job nextJob { get; protected set; }

    public Job(string _name, Job _nextJob)
    {
        name = _name;
        nextJob = _nextJob;
    }

    public virtual void init(Person _person)
    {
        this.person = _person;
    }

    public virtual void tick() // Run every update
    {
    }
    
    public virtual void Finished()
    {
        person.setJob(nextJob);
    }

    public virtual bool isValidTarget(Entity e) { return false; }

    /*public virtual bool hasTarget() // Maybe redundent now
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
    }*/


    public virtual void setTarget(Entity e) { target = e; jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position))); } // Set the target entity and set the jobNode.

    public virtual void arrived() { } // When they arive to the job site

    public virtual void cantReach() // If they cannot reach the job site
    {
        if(Random.value < .1)
        {
            target = null;
        }
    }

    public override string ToString()
    {
        return string.Format("[{0}State]", name);
    }

    public virtual int getCarried() { return -1; }

    /*public virtual void collide(Entity e) // Maybe redundent
    {
        if (isValidTarget(e))
        {
            setTarget(e);
        }
        else
        {
            cantReach();
        }
    }*/

}
/*public class Goto : Job // Maybe redundent
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

}*/