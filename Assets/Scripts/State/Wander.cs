using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Wander : Job
{
    public float totalIdelTime = 0;
    private float timeSpendIdle;
    public Wander()
    {
        name = "Wander";
    }


    public override void init(Person _person)
    {
        base.init(_person);
        timeSpendIdle = 0f;
        totalIdelTime = Random.Range(.2f, 2.0f);
    }

    public override void tick()
    {
        Debug.Log("Wander Tick!");
        Random.seed = System.DateTime.Now.Millisecond;

        timeSpendIdle += Time.deltaTime;
        if (timeSpendIdle >= totalIdelTime)
        {
            ArrayList neighbors = new ArrayList();
            GridManager.init.GetNeighbours(person.currNode, neighbors);

            Node endNode = (Node)neighbors[Random.Range(0, neighbors.Count)];
            person.setJob(new Move(endNode));
        }

    }
}
