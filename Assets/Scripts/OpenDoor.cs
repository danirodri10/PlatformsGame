using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{

    public TMPro.TextMeshProUGUI text;
    public string levelName;
    public bool inDoor = false; 
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(true); //activamos el texto de información para entrar por la puerta
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false); //desactivamos el texto de información para entrar por la puerta
        inDoor = false;
    }


    private void Update()
    {
        //si estamos en la puerta y pulsamos la E, cargamos el nivel correspondiente 
        if(inDoor && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(levelName); //le pasamos en el inspector a cada puerta su respectivo String para cargar dicha escena
        }
    }
}
