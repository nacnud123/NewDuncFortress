using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Move : State
{
    private Node endNode;
    private Node realEndNode;

    //TODO: Comment steps
    public Move(Node end, State _nextJob = null) : base("Move", _nextJob)
    {
        endNode = end;
    }

    public override void init(Person _person)
    {
        Debug.Log("Move init!");
        base.init(_person);

        pathArray = AStar.FindPath(person.currNode, endNode);
        nextPos = ((Node)pathArray[0]).position;
    }

    public override void tick()
    {
        //Debug.Log("Move Tick!");
        if(pathArray.Count > 2)
        {
            realEndNode = ((Node)pathArray[pathArray.Count - 2]);
        }
        else // Just used for wander state.
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
        //person.currNode = GridManager.init.getNodeFromVec3(person.transform.position);
        if (nextJob != null)
        {
            nextJob.isAtLoc = true;
            person.setJob(nextJob);
        }
        else
        {
            person.setJob(null);
        }

    }

}
