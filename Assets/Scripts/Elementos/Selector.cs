using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour
{

    public GameObject flecha, lista;

    int indice = 0;

    void Start()
    {
        Dibujar();
    }

    void Update()
    {

        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");

        if (up) indice--;
        if (down) indice++;

        if (indice > lista.transform.childCount - 1) indice = 0;
        else if (indice < 0) indice = lista.transform.childCount - 1;

        if (up || down) Dibujar();

        if (Input.GetKeyDown("return")) Accion();

    }

    void Dibujar()
    {
        Transform opcion = lista.transform.GetChild(indice);
		flecha.transform.position =new Vector3 (flecha.transform.position.x , opcion.position.y - 0.2566187f, 0f); // Pivote a la izquierda
    }

    void Accion()
    {
		Transform opcion = lista.transform.GetChild(indice);
		if (opcion.gameObject.name == "Cargar" || opcion.gameObject.name == "Salir")
        { // Conseguir nombre del objeto
            print("Proximamente");
            //Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(opcion.gameObject.name);
        }
    }
}