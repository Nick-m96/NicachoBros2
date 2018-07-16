using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaController : MonoBehaviour
{

    public int vidaTotal;
    public float vidaActual;

    public Image[] vida;
    public Sprite full;
    public Sprite media;
    public Sprite vacia;

    private void Update()
    {
        if (vidaActual > vidaTotal)
            vidaActual = vidaTotal;

        for (int i = 0; i < vida.Length; i++)
        {
            if (i + .5f < vidaActual)
                vida[i].sprite = full;
            else if (i < vidaActual)
                vida[i].sprite = media;
            else
                vida[i].sprite = vacia;


            //cant de corazones visibles
            if (i < vidaTotal)
                vida[i].enabled = true;
            else
                vida[i].enabled = false;
        }
    }

	public void RestarVida()
    {
		if(vidaActual > 0f)
		vidaActual -= .5f;
    }
}