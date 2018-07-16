using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminoSimple : MonoBehaviour {

	public Transform target;
	public float vel;

	private Vector3 start;
	private Vector3 final;

	// Use this for initialization
	void Start () {
		if (target){
			target.parent = null;
			start = transform.position;
			final = target.position;
        }
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target != null){
			float fixVel = vel * Time.deltaTime;			
			transform.position = Vector3.MoveTowards(transform.position, target.position, fixVel);
        }
		if (transform.position == target.position)
			target.position = (target.position == start) ? final : start;
	}
}
