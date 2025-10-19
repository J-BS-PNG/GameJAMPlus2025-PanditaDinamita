using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    private bool espacioObjecto = false;
    private int currentHealth;
    private GameObject objectoCercano;

    public bool objectObtained;

    public List<Image> hearts = new List<Image>();
    //public Sprite fullHeart;       // Corazón lleno
    //public Sprite emptyHeart; 

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
            if (habilidadadActiva.canUse() && espacioObjecto) 
            {
                Destroy(objectoCercano);
                habilidadadActiva.UseAbility();
                //Destroy(other.gameObject);
            }
            else 
            {
                Debug.Log($"{habilidadadActiva.GetName()} no tiene m�s usos.");
            }

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


    void UpdateHearts()
    {

        Image lastHeart = hearts[hearts.Count - 1];
        hearts.RemoveAt(hearts.Count - 1);
        Destroy(lastHeart.gameObject); // Elimina el objeto del Canvas
    
        /*for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Character collided");
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1); 
        }
        else if(other.CompareTag("Item"))
        {
            objectObtained = true;
            Debug.Log("Object obtained!");

        if (other.CompareTag("ObjectoB"))
        {
            objectoCercano = other.gameObject;
            espacioObjecto = true;
            Debug.Log("Se rompio");
        }
    }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ObjectoB"))
        {
            objectoCercano = null;
            espacioObjecto = false;
            Debug.Log("Salio de habilidad");
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Character took " + damage + " damage. Current health: " + currentHealth);
        UpdateHearts();
        if (currentHealth <= 0)
        {
            Debug.Log("Character has died.");
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


        

