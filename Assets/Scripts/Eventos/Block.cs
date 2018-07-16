using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    
	protected SpriteRenderer spriteRenderer;
	public Sprite nuevo;
	protected Sprite sprite1;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		sprite1 = spriteRenderer.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player")){
			if (col.transform.position.y + 1.5f < transform.position.y)
				spriteRenderer.sprite = nuevo;     
        }
	}
}
