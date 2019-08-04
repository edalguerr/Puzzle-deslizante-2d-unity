using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class navegacionEntreEscenas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	 
	// Update is called once per frame
	void Update () {
		
	}

	public void irMenu(){
		SceneManager.LoadScene((int)buildIndexScenas.main, LoadSceneMode.Single);
	}

	//los metodos sobrecargados para ir a los diferentes niveles, son usados en los botones de reiniciar en cada escena
	//y se usa el ultimo indice almacenado, el cual correspondera a la ultima imagen seleccionada, y por ende se jugara
	//de nuevo con la misma imagen
	public void irFacil(int indiceImg){
		PlayerPrefs.SetInt("imgNivelfacil",indiceImg);
		SceneManager.LoadScene((int)buildIndexScenas.facil, LoadSceneMode.Single);
	}
	//ca-app-pub-6253241462828803~8450254443
	public void irFacil(){
		SceneManager.LoadScene((int)buildIndexScenas.facil, LoadSceneMode.Single);
	}
	public void irModerado(int indiceImg){
		PlayerPrefs.SetInt("imgNivelModerado",indiceImg);
		SceneManager.LoadScene((int)buildIndexScenas.moderado, LoadSceneMode.Single);
	}

	public void irModerado(){
		SceneManager.LoadScene((int)buildIndexScenas.moderado, LoadSceneMode.Single);
	}

	public void irDificil(int indiceImg){
		PlayerPrefs.SetInt("imgNivelDificil",indiceImg);
		SceneManager.LoadScene((int)buildIndexScenas.dificil, LoadSceneMode.Single);
	}

	public void irDificil(){	
		SceneManager.LoadScene((int)buildIndexScenas.dificil, LoadSceneMode.Single);
	}

	public void irSeleccionImagen(){
		SceneManager.LoadScene((int)buildIndexScenas.menuNiveles, LoadSceneMode.Single);
	}

	
	public void irPuntajes(){
		SceneManager.LoadScene((int)buildIndexScenas.puntajes, LoadSceneMode.Single);
		//StartCoroutine("irAPuntajes");
	}

	public void salir(){
		Application.Quit();
	}

	IEnumerator irAPuntajes(){
		yield return new WaitForSeconds(0.1f);
		
	}
}
