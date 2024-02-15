using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Wander : State
{
    public float totalIdelTime = 0;
    private float timeSpendIdle;
    public Wander(State _nextJob = null): base("Wander", _nextJob)
    {
        timeSpendIdle = 0f;
        totalIdelTime = Random.Range(.2f, 1.5f);
    }


    public override void init(Person _person)
    {
        base.init(_person);
    }

    public override void tick()
    {
        
        Random.seed = System.DateTime.Now.Millisecond;

        timeSpendIdle += Time.deltaTime;
        if (timeSpendIdle >= totalIdelTime)
        {
            ArrayList neighbors = new ArrayList();
            GridManager.init.GetNeighbours(person.currNode, neighbors);

            Node endNode = (Node)neighbors[Random.Range(0, neighbors.Count)];

            //Debug.Log("Wander Tick!");

            person.setJob(new Move(endNode));
        }

    }
}
