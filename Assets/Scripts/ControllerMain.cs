using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControllerMain : MonoBehaviour {

	bool activarSonido = false;
	bool activarMusica = false;
	public GameObject panelpreferencias;
	public GameObject panelInformacion;
	public GameObject panelFondo;
	public Sprite[] spritesSonido;
	public Button btnMusica;
	public Button btnSonido;
	//public AudioSource musicaFondo;

	Text txtMusica;
	Text txtSonido;

	// Use this for initialization
	void Start () {
		
		txtMusica = btnMusica.transform.GetChild(0).GetComponent<Text>();
		txtSonido = btnSonido.transform.GetChild(0).GetComponent<Text>();
		//this.establecerAudio();
	
		int estado = PlayerPrefs.GetInt("musica");
		Debug.Log("estado musica: "+estado);
		this.establecerMusica2();
		estado = PlayerPrefs.GetInt("sonido");
		Debug.Log("estado audio: "+estado);
		this.establecerAudio2();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void establecerMusica(){
		this.activarMusica = !this.activarMusica;

		if(this.activarMusica){
			//this.musicaFondo.Play();
			this.btnMusica.image.sprite = this.spritesSonido[(int)spritesUIAudio.activarMusica];
			this.txtMusica.text = "Musica activada";
			PlayerPrefs.SetInt("musica",1);//musica activa
		}else{
			//this.musicaFondo.Stop();
			this.btnMusica.image.sprite = this.spritesSonido[(int)spritesUIAudio.desactivarMusica];
			this.txtMusica.text = "Musica desactivada";
			PlayerPrefs.SetInt("musica",2);//musica desactivada
		}
	}

	public void establecerMusica2(){
		int estado = PlayerPrefs.GetInt("musica");

		//no asignada o activada
		if(estado == 0 || estado == 1){
			//this.musicaFondo.Play();
			this.btnMusica.image.sprite = this.spritesSonido[(int)spritesUIAudio.activarMusica];
			this.txtMusica.text = "Musica activada";
			PlayerPrefs.SetInt("musica",1);//musica activa
		}else{
			//this.musicaFondo.Stop();
			this.btnMusica.image.sprite = this.spritesSonido[(int)spritesUIAudio.desactivarMusica];
			this.txtMusica.text = "Musica desactivada";
			PlayerPrefs.SetInt("musica",2);//musica desactivada
		}
	}

	public void establecerAudio(){
		this.activarSonido = !this.activarSonido;

		if(this.activarSonido){
			this.btnSonido.image.sprite = this.spritesSonido[(int)spritesUIAudio.activarSonido];
			this.txtSonido.text = "Sonido activado";
			PlayerPrefs.SetInt("sonido",1);//sonido activo
		}
		else{
			this.btnSonido.image.sprite = this.spritesSonido[(int)spritesUIAudio.desactivarSonido];
			this.txtSonido.text = "Sonido desactivado";
			PlayerPrefs.SetInt("sonido",2);//sonido desactivado
		}

	}

	public void establecerAudio2(){
		int estado = PlayerPrefs.GetInt("sonido");

		//no asignada o activada
		if(estado == 0 || estado == 1){
			this.btnSonido.image.sprite = this.spritesSonido[(int)spritesUIAudio.activarSonido];
			this.txtSonido.text = "Sonido activado";
			PlayerPrefs.SetInt("sonido",1);//sonido activo
		}
		else{
			this.btnSonido.image.sprite = this.spritesSonido[(int)spritesUIAudio.desactivarSonido];
			this.txtSonido.text = "Sonido desactivado";
			PlayerPrefs.SetInt("sonido",2);//sonido desactivado
		}

	}

	public void activarPreferencias(){
		this.panelpreferencias.SetActive(true);
		this.panelFondo.SetActive(true);
	}

	public void cerrarPanelPreferencias(){
		this.panelpreferencias.SetActive(false);
		this.panelFondo.SetActive(false);
	}

	public void activarInformacion(){
		this.panelInformacion.SetActive(true);
		this.panelFondo.SetActive(true);
	}

	public void cerrarPanelInformacion(){
		this.panelInformacion.SetActive(false);
		this.panelFondo.SetActive(false);
	}
}
