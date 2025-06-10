using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FruitManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI succesfullLevel; //texto de nivel superado
    public TMPro.TextMeshProUGUI frutasPorObtener; //texto de frutas que faltan por coger
    public TMPro.TextMeshProUGUI frutasTotales; //texto de frutas totales
    public int frutasTotalesNivel;

     public PlayerController playerController;

    public GameObject transition;

    private void Start()
    {
        frutasTotalesNivel = transform.childCount;
    }

    private void Update()
    {
        
        frutasTotales.text = frutasTotalesNivel.ToString(); //actualizamos el texto de las frutas totales con la cantidad de dichas frutas

        frutasPorObtener.text = transform.childCount.ToString(); //va actualizando el texto de la cantidad de frutas que nos quedan por coger
    }

}
