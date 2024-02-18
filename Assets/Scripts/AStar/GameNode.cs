using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class GameNode : MonoBehaviour
{
    public Node currNode;
    public Inventory tileInv;
    public bool isStockpile = false;

    public void init()
    {
        this.tileInv = GetComponent<Inventory>();
    }



}
