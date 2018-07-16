using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour {

	public GameObject follow;
	public Vector2 minCamPos, maxCampos;
	public float smoothTime = 2f;

	private Vector2 vel;

	// Use this for initialization
	void Awake () {
		follow = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref vel.x, smoothTime);
		float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref vel.y, smoothTime);

		transform.position = new Vector3(
		Mathf.Clamp(posX, minCamPos.x, maxCampos.x),
		Mathf.Clamp(posY, minCamPos.y, maxCampos.y),
		transform.position.z
		);

		if (transform.position.y - 6f > follow.transform.position.y)
			follow.SendMessage("SeCayo");
	}
}
