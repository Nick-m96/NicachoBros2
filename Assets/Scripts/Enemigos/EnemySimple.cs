using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : EnemyController {

	// Use this for initialization
	private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			if ((col.transform.position.y - 0.3f) > transform.position.y)
			{
				col.SendMessage("EnemyJump");
				Destroy(gameObject);
			}
			else col.SendMessage("EnemyKnock", transform.position.x);
        }
    }
}
