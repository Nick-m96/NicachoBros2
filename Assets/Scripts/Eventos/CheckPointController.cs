using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

	private Vector2 pos;
	public Sprite check;
	private Animator animator;
	private bool activarCheck;

	// Use this for initialization
	void Start () {
		pos = new Vector2(transform.position.x, transform.position.y);
		animator = GetComponent<Animator>();

	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Player")){
			col.SendMessage("SetPosIni", pos);
			activarCheck = true;      
			animator.SetBool("activarCheck", activarCheck);
        }
	}
}
