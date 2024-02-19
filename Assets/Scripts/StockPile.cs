using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;
using System;

public class StockPile : Entity
{
    public Inventory inventory;
    public SpriteRenderer displaySpr;

    public bool isFull = false;



    public override bool acceptsResource(int resourceID)
    {
        return true;
    }

    public void dropOffItem(Entity item)
    {
        /*SpriteRenderer itemSR = item.GetComponent<SpriteRenderer>();
        displaySpr.sprite = itemSR.sprite;
        displaySpr.color = itemSR.color;*/
        isFull = true;
        inventory.inInv = item;
    }

    /*private void fillArray()
    {
        for (int x = 0; x < currSlots.GetLength(0); x++)
        {
            for (int y = 0; y < currSlots.GetLength(1); y++)
            {
                currSlots[x, y] = new Slot();
            }
        }
    }*/
}
