using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour {

	public int valor;

	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") || col.CompareTag("Weapon")){
			col.SendMessage("SumarDinero", valor);
			Destroy(gameObject);
			Debug.Log("sume " + valor);
		}
	}

}
