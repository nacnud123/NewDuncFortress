using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Person : Entity
{
    public float wanderTime = 0;
    public State job;
    public float moveTick = 0;

    public Inventory inventory;

    [SerializeField] private int hp = 100;
    private int maxHp = 100;
    /*private int xp = 0;
    private int nextLevel = 1;
    private int level = 0;*/
    //public float rot = 0;
   

    public float speed = 2f;

    private void Awake()
    {
        r = .1f;
        moveTick = Random.Range(0, 13);
    }

    public override void tick()
    {
        currNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(this.transform.position)));
        //person.currNode = GridManager.init.getNodeFromVec3(person.transform.position);
        // Above for some reson causes a memory leak and the game to crash.

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
                Debug.Log("No Job!");
                setJob(new Wander());
            }
           
        }

    }

    public void setJob(State job)
    {
        this.job = job;
        if (job != null) job.init(this);
    }

    public void pickUpItem(Resource newItem)
    {
        if(!inventory.addItem(newItem))
        {
            return;
        }
        //newItem.transform.parent = this.transform;
        //newItem.transform.position = this.transform.position;
    }

    public void dropOffItem(Inventory place = null) // = null may be redundent
    {
        Debug.Log("Person DropOffItem!");
        if (place != null)
        {
            place.addItem(this.inventory);
        }
    }

    public void dropItem(Resource specificItem = null)
    {
        inventory.dropItem(specificItem);
        inventory.sr.sprite = null;
    }
    
}
