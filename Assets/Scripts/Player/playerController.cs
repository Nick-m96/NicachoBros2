using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public static playerController instancia = null;

	public float vel = 20f;
	public float maxVel = 5f;
	public bool enSuelo;
	public float poderSalto = 120f;
	public VidaController vida;
	public Vector2 posIni;
	public GameObject camara;
    
	private Rigidbody2D rg;
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private bool saltar;
	private bool dobleSalto;
	private bool movimiento = true;
	private bool cargando;
	public bool atacando;

	// Use this for initialization
	void Start () {
		posIni = transform.position;
		rg = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
        
	}

	private void Awake()
	{
		if (!instancia) instancia = this;
		else if (instancia != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {
		animator.SetFloat("Vel", Mathf.Abs(rg.velocity.x));
		animator.SetBool("EnSuelo", enSuelo);
		animator.SetBool("Atacando", atacando);

		if (Input.GetKeyDown(KeyCode.Space)){
			if(enSuelo){
				saltar = true;
				dobleSalto = true;          
			}
			else if (dobleSalto)
            {
                saltar = true;
                dobleSalto = false;
	        }   
		}
		if (Input.GetKeyDown(KeyCode.E) && enSuelo){

			StartCoroutine(Atacar());
		}
	}

	private void FixedUpdate()
	{
		Vector3 fixedVelocity = rg.velocity;
		fixedVelocity.x *= 0.75f;

		if (enSuelo)
			rg.velocity = fixedVelocity;

		float h = Input.GetAxis("Horizontal");
		if (!movimiento) h = 0;

		float velLimite = Mathf.Clamp(rg.velocity.x, -maxVel, maxVel);
		rg.AddForce(Vector2.right * h * vel);

		rg.velocity = new Vector2(velLimite, rg.velocity.y);
		if (h > 0.1f)
			transform.localScale = new Vector3(1f, 1f, 1f);
		if (h < -0.1f)
			transform.localScale = new Vector3(-1f, 1f, 1f);
		if (saltar && dobleSalto)
		{// primer salto
			rg.velocity = new Vector2(rg.velocity.x, 0);
			rg.AddForce(Vector2.up * poderSalto, ForceMode2D.Impulse);
			saltar = false;
		}
		else if (saltar)
		{
			rg.velocity = new Vector2(rg.velocity.x, 0);
			rg.AddForce(Vector2.up * poderSalto * .75f, ForceMode2D.Impulse);
			saltar = false;
		}
	}

	private void SeCayo()
	{

			transform.position = posIni;
			vida.RestarVida();     
	}

	public void EnemyJump(){
		saltar = true;
	}

	public void EnemyKnock(float posX)
    {
        saltar = true;
		float side = Mathf.Sign(posX - transform.position.x);
		rg.AddForce(Vector2.left * side * poderSalto, ForceMode2D.Impulse);

		movimiento = false;
		spriteRenderer.color = Color.red;
		vida.RestarVida();
        Invoke("ActivarMove", 0.5f);

    }

	void ActivarMove(){
		movimiento = true;
		spriteRenderer.color = Color.white;
     }

	public void NuevaPosInicial(Vector2 nuevaPosIni){
		posIni = nuevaPosIni;
	}

	IEnumerator Atacar(){
		atacando = true;

		if (atacando)
			yield return new WaitForSeconds(.5f);
        atacando = false;
       }

	public void SetPosIni(Vector2 pos){
		posIni = pos;
	}
}
