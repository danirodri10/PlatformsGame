using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player")){
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamage();  //Cuando el Player colisiona con dicho objeto, le hace daño
        }
    }
}
