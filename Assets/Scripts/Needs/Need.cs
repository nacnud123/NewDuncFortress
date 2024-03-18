using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Need
{
	#region Private vars
	private float amount = 0;
	private bool highToLow = true;
	#endregion
	
	#region public Vars gets and sets
	public string Name {get; set;}
	
	public string Type {get; set;}
	
	public Person person {get; set;}
	
	public float restoreNeedAmount {get; set;}
	
	public float growthRate {get; set;}
	
	public float Damage {get; set;}
	
	public Furniture restoreNeedFurn {get; set;}
	
	public float restoreNeedTime {get; set;}

	public bool inQueue = false;

	public bool beingWorkedOn = false;

	public float Amount
	{
		get
		{
			return amount;
		}
		set
		{
			amount = Mathf.Clamp(value, 0f, 100f);
		}
	}
	public string displayAmount{
		get
		{
			if(highToLow)
			{
				return (100 - ((int)Amount)) + "%";
			}
			
			return ((int)Amount) + "%";
		}
	}
	
	#endregion
	
	#region Constructors
	public Need()
	{
		Amount = 0;
		restoreNeedAmount = 100;
		Name = "Null";
	}
	#endregion

	public void Update()
    {
		if(beingWorkedOn == false)
        {
			Amount += this.growthRate * Time.deltaTime;
		}
		
    }
}