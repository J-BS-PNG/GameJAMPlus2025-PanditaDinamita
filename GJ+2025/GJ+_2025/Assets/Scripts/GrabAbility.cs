using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAbility : MonoBehaviour, IAbility
{
    public void UseAbility()
    {
        Debug.Log("Agarrar objeto");
    }

    public string GetName() => "Agarrar";
}
