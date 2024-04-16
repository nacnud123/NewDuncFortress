using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : MonoBehaviour
{
    public enum Jobs
    {
        Gather = 0,
        Destroy = 1
    }

    public static JobController init;

    private void Awake()
    {
        init = this;
    }

    public bool active = false;

    public Jobs jobSelected;

    public void changeJob(int jobNum)
    {
        if (jobNum == 0)
            jobSelected = Jobs.Gather;
        if (jobNum == 1)
            jobSelected = Jobs.Destroy;
    }
}
