using UnityEngine;

public class PlantEnemie : MonoBehaviour
{
    
    private float waitedTime; // tiempo entre disparo y disparo

    public float waitTimeToAttack = 3;  // frecuencia de disparo (3s)

    public Animator animator;

    public GameObject bulletPrefab;

    public Transform launchSpawnPoint; //posición desde donde se lanza la bala

    private void Start()
    {
        waitedTime = waitTimeToAttack; 
    }

    private void Update()
    {
        if(waitedTime <= 0)
        {
            waitedTime = waitTimeToAttack; //reseteamos el tiempo para atacar
            animator.Play("Attack");
            Invoke("LaunchBullet", 0.5f); //invocamos el método LaunchBullet cada 0,5s
        }
        else
        {
            waitedTime -= Time.deltaTime; //decrementamos el tiempo del disparo en cuestión para volver a atacar
        }
    }

    public void LaunchBullet()
    {
        GameObject newBullet;

        newBullet = Instantiate(bulletPrefab, launchSpawnPoint.position, launchSpawnPoint.rotation); //instanciamos una bala en una posición específica
    }
}
