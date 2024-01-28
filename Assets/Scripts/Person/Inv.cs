using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv : MonoBehaviour
{
    public GameObject inHands;

    public void dropItem()
    {
        inHands.transform.parent = null;
    }
}
