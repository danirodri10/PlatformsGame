using UnityEngine;
using UnityEngine. SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{

    public GameObject transition;

    public TMPro.TextMeshProUGUI succesfullLevel; //texto de nivel no superado

    public GameObject[] hearts; //array con las vidas totales

    private int life; // referencia de nuestra vida actual

    private float checkPointX, checkPointY;

    public Animator animator;

    void Start()
    {

        life = hearts.Length; //establecemos la cantidad de vidas totales a la cantidad de corazones del array

        //comprobamos que se le haya asignado alguna posición al CheckPoint
        if(PlayerPrefs.GetFloat("checkPointX") != 0)
        {
            //Si es así, respawneamos en dicha posición cuando toque
            transform.position=(new Vector2(PlayerPrefs.GetFloat("checkPointX"),PlayerPrefs.GetFloat("checkPointY")));
        }
    }

    private void CheckLife()
    {
        if(life < 1) // morimos
        {
            Destroy(hearts[0].gameObject); //destruímos el primer corazón del array
            animator.Play("Hit"); //activamos la animación de Hit al hacernos daño
            succesfullLevel.text = "DERROTA";
            succesfullLevel.gameObject.SetActive(true); //activamos el texto de derrota cuando hayamos perdido
            transition.SetActive(true); //activamos la transición de cambio de escena
            Invoke("LoadMainMenu", 1f); //invocamos el método para ir al menú principal con un retardo de 0.5s
        }
        else if (life < 2) // quitamos la segunda vida
        {
            Destroy(hearts[1].gameObject); //destruímos el penúltimo corazón del array
            animator.Play("Hit"); //activamos la animación de Hit al hacernos daño
        }
        else if (life < 3) // quitamos la primera vida
        {
            Destroy(hearts[2].gameObject); //destruímos el último corazón del array
            animator.Play("Hit"); //activamos la animación de Hit al hacernos daño
        }
    }

    //guarda la posición del CheckPoint/Respawn
    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkPointX",x);
        PlayerPrefs.SetFloat("checkPointY",y);
    }

    
    public void PlayerDamage()
    {
        // Detener movimiento del jugador para evitar que salga volando
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        life--; //vamos decrementando las vidas
        CheckLife();
    }

    //método para cargar el menú principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); //cargamos la escena del menú principal
    }
}
