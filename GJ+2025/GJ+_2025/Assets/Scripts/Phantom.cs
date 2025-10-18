using System.Collections;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    //Movimiento
    public float speed = 2f;

    //Persecución del jugador
    public float followRange = 5f;
    public Transform player;
    public bool followPlayer = true; 

    //Movimiento aleatorio
    public float wanderRadius = 10f;
    public float wanderChangeTime = 3f;

    //Waypoints 
    public bool useWaypoints = false;
    public Transform[] waypoints;
    private int currentWaypoint = 0;


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
    
    void FollowWaypoints()
    {
        if (waypoints.Length == 0) return;

        Transform targetPoint = waypoints[currentWaypoint];
        Vector2 direction = ((Vector2)targetPoint.position - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Cuando llega cerca, pasa al siguiente
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void PickNewRandomTarget()
    {
        // Genera un punto aleatorio dentro del área de wanderRadius
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        randomTarget = (Vector2)transform.position + randomCircle;
        nextChangeTime = Time.time + wanderChangeTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);

        Gizmos.color = Color.green;
        if (waypoints != null && waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoints[i] != null)
                {
                    Gizmos.DrawSphere(waypoints[i].position, 0.15f);
                    if (i + 1 < waypoints.Length && waypoints[i + 1] != null)
                        Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
