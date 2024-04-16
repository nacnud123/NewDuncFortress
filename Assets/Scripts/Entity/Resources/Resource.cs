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
    public bool needsJob = false;

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

        stackDisplay.text = $"x{currentStackSize}";

    }

    public int addToStack(Resource addItem)
    {
        int ammAdded = 0;
        for(int i =0; i < addItem.currentStackSize; i++)
        {
            currentStackSize += 1;
            if (currentStackSize >= maxItemStack)
            {
                currentStackSize = maxItemStack;
                updateDisplay();
                addItem.currentStackSize -= ammAdded;
                addItem.updateDisplay();
                needsJob = true;
                return 0;
            }

            ammAdded += 1;

        }
        updateDisplay();
        addItem.currentStackSize = 0;
        return -1;
    }

    private void updateDisplay()
    {
        stackDisplay.text = $"x{currentStackSize}";
    }
    
    public void addHaulJob()
    {
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
