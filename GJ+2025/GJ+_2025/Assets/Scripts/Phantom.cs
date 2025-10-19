using System.Collections;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    // Movimiento
    public float speed = 2f;

    // Persecución del jugador
    public float followRange = 5f;
    public Transform player;
    public bool followPlayer = true;

    // Movimiento aleatorio
    public float wanderRadius = 10f;
    public float wanderChangeTime = 3f;

    // Waypoints
    public bool useWaypoints = false;
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    // Extras
    private Vector2 randomTarget;
    private float nextChangeTime;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        PickNewRandomTarget();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (followPlayer && distanceToPlayer < followRange)
        {
            FollowPlayer();
        }
        else if (useWaypoints && waypoints.Length > 0)
        {
            FollowWaypoints();
        }
        else
        {
            Wander();
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        MoveAndFlip(direction);
    }

    void Wander()
    {
        if (Time.time > nextChangeTime || Vector2.Distance(transform.position, randomTarget) < 0.5f)
        {
            PickNewRandomTarget();
        }

        Vector2 direction = (randomTarget - (Vector2)transform.position).normalized;
        MoveAndFlip(direction);
    }

    void FollowWaypoints()
    {
        if (waypoints.Length == 0) return;

        Transform targetPoint = waypoints[currentWaypoint];
        Vector2 direction = ((Vector2)targetPoint.position - (Vector2)transform.position).normalized;
        MoveAndFlip(direction);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void MoveAndFlip(Vector2 direction)
    {
        // Movimiento
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Flip del sprite (solo si se mueve horizontalmente)
        if (direction.x != 0)
            spriteRenderer.flipX = direction.x > 0;

        // Activar animación si existe
        if (animator != null)
            animator.SetBool("isMoving", direction.magnitude > 0.1f);
    }

    void PickNewRandomTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        randomTarget = (Vector2)transform.position + randomCircle;
        nextChangeTime = Time.time + wanderChangeTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}
