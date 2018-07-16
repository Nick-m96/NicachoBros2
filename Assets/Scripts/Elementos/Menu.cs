using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public static Menu instancia = null;

	public Canvas panel;
	private bool pausa;

	// Use this for initialization
	void Start () {
        
		panel.enabled = false;

	}
	private void Awake()
	{
		if (!instancia) instancia = this;
        else if (instancia != this)
            Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			pausa = !pausa;
			Time.timeScale = (pausa) ? 0f : 1f;
			panel.enabled = pausa;
		}
	}
}
