using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingButton : MonoBehaviour
{
    public Person person;
    public GameObject target;
    public GameObject gotoTarget;

    public void onButtonClick()
    {
        JobQueue.init.Enqueue(new Build(0, target.GetComponent<Entity>()));
    }

}
