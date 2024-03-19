using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture
{
    public Person assignedPerson;

    public override void Start()
    {
        base.Start();
        JobQueue.init.Enqueue(new Build(0, this));
        //this.godModeBuild();
        //this.build();
    }

    public override bool build()
    {
        // TODO: Once build assign the bed to someone
        if (base.build())
        {
            Debug.Log("Bed built, starting room update");
            currNode.currRoom.updateRoom();

            TargetFilter personFilter = new TargetFilter
            {
                Accepts = e => e.gameObject.GetComponent<Person>() && e.alive
            };

            Person e = (Person)GameManager.init.findClosestEntity(this, this, personFilter);

            e.assignBed(this);
            return true;
        }

        return false;

    }

    public override void godModeBuild()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
        built = true;

        currNode.parentGameNode.tileFurniture = this;

        Debug.Log("Bed built, starting room update");
        currNode.currRoom.updateRoom();

        TargetFilter personFilter = new TargetFilter
        {
            Accepts = e => e.gameObject.GetComponent<Person>() && e.alive
        };

        Person e = (Person)GameManager.init.findClosestEntity(this, this, personFilter);

        e.assignBed(this);


    }


}
