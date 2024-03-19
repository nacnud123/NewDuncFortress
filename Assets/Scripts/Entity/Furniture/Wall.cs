using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Furniture
{
    public override void Start()
    {
        base.Start();
        //godModeBuild();

        JobQueue.init.Enqueue(new Build(0, this));

    }
}
