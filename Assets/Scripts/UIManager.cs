using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public AudioSource clip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf) // Si el panel está activo
            {
                ReturnToGame(); // Cierra el panel
            }
            else // Si el panel está inactivo
            {
                OpenOptionsPanel(); // Abre el panel
            }
        }
    }

    //método para abrir el panel de opciones
    public void OpenOptionsPanel()
    {
        Time.timeScale = 0; //se para el tiempo (es decir, se para el juego)
        optionsPanel.SetActive(true); //activamos el panel de opciones
    }

    //método para cerrar el panel y volver al juego
    public void ReturnToGame()
    {
        Time.timeScale = 1; //se reanuda el tiempo (es decir, se reanuda el juego)
        optionsPanel.SetActive(false); //desactivamos el panel de opciones
    }

    //método para mostrar los ajustes
    public void Settings()
    {
        //todo
    }

    //método para ir al menú principal
    public void GoMainMenu()
    {
        Time.timeScale = 1; //se reanuda el tiempo (es decir, se reanuda el juego)
        SceneManager.LoadScene("MainMenu"); //cargamos la escena del menú principal
    }

    //método para salir del juego
    public void QuitGame()
    {
        Application.Quit(); //sale del juego (solo funciona con la build)
    }

    //método para iniciar el sonido al clickar el botón de settings
    public void PlaySoundButton()
    {
        clip.Play(); //arrancamos la música
    }
}
