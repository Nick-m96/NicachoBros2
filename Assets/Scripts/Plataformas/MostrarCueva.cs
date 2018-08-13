using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarCueva : MonoBehaviour {

	public GameObject pared;

	// Use this for initialization

	// Update is called once per frame
	private void OnTriggerStay2D(Collider2D col)
	{
		if(col.CompareTag("Player")) pared.SetActive(false);
    }

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player"))pared.SetActive(true);
	}
}
