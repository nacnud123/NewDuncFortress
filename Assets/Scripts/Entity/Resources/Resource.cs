using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Entity
{
    public enum ItemType
    {
        WOOD = GameManager.WOOD,
        ROCK = GameManager.ROCK,
        FOOD = GameManager.FOOD
    }

    public ItemType itemType;

    private void Awake()
    {
        JobQueue.init.Enqueue(new Haul((int)itemType, this));
    }
}
