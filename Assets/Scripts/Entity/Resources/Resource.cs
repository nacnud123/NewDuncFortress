using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Entity
{
    public bool waiting = false;
    public int position = 0;

    public enum ItemType
    {
        WOOD = GameManager.WOOD,
        ROCK = GameManager.ROCK,
        FOOD = GameManager.FOOD
    }

    public ItemType itemType;

    public override void Start()
    {
        base.Start();

        if (GameManager.init.areOpenStockPiles())
        {
            JobQueue.init.Enqueue(new Haul((int)itemType, this));
        }
        else
        {
            JobQueue.init.waitingJobs.Add(new Haul((int)itemType, this));
            waiting = true;
            position = JobQueue.init.waitingJobs.Count - 1;
        }
        
    }

    private void FixedUpdate()
    {
        if (GameManager.init.areOpenStockPiles() && waiting == true)
        {
            JobQueue.init.Enqueue(JobQueue.init.waitingJobs[position]);
            JobQueue.init.waitingJobs.RemoveAt(position);
            waiting = false;
        }
    }
}
