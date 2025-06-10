using UnityEngine;
using UnityEngine.Audio;

public class FruitCollected : MonoBehaviour
{
    public AudioSource clip;
    private Vector3 originalPosition;
    private bool isCollected = false;
    private float respawnTimer = 0f;

    void Start()
    {
        originalPosition = transform.position; // Guardamos la posición original
    }

    void Update()
    {
        //lógica para cuando recolectamos la fruta
        if (isCollected)
        {
            respawnTimer -= Time.deltaTime; // reducimos el temporizador. Si llega a 0, respawneamos la fruta 
            if (respawnTimer <= 0f)
            {
                RespawnFruit();
            }
        }
    }

    //verificación de que es el jugador quien recoge la fruta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isCollected)
        {
            CollectFruit();
        }
    }

    //lógica para recolectar la fruta
    private void CollectFruit()
    {
        isCollected = true;
        GetComponent<SpriteRenderer>().enabled = false; //desactivamos el sprite de la fruta
        gameObject.transform.GetChild(0).gameObject.SetActive(true); //activamos la partícula de la fruta
        GetComponent<Collider2D>().enabled = false; // Desactivamos el collider
        gameObject.transform.GetChild(0).gameObject.SetActive(false); //volvemos a desactivar la partícula de la fruta para que no quede en bucle
        clip.Play();
        respawnTimer = 7f; // Establecemos el tiempo de respawn
    }

    //lógica para respawnear la fruta
    private void RespawnFruit()
    {
        isCollected = false;
        transform.position = originalPosition;
        GetComponent<SpriteRenderer>().enabled = true; //activamos el sprite de la fruta
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true; // Reactivamos el collider
    }
}
