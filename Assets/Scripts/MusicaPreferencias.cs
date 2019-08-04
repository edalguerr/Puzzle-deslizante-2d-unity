using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaPreferencias : MonoBehaviour {

	AudioSource musicaFondo;
	static int inicial =0;
	void Awake()
	{	
		if(MusicaPreferencias.inicial == 0){
			MusicaPreferencias.inicial = 1;
		}
		else{
			Destroy(gameObject);
		}
		this.musicaFondo = gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		int estado = PlayerPrefs.GetInt("musica");
		
		if(estado == 0 || estado == 1){
			if(!this.musicaFondo.isPlaying){
				this.musicaFondo.Play();
				Debug.Log("iniciando");
			}
		}
		else{
			this.musicaFondo.Stop();
			Debug.Log("detenida");
		}
	}
}
