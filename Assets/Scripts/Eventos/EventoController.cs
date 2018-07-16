using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoController : MonoBehaviour
{

	public GameObject[] obj;
	public float tiempoEvento;
	public Sprite nuevo;
	public bool esBloque;// o palanca

	private float posPlayer;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite1;
	private CircleCollider2D col2d;
	private bool evento;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite1 = spriteRenderer.sprite;
		posPlayer = esBloque ? 1.5f : -.3f;
		if (!esBloque)
			col2d = GetComponent<CircleCollider2D>();
    }
       
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			if(!evento){
				if (Mathf.Abs(col.transform.position.y + posPlayer - transform.position.y) < .5f){
					spriteRenderer.sprite = nuevo;
					foreach(GameObject item in obj){
						item.SendMessage("ComenzarEvento");
					}
					evento = true;
                    Activar();
                }
            }
		}
	}

	void Activar()
    {
    	if (!esBloque)
    		col2d.offset -= new Vector2(0f, .3f);
       
		Invoke("Desactivar", tiempoEvento);
	}

	void Desactivar()
	{
		spriteRenderer.sprite = sprite1;
		evento = false;
        foreach (GameObject item in obj)
        {
            item.SendMessage("FinalizarEvento");
        }
		if (!esBloque)
			col2d.offset += new Vector2(0f, .3f);
	}
}
