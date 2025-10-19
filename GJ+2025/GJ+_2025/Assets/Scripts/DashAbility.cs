using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    [SerializeField] private int maxUses = 3;
    private int currentUses;

    void Start() 
    {
        currentUses = maxUses;
    }

    public void UseAbility()
    {
        if (!canUse()) 
        {
            Debug.Log("No quedan usos para el Dash");
            return;
        }

        currentUses--;
        Debug.Log($"Usando Dash ({currentUses}/{maxUses}) restante");
    }

    public bool canUse() 
    {
        return currentUses > 0;
    }

    public int remainingUses() 
    {
        return currentUses;
    }

    public string GetName() => "Dash";
}
