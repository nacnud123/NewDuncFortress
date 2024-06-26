using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager init;

    public const int WOOD = 0;
    public const int ROCK = 1;
    public const int FOOD = 2;

    public int woodAvail = 0;

    public List<Entity> entities = new List<Entity>();

    public GameObject gameNodeObj;

    public int tick = 0;

    [Header("Ui Stuff")]
    public GameObject selectedPlayer;
    public TMP_Text personNameText;
    public TMP_Text personMoodText;
    public TMP_Text personJobText;

    private void Awake()
    {
        init = this;
        //var temp = FindObjectsOfType(typeof(Entity)) as Entity[];
        //entities = new List<Entity>(temp);
    }

    private void Update()
    {
        for (int i = 0; i < entities.Count; i++) // Figure out tick system
        {
            Entity e = entities[i];
            e.tick();
            if (!e.alive)
            {
                entities.RemoveAt(i--);
                Destroy(e.gameObject);
            }
        }
        tick += 1;
        if(tick % 2500 == 0)
        {
            for(int i = 0; i < entities.Count; i++)
            {
                if (entities[i].GetComponent<Person>())
                {
                    entities[i].GetComponent<Person>().updateMood();
                }
            }
        }

        for(int i =0; i < JobQueue.init.waitingJobs.Count; i++)
        {
            JobQueue.init.waitingJobs[i].jobCheck();
        }

        if(selectedPlayer != null)
        {
            personNameText.text = $"Name: {selectedPlayer.GetComponent<Person>().personName}";
            personMoodText.text = $"Mood: {selectedPlayer.GetComponent<Person>().mood}%";
            personJobText.text = $"Job: {selectedPlayer.GetComponent<Person>().job.ToString()}";
        }
        else
        {
            personNameText.text = $"Name: ";
            personMoodText.text = $"Mood: ";
            personJobText.text = $"Job: ";
        }


    }

    //Function that returns if there are open stockpiles
   /*public bool areOpenStockPiles()
    {
        var temp = FindObjectsOfType(typeof(StockPile)) as StockPile[];
        foreach(StockPile s in temp)
        {
            if (!s.isFull) { return true; }
        }
        return false;
    }*/


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


    public bool isInvWithItem(int id)
    {
        Inventory[] gameInv = FindObjectsOfType(typeof(Inventory)) as Inventory[];
        foreach(Inventory inv in gameInv)
        {
            if (inv.givesResources(id) && inv.isStockpile)
            {
                return true;
            }
        }
        return false;
    }

    public bool areOpenInv()
    {
        Inventory[] gameInv = FindObjectsOfType(typeof(Inventory)) as Inventory[];
        foreach (Inventory inv in gameInv)
        {
            if (!inv.isFull())
            {
                return true;
            }
        }
        return false;
    }

    public bool areOpenStockpile()
    {
        Inventory[] gameInv = FindObjectsOfType(typeof(Inventory)) as Inventory[];
        foreach (Inventory inv in gameInv)
        {
            if (!inv.isFull() && inv.isStockpile)
            {
                return true;
            }
        }
        return false;
    }
}
