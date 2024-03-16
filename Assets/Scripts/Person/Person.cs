using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Person : Entity
{
    #region Tesing stuff
    public Bed assignedBed;
    #endregion

    #region Needs
    /// <summary>
    /// 0 - Sleep, 1 - Hunger, 2 - Fun
    /// </summary>
    public List<Need> needs = new List<Need>() { new Sleep() };
    #endregion

    public float wanderTime = 0;
    public Job job;
    public List<Job> globalJobs = new List<Job>();

    public float moveTick = 0;

    public Inventory inventory;

    [SerializeField] private int hp = 100;
    private int maxHp = 100;

    public string personName = "Jim";
    public int mood = 100;

    /*private int xp = 0;
    private int nextLevel = 1;
    private int level = 0;*/
    //public float rot = 0;
   

    public float speed = 2f;

    private void Awake()
    {
        r = .1f;
        moveTick = Random.Range(0, 13);
        globalJobs.Add(new NeedJob());

        foreach (Job j in globalJobs)
        {
            j.init(this);
        }
    }

    public override void tick()
    {
        foreach(Job j in globalJobs)
        {
            j.tick();
        }

        currNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(this.transform.position))); // So stupid, using this instead of getNodeFromVec3 because using that causes memory overflow. Why? I don't know 

        if (job != null)
        {
            job.tick();
        }

        if(hp < maxHp && Random.Range(0,6) == 0)
        {
            hp++;
        }

        if(wanderTime == 0 && job != null)
        {
            //job.hasTarget();
        }

        if(job == null)
        {
            if(JobQueue.init.globalJobQueue.Count > 0)
            {
                setJob(JobQueue.init.Dequeue());
            }
            else
            {
                //Debug.Log("No Job!");
                setJob(new Wander());
            }
           
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Moveing to assigned bed");
            setJob(new Move(assignedBed.currNode));
        }

    }

    public void setJob(Job job)
    {
        this.job = job;
        if (job != null) job.init(this);
    }

    public void pickUpItem(Entity newItem)
    {
        inventory.inInv = newItem;
        newItem.transform.parent = this.transform;
        newItem.transform.position = this.transform.position;
    }

    public void dropOffItem(Inventory place = null) // = null may be redundent
    {
        inventory.inInv.transform.parent = place.displaySpr.gameObject.transform;
        inventory.inInv.transform.position = place.displaySpr.gameObject.transform.position;
        if(place != null)
        {
            place.dropOffItem(inventory.inInv);
        }

        //Destroy(inventory.inInv.gameObject);
        inventory.inInv = null;
    }

    public void assignBed(Bed _inBed)
    {
        Debug.Log("Assigned Bed");

        needs[0].restoreNeedFurn = _inBed;
    }
    
}
