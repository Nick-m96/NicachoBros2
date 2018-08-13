using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour {

	public float smoothTime = 3f;

    public Transform target;
    public float tLX, tLY, bRX, bRY;

    Vector2 velocity; // necesario para el suavizado de cámara

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }



	void Update()
	{
		float posX = Mathf.Round(
            Mathf.SmoothDamp(transform.position.x,
                target.position.x, ref velocity.x, smoothTime) * 100) / 100;
        float posY = Mathf.Round(
            Mathf.SmoothDamp(transform.position.y,
                target.position.y, ref velocity.y, smoothTime) * 100) / 100;
		transform.position = new Vector3(
			Mathf.Clamp(posX, tLX, bRX),
			Mathf.Clamp(posY, bRY, tLY),
			transform.position.z);

		if (transform.position.y - 6f > target.transform.position.y)
            target.SendMessage("SeCayo");
    }

    public void SetBound(GameObject map)
    {
        Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
        float cameraSize = Camera.main.orthographicSize;
        tLX = map.transform.position.x + cameraSize + 4;
		tLY = map.transform.position.y - cameraSize ;
		bRX = map.transform.position.x + config.NumTilesWide - cameraSize -4;
		bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;

        FastMove();

    }

    public void FastMove()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
    }
}
