using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Move : Job
{
    private Job lastJob;
    private Node endNode;
    private Node realEndNode;

    public override void init(Person _person)
    {
        base.init(_person);

        pathArray = AStar.FindPath(person.currNode, endNode);
        nextPos = ((Node)pathArray[0]).position;
    }

    public Move(Job _lastJob)
    {
        name = "Move";
        lastJob = _lastJob;
        endNode = _lastJob.jobNode;
    }

    public Move(Node _endNode)
    {
        name = "Move";
        endNode = _endNode;
    }

    public override void tick()
    {
        Debug.Log("Move Tick!");
        if(pathArray.Count > 2)
        {
            realEndNode = ((Node)pathArray[pathArray.Count - 2]);
        }
        else
        {
            realEndNode = ((Node)pathArray[pathArray.Count - 1]);
        }


        if (person.transform.position == realEndNode.position)
        {
            this.arrived();
        }
        else
        {
            if (person.transform.position == nextPos)
            {
                if (index <= pathArray.Count)
                {
                    nextPos = ((Node)pathArray[index]).position;

                    person.transform.position = Vector3.MoveTowards(person.transform.position, nextPos, person.speed * Time.deltaTime);
                    index += 1;
                }
            }
            else
            {
                person.transform.position = Vector3.MoveTowards(person.transform.position, nextPos, person.speed * Time.deltaTime);
            }
        }

    }

    public override void arrived()
    {
        if (lastJob != null)
        {
            lastJob.isAtLoc = true;
            person.setJob(lastJob);
        }
        else
        {
            person.setJob(null);
        }

    }

}
