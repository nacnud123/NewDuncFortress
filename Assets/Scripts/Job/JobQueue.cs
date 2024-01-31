using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JobQueue : MonoBehaviour
{
    public static JobQueue init;

    public SortedList<Job.JobPriority, Job> globalJobQueue = new SortedList<Job.JobPriority, Job>();
    public List<Job> waitingJobs = new List<Job>();

    private void Awake()
    {
        init = this;
        globalJobQueue = new SortedList<Job.JobPriority, Job>(new DuplicateKeyComparer<Job.JobPriority>(true));
    }

    public void Enqueue(Job job)
    {
        Debug.Log($"Enqueue {job.name}");
        globalJobQueue.Add(job.priority, job);
    }

    public Job Dequeue()
    {
        if(globalJobQueue.Count == 0)
        {
            return null;
        }

        Job job = globalJobQueue.Values[0];
        globalJobQueue.RemoveAt(0);
        return job;
    }
}
