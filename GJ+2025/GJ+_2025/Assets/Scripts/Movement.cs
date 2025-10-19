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

    private IAbility habilidadadActiva; //poderes

    public int habilidadesRestantes; 
    public List<Image> habilidadesIcons = new List<Image>();

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

    private SpriteRenderer spriteRenderer;


    //public Sprite fullHeart;       // Corazón lleno
    //public Sprite emptyHeart; 

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        currentHealth = maxHealth;
        direction = Direction.down; 
        spriteRenderer = GetComponent<SpriteRenderer>();




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
                habilidadesRestantes = habilidadadActiva.remainingUses();
                Debug.Log($"Usando habilidad: {habilidadadActiva.GetName()}, usos restantes: {habilidadesRestantes}");
                UpdatePower();
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
            // Solo voltear el sprite
        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;
    }

    void MoveAndFlip(Vector2 direction)
    {
        // Movimiento
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Flip del sprite (solo si se mueve horizontalmente)
        if (direction.x != 0)
            spriteRenderer.flipX = direction.x > 0;

    }

    void UpdatePower(){
        Image lastIcon = habilidadesIcons[habilidadesIcons.Count - 1];
        habilidadesIcons.RemoveAt(habilidadesIcons.Count - 1);
        Destroy(lastIcon.gameObject);
    }

    void UpdateHearts()
    {
        if (hearts.Count == 0)
            return;

        Image lastHeart = hearts[hearts.Count - 1];
        hearts.RemoveAt(hearts.Count - 1);

        // 🔹 Animación con LeanTween: escala y desvanecimiento
        RectTransform heartRect = lastHeart.GetComponent<RectTransform>();
        CanvasGroup cg = lastHeart.GetComponent<CanvasGroup>();

        // Si no tiene CanvasGroup, se lo agregamos
        if (cg == null)
            cg = lastHeart.gameObject.AddComponent<CanvasGroup>();

        // Escalar hacia abajo y desvanecer
        LeanTween.scale(heartRect, Vector3.zero, 0.3f)
            .setEaseInBack();

        LeanTween.alphaCanvas(cg, 0f, 0.3f)
            .setEaseInCubic()
            .setOnComplete(() =>
            {
                Destroy(lastHeart.gameObject); // Elimina el objeto después de la animación
            });
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


        

