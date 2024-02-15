using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Job 
{
    public enum JobPriority // Job Priority stuff
    {
        High,
        Medium,
        Low
    }


    public JobPriority priority;

    public Node jobNode;
    public string name { get; protected set; }

    public bool acceptAny = false;

    public Entity parentEntity;

    Dictionary<Resource, int> requestItems = new Dictionary<Resource, int>();
    Dictionary<Resource, int> delivedItems = new Dictionary<Resource, int>();


    public int currJobTime { get; set; }
    public int jobTime { get; set; }

    public bool isBeingWorked { get; set; }

    public Job(Node _jobNode, Dictionary<Resource, int> _requestItems, Entity _parentEntity)
    {
        jobNode = _jobNode;
        requestItems = _requestItems;
        parentEntity = _parentEntity;
    }

    public bool hasAllItem()
    {
        if (requestItems == null)
            return true;

        foreach(Resource item in requestItems.Keys)
        {
            if (delivedItems.ContainsKey(item) == false || delivedItems[item] >= requestItems[item])
                return true;
        }

        return false;
    }

    public void doWork()
    {
        parentEntity.GetComponent<Furniture>().build(); // TODO: change so people can work at workbenches or something
    }

}
