using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPile : Entity
{

    public override void tick()
    {
        Person person = getRandomPerson(50, 50, true);
        if(person != null && person.job == null)
        {
            person.setJob(new Gather(GameManager.init.WOOD, this));
        }
    }

    public override bool acceptsResource(int resourceID)
    {
        return true;
    }

    private Person getRandomPerson(float r, float s, bool mustBeFree)
    {
        TargetFilter peonFilter = new TargetFilter
        {
            Accepts = e => e.alive && (e is Person) && (!mustBeFree || ((Person)e).job == null)
        };

        Entity e = getRandomTarget(r, s, peonFilter);
        if(e is Person)
        {
            Person person = (Person)e;
            return person;
        }
        return null;

    }
}
