using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitControler : MonoBehaviour {

	public string nivel;
	public float posX, posY;

	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter2D(Collider2D player)
	{
		if (player.gameObject.CompareTag("Player"))
		{
			player.transform.position = new Vector3(posX, posY, player.transform.position.z);
			player.SendMessage("NuevaPosInicial", new Vector2(posX, posY));
			SceneManager.LoadScene(nivel);
        }	
	}
}
