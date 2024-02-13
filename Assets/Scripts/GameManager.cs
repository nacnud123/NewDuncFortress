using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager init;


    public enum ITEMTYPE
    {
        WOOD = 0,
        ROCK = 1,
        FOOD = 2
    };

    public int woodAvail = 0;

    public List<Entity> entities = new List<Entity>();
    public List<GameObject> cropObj = new List<GameObject>();

    public Dictionary<int, int>  maxPileSize = new Dictionary<int, int>()
    {
        {(int)ITEMTYPE.WOOD, 2 },
        {(int)ITEMTYPE.ROCK, 2 },
        {(int)ITEMTYPE.FOOD, 2 }
    };



    private void Awake()
    {
        init = this;
        //var temp = FindObjectsOfType(typeof(Entity)) as Entity[];
        //entities = new List<Entity>(temp);
    }

    private void Update()
    {
        for (int i = 0; i < entities.Count; i++) // TODO: Figure out tick system
        {
            Entity e = entities[i];
            e.tick();
            if (!e.alive)
            {
                entities.RemoveAt(i--);
                Destroy(e.gameObject);
            }
        }
    }

    //Function that returns if there are open stockpiles
    public bool areOpenStockPiles()
    {
        var temp = FindObjectsOfType(typeof(StockPile)) as StockPile[];
        foreach(StockPile s in temp)
        {
            if (!s.inventory.checkIfFull()) { return true; }
        }
        return false;
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
}
