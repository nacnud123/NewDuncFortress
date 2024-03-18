using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class WaitingJob
{
	public Job thisJob;
	
	public System.Func<bool> updateAction;
	
	public WaitingJob(Job _inJob)
	{
		thisJob = _inJob;
	}
	
	public void jobCheck()
	{
        if (updateAction())
        {
			JobQueue.init.Enqueue(thisJob);
			JobQueue.init.waitingJobs.Remove(this);
        }
	}
}