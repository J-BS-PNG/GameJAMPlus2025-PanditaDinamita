using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    public bool objectObtained;

    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public List<Image> hearts = new List<Image>();
    private Rigidbody2D rb;

    //public Sprite fullHeart;       // Corazón lleno
    //public Sprite emptyHeart; 

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        currentHealth = maxHealth;
        direction = Direction.down; 



        habilidades.AddRange(GetComponents<IAbility>());
        rb = GetComponent<Rigidbody2D>();

        if (habilidades.Count > 0) 
        {
            habilidadadActiva = habilidades[0];
            Debug.Log("Habilidad Activa incial: " + habilidadadActiva.GetName());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (habilidadadActiva is DashAbility dash && dash.isDashing) 
        {
            Debug.Log($"entro al saltar el dash.");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) SetActiveAbility(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetActiveAbility(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetActiveAbility(2);

        if (Input.GetKeyDown(KeyCode.Q) && habilidadadActiva != null) 
        {
            if (habilidadadActiva.canUse()) 
            {
                habilidadadActiva.UseAbility();
                //Destroy(other.gameObject);
            }
            else 
            {
                Debug.Log($"{habilidadadActiva.GetName()} no tiene m�s usos.");
            }

        }

        // Movimiento del personaje
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;



    }

    private void FixedUpdate()
    {
        // Movimiento físico estable
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    void UpdateHearts()
    {
        Image lastHeart = hearts[hearts.Count - 1];
        hearts.RemoveAt(hearts.Count - 1);
        Destroy(lastHeart.gameObject); // Elimina el objeto del Canvas
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Character collided");

        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            objectObtained = true;
            Debug.Log("Object obtained!");
            Destroy(other.gameObject);
        }
    }


    public Direction GetCurrentDirection() 
    {
        return direction;
    }

    public void SetPosition(Vector2 newPosition) 
    {
        transform.position = newPosition;
        targetPosition = newPosition;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Character took " + damage + " damage. Current health: " + currentHealth);
        UpdateHearts();
        if (currentHealth <= 0)
        {
            Debug.Log("Character has died.");
            SceneManager.LoadScene("Endings");
        }
    }
    
    void SetActiveAbility(int index) 
    {
        if(index >= 0 && index < habilidades.Count) 
        {
            habilidadadActiva = habilidades[index];
            Debug.Log("Habilidad Activa: " + habilidadadActiva.GetName());
        }
    }
}


        

