using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    up,
    down,
    left,
    right
}

public class Movement : MonoBehaviour
{
    Vector3 targetPosition;
    Direction direction;
    public float speed = 5f;
    public int maxHealth = 3;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        currentHealth = maxHealth;
        direction = Direction.down; 
    }

    // Update is called once per frame
    void Update()
    {

            // Mientras se mueve, ignora nueva entrada
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            return;
        }

        Vector2 axisDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (axisDirection != Vector2.zero)
        {
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    direction = Direction.right;
                    targetPosition += Vector3.right;
                }
                else
                {
                    direction = Direction.left;
                    targetPosition += Vector3.left;
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    direction = Direction.up;
                    targetPosition += Vector3.up;
                }
                else
                {
                    direction = Direction.down;
                    targetPosition += Vector3.down;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Character collided");
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1); 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Character took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Character has died.");
        }
    }
}

        

