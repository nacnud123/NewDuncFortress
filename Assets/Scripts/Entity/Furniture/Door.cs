using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Furniture
{
    public override void Start()
    {
        base.Start();
        //this.godModeBuild();
        JobQueue.init.Enqueue(new Build(0, this));

    }

    public override bool build()
    {
        // TODO: Once build assign the bed to someone
        if (base.build())
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
            built = true;

            currNode.parentGameNode.tileFurniture = this;
            return true;
        }

        return false;

    }

    public override void godModeBuild()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
        built = true;

        currNode.parentGameNode.tileFurniture = this;
    }


}
