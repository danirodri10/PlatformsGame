using UnityEngine;

public class PlayerSelect : MonoBehaviour
{

    public bool enabledSelectCharacter;
    public enum Player {Frog, PinkMan, VirtualGuy};
    public Player playerSelected;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public RuntimeAnimatorController[] playersController; //árboles de animación de cada personaje
    public Sprite[] playersRenderer; //Sprites de cada personaje

    void Start()
    {

        if(!enabledSelectCharacter)
        {
            ChangePlayerInMenu();
        }
        else
        {
            switch(playerSelected)
            {
                case Player.Frog:
                spriteRenderer.sprite = playersRenderer[0]; //cargamos el Sprite del frog
                animator.runtimeAnimatorController = playersController[0]; //cargamos el árbol de animación del frog
                break;

                case Player.PinkMan:
                spriteRenderer.sprite = playersRenderer[1]; //cargamos el Sprite del frog
                animator.runtimeAnimatorController = playersController[1]; //cargamos el árbol de animación del frog
                break;

                case Player.VirtualGuy:
                spriteRenderer.sprite = playersRenderer[2]; //cargamos el Sprite del frog
                animator.runtimeAnimatorController = playersController[2]; //cargamos el árbol de animación del frog
                break;

                default:
                break;
            }
        }
    }

    public void ChangePlayerInMenu()
    {
        switch(PlayerPrefs.GetString("PlayerSelected"))
            {
                case "Frog":
                spriteRenderer.sprite = playersRenderer[0]; //cargamos el Sprite del frog
                animator.runtimeAnimatorController = playersController[0]; //cargamos el árbol de animación del frog
                break;

                case "PinkMan":
                spriteRenderer.sprite = playersRenderer[1]; //cargamos el Sprite del frog
                animator.runtimeAnimatorController = playersController[1]; //cargamos el árbol de animación del frog
                break;

                case "VirtualGuy":
                spriteRenderer.sprite = playersRenderer[2]; //cargamos el Sprite del frog
                animator.runtimeAnimatorController = playersController[2]; //cargamos el árbol de animación del frog
                break;

                default:
                break;
            }
    }
}
