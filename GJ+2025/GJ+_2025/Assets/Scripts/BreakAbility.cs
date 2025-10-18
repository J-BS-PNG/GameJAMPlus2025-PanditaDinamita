using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAbility : MonoBehaviour, IAbility
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
            Debug.Log("No quedan usos para el romper");
            return;
        }

        currentUses--;
        Debug.Log($"Usando romper ({currentUses}/{maxUses}) restante");
    }

    public bool canUse()
    {
        return currentUses > 0;
    }

    public int remainingUses()
    {
        return currentUses;
    }

    public string GetName() => "Romper";


}
