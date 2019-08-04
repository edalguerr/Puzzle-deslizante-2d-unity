using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum buildIndexScenas{
	main =0,
	facil = 1,
	moderado,
	dificil,
	menuNiveles,
	puntajes
}

public class imagenesPatrones : MonoBehaviour {

	public List<Sprite> spritesImagen1;
	public List<Sprite> spritesImagen2;
	public List<Sprite> spritesImagen3;
	public List<Sprite> spritesImagen4;
	public List<Sprite> spritesImagen5;
	public List<Sprite> spritesImagen6;
	public List<Sprite> spritesImagen7;
	public List<Sprite> spritesImagen8;
	public List<Sprite> spritesImagen9;
	public List<Sprite> spritesImagen10;
	public List<Sprite> spritesImagen11;
	public List<Sprite> spritesImagen12;
	public List<Sprite> spritesImagen13;
	public List<Sprite> spritesImagen14;
	public List<Sprite> spritesImagen15;

	public int[] patronDesorden1;
	public int[] patronDesorden2;
	public int[] patronDesorden3;
	public int[] patronDesorden4;
	public int[] patronDesorden5;
	public int[] patronDesorden6;
	public int[] patronDesorden7;
	public int[] patronDesorden8;
	public int[] patronDesorden9;
	public int[] patronDesorden10;
	public int[] patronDesorden11;
	public int[] patronDesorden12;
	public int[] patronDesorden13;
	public int[] patronDesorden14;
	public int[] patronDesorden15;

	public List<Sprite>[] listaSprites;
	public int[][] listaPatrones;

	public int nroImagenes;
	public int cantidadPiezas;
	public int indice;
	
	
	
	// Use this for initialization
	void Awake () {	
		this.listaSprites = new List<Sprite>[nroImagenes]; 
		this.listaPatrones = new int[nroImagenes][];
		inicializarSprites();
		inicializarPatrones();
		inicializarIndice();
		
		Debug.Log("index "+ SceneManager.GetActiveScene().buildIndex);
	}
	 
	// Update is called once per frame
	void Update () {
		
	}

	void inicializarSprites(){
		this.listaSprites[0] = this.spritesImagen1;
		this.listaSprites[1] = this.spritesImagen2;
		this.listaSprites[2] = this.spritesImagen3;
		this.listaSprites[3] = this.spritesImagen4;
		this.listaSprites[4] = this.spritesImagen5;
		this.listaSprites[5] = this.spritesImagen6;
		this.listaSprites[6] = this.spritesImagen7;
		this.listaSprites[7] = this.spritesImagen8;
		this.listaSprites[8] = this.spritesImagen9;
		this.listaSprites[9] = this.spritesImagen10;
		this.listaSprites[10] = this.spritesImagen11;
		this.listaSprites[11] = this.spritesImagen12;
		this.listaSprites[12] = this.spritesImagen13;
		this.listaSprites[13] = this.spritesImagen14;
		this.listaSprites[14] = this.spritesImagen15;
			
	}

	void inicializarPatrones(){
		this.listaPatrones[0] = this.patronDesorden1;
		this.listaPatrones[1] = this.patronDesorden2;
		this.listaPatrones[2] = this.patronDesorden3;
		this.listaPatrones[3] = this.patronDesorden4;
		this.listaPatrones[4] = this.patronDesorden5;
		this.listaPatrones[5] = this.patronDesorden6;
		this.listaPatrones[6] = this.patronDesorden7;
		this.listaPatrones[7] = this.patronDesorden8;
		this.listaPatrones[8] = this.patronDesorden9;
		this.listaPatrones[9] = this.patronDesorden10;
		this.listaPatrones[10] = this.patronDesorden11;
		this.listaPatrones[11] = this.patronDesorden12;
		this.listaPatrones[12] = this.patronDesorden13;
		this.listaPatrones[13] = this.patronDesorden14;
		this.listaPatrones[14] = this.patronDesorden15;

	}

	void inicializarIndice(){
		Scene actual = SceneManager.GetActiveScene();

		//obtenemos el indice de la imagen, el cual ah sido almacenado desde la escena de seleccion de niveles
		//los indices de las imagenes van desde el cero(0) en adelante
		if(actual.buildIndex == (int)buildIndexScenas.facil){
			this.indice = PlayerPrefs.GetInt("imgNivelfacil");
		}
		else if(actual.buildIndex == (int)buildIndexScenas.moderado){
			this.indice = PlayerPrefs.GetInt("imgNivelModerado");
		}
		else if(actual.buildIndex == (int)buildIndexScenas.dificil){
			this.indice = PlayerPrefs.GetInt("imgNivelDificil");
		}
	}

	//regresa la imagen seleccionada
	public List<Sprite> imagenSeleccionada(){
		return this.listaSprites[indice];
	}

	//regresa el patron asignado a la imagen con el indice indicado
	public int[] patronSelecionado(){
		return this.listaPatrones[indice];
	}
}
