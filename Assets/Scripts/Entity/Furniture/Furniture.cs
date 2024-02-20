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
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
            built = true;
            this.transform.tag = "Obstacle";

            GridManager.init.rebuildObsticalList();

            return true;
        }
        return false;
    }


}
