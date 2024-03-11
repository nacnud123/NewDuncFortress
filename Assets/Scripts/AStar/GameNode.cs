using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class GameNode : MonoBehaviour
{
    public Inventory tileInv;
    public Furniture tileFurniture = null;
    public Node currNode;

    public bool seen = false;

    public SpriteRenderer sr;
}
