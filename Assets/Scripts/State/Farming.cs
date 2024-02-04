using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Farming : Job
{
    public int cropID = 0;
    public bool cutPlants = false;

    public Farming(int _cropId, Entity _target, Job _nextJob = null):base("Farming", _nextJob)
    {
        priority = JobPriority.Medium;
        cropID = _cropId;
        target = _target;
        jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position)));
    }

    public override void init(Person _person)
    {
        base.init(_person);
    }

    public override void tick()
    {
        Debug.Log("Farming Tick!");
        if(isAtLoc == false && target != null)
        {
            jobNode = new Node(GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(target.transform.position))); // Maybe redundent
            person.setJob(new Move(jobNode, this)); // 1. Go to farmplot
        }
        if (isAtLoc)
        {
            this.arrived();
        }
    }

    public override void arrived()
    {
        if (isAtLoc)
        {
            //2. Now at farmplot, plant the crop
            ((Farmplot)target).plantCrop(GameManager.init.cropObj[cropID].GetComponent<Entity>()); // Stupid way

            Finished();
            return;
        }
    }
}
