using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Furniture
{
    public override void Start()
    {
        base.Start();

        JobQueue.init.Enqueue(new Build(0, this));

    }
}
