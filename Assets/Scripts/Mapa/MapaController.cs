using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaController : MonoBehaviour
{

	public LugarController ubicacion;
    public GameObject jugador;
    public GameObject lugar;


    // Use this for initialization
    void Start()
    {
        jugador = GameObject.FindWithTag("Player");
		if (!lugar){
			lugar = GameObject.Find("Bosque");
			ubicacion = lugar.GetComponent<LugarController>();
            
        }
        jugador.transform.position = lugar.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.UpArrow))
			MoverA(ubicacion.up);
		if (Input.GetKeyDown(KeyCode.DownArrow))
            MoverA(ubicacion.down);
		if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoverA(ubicacion.left);
		if (Input.GetKeyDown(KeyCode.RightArrow))
            MoverA(ubicacion.right);
		
    }

	void MoverA(LugarController nuevaUbicacion){
		if(nuevaUbicacion){
			ubicacion = nuevaUbicacion;
			lugar = ubicacion.gameObject;
			jugador.transform.position = lugar.transform.position;         
        }
	}
}