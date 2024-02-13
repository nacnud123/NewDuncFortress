using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;
using System;

public class StockPile : Entity
{
    public Inventory inventory;
    public SpriteRenderer displaySpr;

    public override bool acceptsResource(int resourceID)
    {
        return true;
    }

}
