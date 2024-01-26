using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFilter
{
    public System.Func<Entity, bool> Accepts { get; set; }
}