using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    
    public float speed = 2;

    public float lifeTime = 2; //variable para destruír la bala después de x tiempo (2s)

    public bool left; //booleano para decidir si la bala va para la izquierda o para la derecha

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if(left)
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime); //la bala se traslada hacia la izquierda
        }
        else
        {
            transform.Translate(Vector2.right*speed*Time.deltaTime); //la bala se traslada hacia la derecha
        }
    }
}
