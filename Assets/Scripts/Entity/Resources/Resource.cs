using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Entity
{
    public bool waiting = false;
    public int position = 0;

    public bool claimed = false;

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

        //Once made check to see if there is a stockpile that will accept this item


        JobQueue.init.Enqueue(new Haul((int)itemType, this));

    }

    

    /*private void FixedUpdate()
    {
        if (GameManager.init.areOpenStockPiles() && waiting == true)
        {
            JobQueue.init.Enqueue(JobQueue.init.waitingJobs[position]);
            JobQueue.init.waitingJobs.RemoveAt(position);
            waiting = false;
        }
    }*/
}
