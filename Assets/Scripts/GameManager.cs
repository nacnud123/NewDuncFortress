using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager init;

    public const int WOOD = 0;
    public const int ROCK = 1;
    public const int FOOD = 2;

    public int woodAvail = 0;

    public float timeMaxAmount = 10;
    public float timer = 0f;

    public List<Entity> entities = new List<Entity>();

    private void Awake()
    {
        init = this;
        var temp = FindObjectsOfType(typeof(Entity)) as Entity[];
        entities = new List<Entity>(temp);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeMaxAmount)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                Entity e = entities[i];
                e.tick();
                if (!e.alive)
                {
                    entities.RemoveAt(i--);
                    Destroy(e.gameObject);
                }
            }
            timer = 0;
        }
    }

   

    public bool personAccept(Entity e, bool beFree)
    {
        return e.alive && (e.gameObject.GetComponent<Person>()) && (!beFree || ((Person)e).job == null);
    }

    public Entity getEntityAt(float x, float y, float r, Entity exception, TargetFilter filter = null)
    {
        float closest = 10000000;
        Entity closestEntity = null;

        for (int i = 0; i < entities.Count; i++)
        {
            Entity e = entities[i];
            if (e == exception) continue;
            if (filter != null && !filter.Accepts(e)) continue;
            if (e.collides(x, y, r))
            {
                float dist = (e.x - x) * (e.x - x) + (e.y - y) * (e.y - y);
                if (dist < closest)
                {
                    closest = dist;
                    closestEntity = e;
                }
            }

        }
        return closestEntity;
    }

    public Entity findClosestEntity(Entity inE, Entity exception = null, TargetFilter filter = null)
    {
        float closest = 10000000;
        Entity closestEntity = null;
        List<Entity> nearEntitys = new List<Entity>();

        for (int i = 0; i < entities.Count; i++)
        {
            Entity e = entities[i];
            if (e == exception) continue;
            if (filter != null && !filter.Accepts(e)) continue;
            float dist = Vector3.Distance(inE.gameObject.transform.position, e.transform.position);
            if(dist < closest)
            {
                closestEntity = e;
                closest = dist;
            }

        }
        return closestEntity;
    }

    public Entity getEntityAtPos(Vector3 pos)
    {
        foreach(Entity e in entities)
        {
            if (e.transform.position == pos)
                return e;
        }
        return null;
    }

    public bool isFree(float x, float y, float r, Entity source)
    {
        for(int i = 0; i< entities.Count; i++)
        {
            Entity e = entities[i];
            if(e != source)
            {
                if (e.collides(x, y, r)) return false;
            }
        }
        return true;
    }
}
