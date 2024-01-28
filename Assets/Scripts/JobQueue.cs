using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobQueue : MonoBehaviour
{
    public static JobQueue init;
    public List<Job> globalJobQueue = new List<Job>();
    public List<Job> waitingJobs = new List<Job>();

    private void Awake()
    {
        init = this;
    }

    public void Enqueue(Job job)
    {
        Debug.Log($"Enqueue {job.name}");
        globalJobQueue.Add(job);
    }

    public Job Dequeue()
    {
        if(globalJobQueue.Count == 0)
        {
            return null;
        }

        Job job = globalJobQueue[0];
        globalJobQueue.RemoveAt(0);
        return job;
    }
}
