using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need full re-do on how I am doing this.
public class Crop : Entity
{
    public float age;
    public int growTime = 60;
    public int stamina = 100;
    public bool fullyGrown = false;

    private bool done = false;

    public Farmplot inPlot;
    public GameObject dropItem;

    public override void tick()
    {
        if(age < growTime)
        {
            age += Time.deltaTime;
        }
        else if(age >= growTime && !done)
        {
            JobQueue.init.Enqueue(new Gather(0, this)); // Need to change 0 to other number
            done = true;
        }
    }

    public override bool givesResources(int resourceID)
    {
        return true;
    }

    public override bool gatherResource(int resourceID)
    {
        stamina -= 5;
        if(stamina <= 0)
        {
            alive = false;
            Instantiate(dropItem, transform.position, Quaternion.identity);
            inPlot.takePlant();
            return true;
        }
        return false;
    }
}
