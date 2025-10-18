using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAbility : MonoBehaviour, IAbility
{
    //[SerializeField] private float breakRange = 2f;
    //[SerializeField] private LayerMask breakableLayer;

    public void UseAbility() 
    {
        Debug.Log("Romper objeto");
    }

    public string GetName() => "Romper";


}
