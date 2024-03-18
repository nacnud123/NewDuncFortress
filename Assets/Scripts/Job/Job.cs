using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;
using System;

public class Job
{

    public enum JobPriority // Job Priority stuff
    {
        High,
        Medium,
        Low
    }

    public bool adjacent = false;

    public JobPriority priority;
    protected Person person;
    public Entity target;

    public ArrayList pathArray;
    public int index = 0;
    public Vector3 nextPos;
    public Node jobNode;

    public bool isAtLoc = false;
    public bool isAssigned = false;


    public string name { get; protected set; }
    public Job nextJob { get; protected set; }

    public bool jobRepeats = false;

    public event Action<Job> onJobCompleted;
    public event Action<Job> OnJobStopped;

    // Gets called each time some work is performed -- maybe update the UI?
    public event Action<Job> OnJobWorked;

    public bool IsBeingWorked { get; set; }

    public float JobTime
    {
        get;
        protected set;
    }

    public Job(string _name, Job _nextJob)
    {
        name = _name;
        nextJob = _nextJob;
    }

    public Job(string _name, Job _nextJob, Entity _target)
    {
        name = _name;
        nextJob = _nextJob;
        target = _target;
    }

    public virtual void init(Person _person)
    {
        // TODO: Check if person can reach the destination
        this.person = _person;
    }

    public virtual void tick() // Run every update
    {
        JobTime -= Time.deltaTime;
        if(JobTime <= 0)
        {
            Finished();
        }
    }
    
    public virtual void Finished()
    {
        person.setJob(nextJob);
    }

    public virtual bool isValidTarget(Entity e) { return false; }


    public virtual void setTarget(Entity e) { target = e; jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position))); } // Set the target entity and set the jobNode.

    public virtual void arrived() { } // When they arive to the job site

    public virtual void cantReach() // If they cannot reach the job site
    {
        if(UnityEngine.Random.value < .1)
        {
            target = null;
        }
    }

    public override string ToString()
    {
        return string.Format("[{0}State]", name);
    }

    public virtual int getCarried() { return -1; }

   

}