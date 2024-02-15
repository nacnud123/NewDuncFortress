using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JobQueue : MonoBehaviour
{
    public static JobQueue init;

    public SortedList<State.JobPriority, State> globalJobQueue = new SortedList<State.JobPriority, State>();
    [SerializeField] public List<State> waitingJobs = new List<State>();

    private void Awake()
    {
        init = this;
        globalJobQueue = new SortedList<State.JobPriority, State>(new DuplicateKeyComparer<State.JobPriority>(true));
    }

    public void Enqueue(State job)
    {
        Debug.Log($"Enqueue {job.name}");
        globalJobQueue.Add(job.priority, job);
    }

    public State Dequeue()
    {
        if(globalJobQueue.Count == 0)
        {
            return null;
        }

        State job = globalJobQueue.Values[0];
        globalJobQueue.RemoveAt(0);
        return job;
    }
}
