using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Furniture : Entity
{
    public string entityName = "";
    public int buildTime = 20;
    public int currBuildTime = 0;

    public string type;

    public bool built = false;

    public SpriteRenderer sr;

    public float beautyVal;


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

            currNode.parentGameNode.tileFurniture = this;

            GridManager.init.rebuildObsticalList();


            return true;
        }
        return false;
    }

    public virtual void godModeBuild()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 100);
        built = true;
        this.transform.tag = "Obstacle";

        currNode.parentGameNode.tileFurniture = this;

        GridManager.init.rebuildObsticalList();
    }

    public virtual void destroy()
    {
        currNode.parentGameNode.tileFurniture = null;
        this.transform.tag = "Untagged";
        GridManager.init.rebuildObsticalList();

        Destroy(this.gameObject);
    }


}
