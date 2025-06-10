// CheckPoint.cs modificado
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public TMPro.TextMeshProUGUI succesfullLevel;
    public GameObject transition;
    private bool isTemporarilyActive = false;
    private float desactivationTimer = 0f;

    void Update()
    {
        // si está activado, decrementamos hasta desactivarlo
        if (isTemporarilyActive)
        {
            desactivationTimer -= Time.deltaTime;
            if (desactivationTimer <= 0f)
            {
                DeactivateCheckpoint();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = true; //activamos el collider del checkpoint
            GetComponent<SpriteRenderer>().enabled = true; //activamos el sprite del checkpoint
            GetComponent<Animator>().enabled = true; //activamos la animación del checkpoint
            Invoke("ActiveUI",2); //cargamos la UI 
            Invoke("ChangeScene",3f); //cambiamos de escena
        }
    }

    //método para activar el checkpoint temporalmente
    public void ActivateTemporarily(float duration)
    {
        isTemporarilyActive = true;
        GetComponent<Collider2D>().enabled = true; //activamos el collider del checkpoint
        GetComponent<SpriteRenderer>().enabled = true; //activamos el sprite del checkpoint
        desactivationTimer = duration;
    }

    //método para desactivar el checkpoint
    private void DeactivateCheckpoint()
    {
        isTemporarilyActive = false;
        GetComponent<Animator>().enabled = false; //desactivamos la animación del checkpoint
        GetComponent<Collider2D>().enabled = false; //desactivamos el collider del checkpoint
        GetComponent<SpriteRenderer>().enabled = false; //desactivamos el sprite del checkpoint
    }

    //método para mostrar la UI
    private void ActiveUI()
    {
        succesfullLevel.gameObject.SetActive(true);
        transition.SetActive(true);
    }

    //método para cambiar de escena
    private void ChangeScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
