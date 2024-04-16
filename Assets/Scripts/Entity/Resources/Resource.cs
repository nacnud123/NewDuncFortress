using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : Entity
{
    public bool claimed = false;

    public int maxItemStack;
    public TMP_Text stackDisplay;
    public int currentStackSize = 1;

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

    public int addToStack(int _amm)
    {
        for(int i =0; i < _amm; i++)
        {
            currentStackSize += 1;
            if(currentStackSize > maxItemStack)
            {
                return _amm - maxItemStack;
            }
        }
        return -1;
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
