using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    public void UseAbility()
    {
        Debug.Log("Dash objeto");
    }

    public string GetName() => "Dash";
}
