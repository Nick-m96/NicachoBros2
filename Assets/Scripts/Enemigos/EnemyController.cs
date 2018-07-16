using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float vel = 1.5f;
    public float maxVel = 1.5f;
	public playerController player;

	private Vector3 start;
    private Vector3 final;
	public Transform objetivo;

	private Rigidbody2D rg;

	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D>();
        if (objetivo){
            objetivo.parent = null;
            start = transform.position;
            final = objetivo.position;
        }
		player = GameObject.Find("Player").GetComponent<playerController>();
	}
    
	private void FixedUpdate()
    {
		if (!objetivo)
        {
			float velLimite = Mathf.Clamp(rg.velocity.x, -maxVel, maxVel);
			rg.AddForce(Vector2.right * vel);

			rg.velocity = new Vector2(velLimite, rg.velocity.y);

			if (rg.velocity.x > -0.01f && rg.velocity.x < 0.01f)
			{
				vel *= -1f;
				rg.velocity = new Vector2(vel, rg.velocity.y);
			}


			if (vel < 0.1f)
				transform.localScale = new Vector3(1f, 1f, 1f);
			if (vel > -0.1f)
				transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		else{
			float fixVel = vel * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, objetivo.position, fixVel);
        
			if (transform.position == objetivo.position){
					if(objetivo.position == start){
					objetivo.position = final;
					transform.localScale = new Vector3(1f, 1f, 1f);     
				}
				else{
					objetivo.position = start;
					transform.localScale = new Vector3(-1f, 1f, 1f);            
                }
            }
		}
    }

}
