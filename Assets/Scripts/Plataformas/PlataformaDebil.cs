using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaDebil : MonoBehaviour {

	private Rigidbody2D rgbd;
	private PolygonCollider2D polic;
	private Vector3 pos;

	public float delayCaer = 0.25f;
	public float delaySpawn = 5f;

	// Use this for initialization
	void Start () {
		rgbd = GetComponent<Rigidbody2D>();
		polic = GetComponent<PolygonCollider2D>();
		pos = transform.position;
		rgbd.gravityScale = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player")){
			Invoke("Caer", delayCaer);
			Invoke("Respawn", delaySpawn);
        }
	}

	void Caer(){
		rgbd.isKinematic = false;
        polic.isTrigger = true;
      }

	void Respawn(){
		transform.position = pos;
		rgbd.velocity = Vector3.zero;
		rgbd.isKinematic = true;
        polic.isTrigger = false;
	}
}
