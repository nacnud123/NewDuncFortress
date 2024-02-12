using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Furniture : Entity
{
    public string entityName = "";
    public int buildTime = 20;
    public int currBuildTime = 0;

    public bool built = false;

    public SpriteRenderer sr;
    public Sprite displaySpritre;


    public Furniture(string _name = "", int _buildTime = 20)
    {
        entityName = _name;
        buildTime = _buildTime;
    }

    public virtual bool build()
    {
        currBuildTime += 1;
        if(currBuildTime >= buildTime)
        {
            built = true;
            this.transform.tag = "Obstacle";

            GridManager.init.rebuildObsticalList();

            return true;
        }
        return false;
    }


}
