using UnityEngine;

public class BasicAI : MonoBehaviour
{

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public float speed = 0.5f;

    private float waitTime; //tiempo de espera al llegar a cierto punto

    public float startWaitTime = 2; //tiempo de espera en cierto punto

    public Transform[] moveSpots; 

    private int position = 0; //gestiona en que posición estamos y a cual tenemos que ir

    private Vector2 actualPos; //es la posición actual


    void Start()
    {
        waitTime = startWaitTime;
    }

    
    void Update()
    {

        // Detectar dirección y voltear el sprite
        // si la posición en X del siguiente punto, es mayor que nuestra posición actual (es decir, está más a la derecha), flipeamos
        if (moveSpots[position].position.x > transform.position.x)
        {
            spriteRenderer.flipX = true; // mirando a la derecha
            animator.SetBool("Idle", false); //nos estamos moviendo, animación idle = false
        }
        else
        {
            spriteRenderer.flipX = false; // mirando a la izquierda
            animator.SetBool("Idle", false); //nos estamos moviendo, animación idle = false
        }

        
        
        //movimiento del enemigo avanzando de un punto a otro
        //utilizamos position para moverlo en x puntos, no solo de punto a a punto b
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[position].transform.position, speed * Time.deltaTime);

        //condición para saber si el enemigo está cerca del punto al que tiene que llegar
        if(Vector2.Distance(transform.position, moveSpots[position].transform.position) < 0.1f)
        {
            animator.SetBool("Idle", true); // Está quieto, animación Idle activada
            
            //si ya se ha completado el tiempo de espera en el punto
            if(waitTime<=0)
            {
                //si el punto actual no es el último punto, avanzamos al siguiente
                if(moveSpots[position]!=moveSpots[moveSpots.Length - 1])
                {
                    position++; //incrementamos para ir a la siguiente posición
                }
                else
                {
                    position = 0; //reiniciamos y vamos al punto 0
                }

                waitTime = startWaitTime; //reiniciamos el tiempo
            }
            else
            {
                waitTime -= Time.deltaTime; //vamos decrementando el tiempo de espera
            }
        }
    }
}
