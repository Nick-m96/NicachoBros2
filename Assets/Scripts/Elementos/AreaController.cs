using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	public IEnumerator FadeOut(string area){
		animator.Play("AreaShow");

		transform.GetChild(0).GetComponent<Text>().text = area;
		transform.GetChild(1).GetComponent<Text>().text = area;

		yield return new WaitForSeconds(1f);

		animator.Play("AreaFadeOut");
	}

}
