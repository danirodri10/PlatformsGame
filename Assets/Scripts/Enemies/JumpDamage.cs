using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Collider2D collider;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public GameObject destroyParticle;

    public float jumpForce = 2.5f; //fuerza de salto al saltar sobre el enemigo, que nos hace rebotar

    public int lifes = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (Vector2.up * jumpForce); //hacer que el personaje rebote al saltar sobre el enemigo
            LoseLifeAndHit();
            CheckLife();
        }
    }

    public void LoseLifeAndHit()
    {
        lifes--; //le restamos una vida al enemigo cada vez que sea golpeado
        animator.Play("Hit"); //activamos la animación de hit
    }

    public void CheckLife()
    {
        if(lifes == 0)
        {
            destroyParticle.SetActive(true); //activamos las partículas
            spriteRenderer.enabled = false; //hacemos que desaparezca el sprite del enemigo
            Invoke("EnemyDie", 0.2f); //después de 0.2s, invocamos al método EnemyDie para destruir al enemigo
        }
    }

    public void EnemyDie()
    {
        Destroy(gameObject); //destruímos el enemigo
    }
}
