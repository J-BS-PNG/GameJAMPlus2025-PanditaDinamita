using System.Collections;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    public float speed = 2f;
    public float followRange = 5f;
    public float wanderRadius = 10f;
    public float wanderChangeTime = 3f;
    public Transform player;
    public bool followPlayer = true; // puedes activar/desactivar esto en el inspector

    private Vector2 randomTarget;
    private float nextChangeTime;

    void Start()
    {
        PickNewRandomTarget();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (followPlayer && distanceToPlayer < followRange)
        {
            FollowPlayer();
        }
        else
        {
            Wander();
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void Wander()
    {
        // Cambia de dirección cada cierto tiempo
        if (Time.time > nextChangeTime || Vector2.Distance(transform.position, randomTarget) < 0.5f)
        {
            PickNewRandomTarget();
        }

        Vector2 direction = (randomTarget - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void PickNewRandomTarget()
    {
        // Genera un punto aleatorio dentro del área de wanderRadius
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        randomTarget = (Vector2)transform.position + randomCircle;
        nextChangeTime = Time.time + wanderChangeTime;
    }
}
