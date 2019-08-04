using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPreferencias : MonoBehaviour {

	public AudioSource sonido;
	//public AudioSource musicaFondo;
	// Use this for initialization
	void Start () {
		/*int estado = PlayerPrefs.GetInt("musica");
		if(estado == 1){
			this.musicaFondo.Play();
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void establecerSonido(){
		int estado = PlayerPrefs.GetInt("sonido");

		if(estado == 1){
			this.sonido.Play();
		}
	}
}
