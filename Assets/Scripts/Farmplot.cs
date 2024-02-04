using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmplot : Entity
{
    public Inv cropInv;
    public SpriteRenderer displaySR;

    public bool isFull = false;

    public override void Start()
    {
        base.Start();
        JobQueue.init.Enqueue(new Farming(0, this));
    }

    /*public override bool acceptsResource(int resourceID)
    {
        return true;
    }*/

    public void plantCrop(Entity crop)
    {
        var tempCrop = Instantiate(crop.gameObject);
        displaySR.sprite = crop.GetComponent<SpriteRenderer>().sprite;
        displaySR.color = crop.GetComponent<SpriteRenderer>().color;
        tempCrop.transform.parent = displaySR.transform;
        tempCrop.transform.position = displaySR.transform.position;
        tempCrop.SetActive(true);

        tempCrop.GetComponent<Crop>().inPlot = this;

        isFull = true;
        cropInv.inInv = crop;
    }

    public void takePlant()
    {
        displaySR.sprite = null;

        isFull = false;
        cropInv.inInv = null;
    }
}
