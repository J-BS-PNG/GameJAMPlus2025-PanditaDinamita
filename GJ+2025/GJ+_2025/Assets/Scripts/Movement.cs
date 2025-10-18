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
    public List<MonoBehaviour> componenteHabilidad = new List<MonoBehaviour>();
    private List<IAbility> habilidades = new List<IAbility>();

    private IAbility habilidadadActiva;
    private bool habilidadActiva;

    Vector3 targetPosition;
    Direction direction;
    public float speed = 5f;
    public int maxHealth = 3;
    private int currentHealth;


    private void Awake()
    {
        //foreach(var comp in componenteHabilidad) 
        //{
        //    if(comp is IAbility habilidad) 
        //    {
        //        habilidades.Add(habilidad);
        //    }
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        currentHealth = maxHealth;
        direction = Direction.down; 

        habilidades.AddRange(GetComponents<IAbility>());

        if(habilidades.Count > 0) 
        {
            habilidadadActiva = habilidades[0];
            Debug.Log("Habilidad Activa incial: " + habilidadadActiva.GetName());
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) SetActiveAbility(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetActiveAbility(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetActiveAbility(2);

        if (Input.GetKeyDown(KeyCode.Q) && habilidadadActiva != null) 
        {
            habilidadadActiva.UseAbility();
        }
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
    private void SetActiveAbility(int index) 
    {
        if(index >= 0 && index < habilidades.Count) 
        {
            habilidadadActiva = habilidades[index];
            Debug.Log("Habilidad Activa: " + habilidadadActiva.GetName());
        }
    }

}

        

