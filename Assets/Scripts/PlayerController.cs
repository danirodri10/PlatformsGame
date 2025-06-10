using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 8f;
    public float doubleJumpSpeed = 2.5f;
    private bool canDoubleJump;

    private Rigidbody2D rb;

    public bool betterJump = false;
    public float smallJump = 0.5f;
    public float bigJump = 1f;

    private float originalJumpForce;
    private float powerUpTimer = 0f;

    private float originalGravityScale; // Almacena la escala de gravedad original
    private float gravityPowerUpTimer = 0f; // Temporizador para la gravedad invertida
    private bool isGravityInverted = false; // Booleano que indica si la gravedad está invertida

    public SpriteRenderer playerSpriteRenderer;
    public Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalJumpForce = jumpForce; // Guardamos la fuerza de salto original
        originalGravityScale = rb.gravityScale; // Guardamos la escala de gravedad original
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Animaciones de movimiento
        if(Input.GetKey("a") || Input.GetKey("left"))
        {
            playerSpriteRenderer.flipX = true;
            playerAnimator.SetBool("Run", true);
        }
        else if(Input.GetKey("d") || Input.GetKey("right"))
        {
            playerSpriteRenderer.flipX = false;
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }

        // Manejo de saltos 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(CheckGround.isGrounded)
            {
                canDoubleJump = true;

                // Si la gravedad está invertida, aplica fuerza hacia abajo (saltar hacia abajo)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, isGravityInverted ? -jumpForce : jumpForce);
            }
            else if(canDoubleJump)
            {
                playerAnimator.SetBool("DoubleJump", true);

                // Adaptamos el doble salto también a la gravedad invertida 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, isGravityInverted ? -doubleJumpSpeed : doubleJumpSpeed);
                canDoubleJump = false;
            }
        }

        // Animaciones de salto/caída
        if(CheckGround.isGrounded == false)
        {
            playerAnimator.SetBool("Jump", true);
            playerAnimator.SetBool("Run", false);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("Falling", false);
            playerAnimator.SetBool("DoubleJump", false);
        }

        // Better Jump
        if(betterJump)
        {
            // Adaptamos el BetterJump a la gravedad invertida
            if (isGravityInverted)
            {
                if(rb.linearVelocity.y > 0) // Cayendo hacia arriba
                {
                    rb.linearVelocity += Vector2.down * Physics2D.gravity.y * smallJump * Time.deltaTime;
                    playerAnimator.SetBool("Falling", true);
                }
                else if(rb.linearVelocity.y < 0 && !Input.GetKey("space")) // Saltando hacia abajo
                {
                    rb.linearVelocity += Vector2.down * Physics2D.gravity.y * bigJump * Time.deltaTime;
                    playerAnimator.SetBool("Falling", false);
                }
            }
            else
            {
                if(rb.linearVelocity.y < 0)
                {
                    rb.linearVelocity += Vector2.up * Physics2D.gravity.y * smallJump * Time.deltaTime;
                    playerAnimator.SetBool("Falling", true);
                }
                else if(rb.linearVelocity.y > 0 && !Input.GetKey("space"))
                {
                    rb.linearVelocity += Vector2.up * Physics2D.gravity.y * bigJump * Time.deltaTime;
                    playerAnimator.SetBool("Falling", false);
                }
            }
        }

        // Temporizador power-up salto
        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                jumpForce = originalJumpForce;
            }
        }

        // Temporizador gravedad invertida
        if (gravityPowerUpTimer > 0)
        {
            gravityPowerUpTimer -= Time.deltaTime;
            if (gravityPowerUpTimer <= 0)
            {
                RestoreGravity();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts) //detecta todos los posibles puntos de contacto entre colliders
            {
                // Si gravedad normal: buscamos colisión desde abajo (normal hacia arriba)
                if (!isGravityInverted && contact.normal.y > 0.5f)
                {
                    CheckGround.isGrounded = true;
                    break;
                }
                // Si gravedad invertida: buscamos colisión desde arriba (normal hacia abajo)
                else if (isGravityInverted && contact.normal.y < -0.5f)
                {
                    CheckGround.isGrounded = true;
                    break;
                }
            }
        }
    }

    // Método para detectar cuando no estamos en el suelo y por ende no poder saltar
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CheckGround.isGrounded = false;
        }
    }

    // Método para activar PowerUps al recoger las frutas
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherries"))
        {
            ActivateJumpPower(6.5f, 6f);
        }
        else if (collision.gameObject.CompareTag("Melon"))
        {
            InvertGravity(5f);
        }
        else if (collision.gameObject.CompareTag("Strawberry"))
        {
            CheckPoint checkpoint = FindObjectOfType<CheckPoint>();
            if (checkpoint != null)
            {
                checkpoint.ActivateTemporarily(6f);
            }
        }
    }

    // Método para activar el PowerUp del super salto
    public void ActivateJumpPower(float newJumpForce, float duration)
    {
        jumpForce = newJumpForce;
        powerUpTimer = duration;
    }

    // Método para activar el PowerUp de invertir la gravedad
    public void InvertGravity(float duration)
    {
        isGravityInverted = true; // ponemos a true el booleano de gravedad invertida
        rb.gravityScale = -originalGravityScale; // invertimos la gravedad
        playerSpriteRenderer.flipY = true; //flipeamos el sprite del player en Y
        gravityPowerUpTimer = duration; // establecemos el tiempo del temporizador
    }

    // Método para restaurar la gravedad normal
    private void RestoreGravity()
    {
        isGravityInverted = false; 
        rb.gravityScale = originalGravityScale; 
        playerSpriteRenderer.flipY = false;
    }
}

