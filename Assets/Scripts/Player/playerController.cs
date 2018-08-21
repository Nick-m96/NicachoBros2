using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public static playerController instancia = null;

	public float vel = 20f;
	public float maxVel = 5f;
	public bool enSuelo;
	public float poderSalto = 120f;
    public Vector3 posIni;
    public GameObject camara;
    
    private Rigidbody2D rg;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CapsuleCollider2D weaponCol;

	public VidaController vida;

    //Acciones-------//
	private bool saltar;
	private bool dobleSalto;
	private bool movimiento = true;
	private bool cargando;

	public bool atacando;
    //---------------//

	public GameObject initialMap;
	public GameObject initialEnemyPreb;
    
	void Start () {

		weaponCol = transform.GetChild(1).GetComponent<CapsuleCollider2D>();
		weaponCol.enabled = false;

        if(initialEnemyPreb) Instantiate(initialEnemyPreb);
        //En caso de caer va al ultimo checkPoint
		posIni = transform.position;

		rg = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		Camera.main.GetComponent<CamaraFollow>().SetBound(initialMap);
        
	}

	private void Awake()
	{
		if (!instancia) instancia = this;
		else if (instancia != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		animator.SetFloat("Vel", Mathf.Abs(rg.velocity.x));
		animator.SetBool("EnSuelo", enSuelo);
        
		Salto();

		Ataque();
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
		Debug.Log("cae");
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
    
	public void SetPosIni(Vector3 pos){
		posIni = pos;
	}

	void Salto(){
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enSuelo)
            {
                saltar = true;
                dobleSalto = true;
            }
            else if (dobleSalto)
            {
                saltar = true;
                dobleSalto = false;
            }
        }
	}

	void Ataque(){
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        atacando = stateInfo.IsName("JugadorEspada");

        //Ataque
        if (Input.GetKeyDown(KeyCode.E) && !atacando)
            animator.SetTrigger("Atacando");

        if (atacando)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.12 && playbackTime < 0.4) weaponCol.enabled = true;
            else weaponCol.enabled = false;
        }
	}
}
