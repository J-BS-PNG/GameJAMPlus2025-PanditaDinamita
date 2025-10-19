using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAbility : MonoBehaviour, IAbility
{
    [SerializeField] private int maxUses = 3;
    private int currentUses;
    private GameObject objectoCercano;

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

        if(objectoCercano != null) 
        {
            Destroy(objectoCercano);
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

    void OnCollisionEnter2D(Collision2D other)
    {


        if (other.gameObject.CompareTag("ObjectoB"))
        {
            objectoCercano = other.gameObject;
            Debug.Log("Se rompio");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ObjectoB"))
        {
            objectoCercano = null;
            Debug.Log("Salio de habilidad");
        }
    }


}
