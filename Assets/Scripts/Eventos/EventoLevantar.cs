using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoLevantar : MonoBehaviour {

	public Vector2 dest;
    public float vel;

	private float fixVel;
	private Vector3 posDest;
	private Vector3 posIni;
	private PolygonCollider2D polygonCollider2D;

	// Use this for initialization
	void Start () {
		posIni = transform.position;
		posDest = posIni + new Vector3(dest.x, dest.y);
		polygonCollider2D = GetComponent<PolygonCollider2D>();
		polygonCollider2D.enabled = false;
		fixVel = vel * Time.deltaTime;  
	}
    
	IEnumerator ComenzarEvento(){
		
		Debug.Log("rutinaArriba");
		while(transform.position != posDest){
			transform.position = Vector3.MoveTowards(transform.position, posDest, fixVel);
			yield return new WaitForSeconds(.01f);
        }
	}
	IEnumerator FinalizarEvento()
    {
		polygonCollider2D.enabled = true;
		Debug.Log("abajo");
		while (transform.position != posIni){
			transform.position = Vector3.MoveTowards(transform.position, posIni, fixVel); 
			yield return new WaitForSeconds(.01f);
        }
		polygonCollider2D.enabled = false;
    }
}
