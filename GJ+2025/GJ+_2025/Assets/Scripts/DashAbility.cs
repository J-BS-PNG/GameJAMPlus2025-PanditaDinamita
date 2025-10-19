using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour, IAbility
{
    [SerializeField] private int maxUses = 3;
    private int currentUses;

    [SerializeField] private float dashDistnace = 5f;
    [SerializeField] private float dashDuration = 0.2f;

    private Movement player;

    public bool isDashing { get; private set; }

    void Start() 
    {
        player = GetComponent<Movement>();
        currentUses = maxUses;
    }

    public void UseAbility()
    {
        if (!canUse()) 
        {
            Debug.Log("No quedan usos para el Dash");
            return;
        }

        if (isDashing || player == null) 
        {
            Debug.Log("Ya estas haciendo dash");
            return;

        }

        currentUses--;
        player.StartCoroutine(DashCoroutine());
        //Debug.Log($"Usando Dash ({currentUses}/{maxUses}) restante");
    }

    private IEnumerator DashCoroutine() 
    {
        isDashing = true;

        Vector2 dir = DirectionToVector(player.GetCurrentDirection());
        Vector2 start = player.transform.position;
        Vector2 end = start + dir * dashDistnace;

        float elapse = 0f;
        while(elapse < dashDuration) 
        {
            player.transform.position = Vector2.Lerp(start, end, elapse / dashDuration);
            elapse += dashDuration;
            yield return null;
        }

        //player.transform.position = end;
        player.SetPosition(end);
        isDashing = false;
        //isDashing = true;

        //Rigidbody2D rb = GetComponent<Rigidbody2D>();


        //if(rb == null) 
        //{
        //    Debug.LogError("DashAbility requiere un Rigidbody2D en el jugador");
        //    yield break;
        //}

        //Vector2 dashDirection = transform.right;
        //float elapsep = 0f;

        //while (elapsep < dashDuration)
        //{
        //    rb.velocity = dashDirection * (dashDistnace / dashDuration);
        //    elapsep += Time.deltaTime;
        //    yield return null;
        //}

        //rb.velocity = Vector2.zero;
        //isDashing = false;

        Debug.Log($"Dash usado ({currentUses}/{maxUses} restante)");
    }

    private Vector2 DirectionToVector(Direction _direction) 
    {
        switch (_direction) 
        {
            case Direction.up: return Vector2.up;
            case Direction.down: return Vector2.down;
            case Direction.left: return Vector2.left;
            case Direction.right: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    public bool canUse() 
    {
        return currentUses > 0 && !isDashing;
    }

    public int remainingUses() 
    {
        return currentUses;
    }

    public string GetName() => "Dash";
}
