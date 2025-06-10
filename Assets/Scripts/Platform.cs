using UnityEngine;

public class PLtform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float startWaitTime = 0.5f; //tiempo de espera antes de permitir bajar
    private float waitedTime; //temporizador que cuenta hacia atrás desde startWaitTime 
    private bool playerOnPlatform;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        // Si el jugador no está en la plataforma, reseteamos
        if (!playerOnPlatform)
        {
            effector.rotationalOffset = 0; //resetea el rotationalOffset a 0
            waitedTime = startWaitTime; //reinicia el temporizador
            return;
        }

        // Lógica para bajar
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && waitedTime <= 0)
        {
            effector.rotationalOffset = 180f; //rotamos el offset 180 grados para poder bajar
            waitedTime = startWaitTime; // Reiniciamos el tiempo
        }
        else
        {
            waitedTime -= Time.deltaTime; //decremenatmos el temporizador con Time.deltaTime para que decremente igual en todos los pcs, independiente de los fps a los que vaya
        }

        // Resetear al saltar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effector.rotationalOffset = 0; //ponemos el offset a 0
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            effector.rotationalOffset = 0; // Aseguramos reset al salir
        }
    }
}
