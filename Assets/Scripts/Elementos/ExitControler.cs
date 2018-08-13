using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitControler : MonoBehaviour {

    [Tooltip("Warp objetivo")]
	public GameObject target;
    [Tooltip("Mapa objetivo")]
	public GameObject targetMap;
	[Tooltip("Donde estan los enemigos")]
	public GameObject EnemigosPreb;

	GameObject area;

 // Para controlar si empieza o no la transición
    bool start = false;
    // Para controlar si la transición es de entrada o salida
    bool isFadeIn = false;
    // Opacidad inicial del cuadrado de transición
    float alpha = 0;
    // Transición de 1 segundo
    float fadeTime = 1f;

	private void Awake()
	{
		area = GameObject.FindWithTag("Area");
	}


	IEnumerator OnTriggerEnter2D(Collider2D col)
    {
		if(col.CompareTag("Player")){

			col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			col.GetComponent<Animator>().enabled = false;
            col.GetComponent<playerController>().enabled = false;

			FadeIn();
			StartCoroutine(area.GetComponent<AreaController>().FadeOut(targetMap.name));
            yield return new WaitForSeconds(fadeTime);

            col.transform.position = target.transform.GetChild(0).transform.position;// posicion del hijo del warp
            col.SendMessage("SetPosIni", col.transform.position);
            Camera.main.GetComponent<CamaraFollow>().SetBound(targetMap);

            //destruyo los enemigos del mapa viejo
         
			GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemigo");
			if(objects.Length != 0)
                foreach(GameObject obj in objects)
			        Destroy(obj);
         
			//Genero los enemigos del nuevo mapa
            if(EnemigosPreb) Instantiate(EnemigosPreb);          

           
                
            FadeOut();

            col.GetComponent<Animator>().enabled = true;
            col.GetComponent<playerController>().enabled = true;

        }
        

    }

	// Dibujaremos un cuadrado con opacidad encima de la pantalla simulando una transición
    void OnGUI()
    {

        // Si no empieza la transición salimos del evento directamente
        if (!start)
            return;

        // Si ha empezado creamos un color con una opacidad inicial a 0
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        // Creamos una textura temporal para rellenar la pantalla
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();

        // Dibujamos la textura sobre toda la pantalla
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        // Controlamos la transparencia
        if (isFadeIn)
        {
            // Si es la de aparecer le sumamos opacidad
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            // Si es la de desaparecer le restamos opacidad
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
            // Si la opacidad llega a 0 desactivamos la transición
            if (alpha < 0) start = false;
        }

    }

    // Método para activar la transición de entrada
    void FadeIn()
    {
        start = true;
        isFadeIn = true;
    }

    // Método para activar la transición de salida
    void FadeOut()
    {
        isFadeIn = false;
    }

}
