using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorn : EnemyController {   

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Player")){
			if (player.atacando) Destroy(gameObject);
            else col.SendMessage("EnemyKnock", transform.position.x);          
        }
   
    }

}
