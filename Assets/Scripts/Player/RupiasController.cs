using UnityEngine;
using System.Collections;

public class RupiasController : MonoBehaviour
{

	public int dinero;
	public int maxDinero;
    // Use this for initialization
    void Start()
    {
		dinero = 0;
		maxDinero = 100;
    }

    // Update is called once per frame
	void Update(){
		
	}


	public void SumarDinero(int valor)
    {
        dinero += valor;
		if (dinero > maxDinero)
			dinero = maxDinero;
        Debug.Log("ahora tenes " + dinero);
    }
}
