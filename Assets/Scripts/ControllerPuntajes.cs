using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPuntajes : MonoBehaviour {

	float puntosFacil = 0f;
	float puntosModerado = 0f;
	float puntosDificil = 0f;
	float puntajeTotal = 0f;

	int valueAnterior;
	public Text txtPuntosSubTotal;
	public Text txtPuntosTotal;
	
	Dropdown desplegableNivel;
	guardarCargarRecords cargarDatos;
	List<DatosGuardar> datosList;

	GameObject panelFacil;
	GameObject panelModerado;
	GameObject panelDificil;
	private void Awake() {
		int nveces = PlayerPrefs.GetInt("nPuntajes");
		PlayerPrefs.SetInt("nPuntajes", 1+nveces);
		
		this.desplegableNivel = GameObject.FindGameObjectWithTag("desplegablePuntos").GetComponent<Dropdown>();
		this.cargarDatos = new guardarCargarRecords();

		//obteniendo referencia a los paneles
		this.panelFacil = GameObject.FindGameObjectWithTag("panelFacil");
		this.panelModerado = GameObject.FindGameObjectWithTag("panelModerado");
		this.panelDificil = GameObject.FindGameObjectWithTag("panelDificil");
	}

	// Use this for initialization
	void Start () {
		//iniciamos las puntuaciones de cada nivel 
		this.inicializarPuntaje(this.cargarDatos.FacilArchivo,ref this.puntosFacil);
		this.inicializarPuntaje(this.cargarDatos.ModeradoArchivo,ref this.puntosModerado);
		this.inicializarPuntaje(this.cargarDatos.DificilArchivo,ref this.puntosDificil);

		//inicializamos la puntuacion total
		this.puntajeTotal = this.puntosFacil + this.puntosModerado + this.puntosDificil;
		this.txtPuntosTotal.text = "Puntaje total: "+this.puntajeTotal;

		//obtenemos el valor del value inicial del dropdown
		this.valueAnterior = this.desplegableNivel.value;
		this.txtPuntosSubTotal.text = "Puntos facil: "+this.puntosFacil;

		this.seleccionPanelActualizar();
	}
	
	// Update is called once per frame
	void Update () {
		//verificamos que el valor seleccionado por el usuario sea diferente del ya establecido, para entonces
		//si modificar el texo a mostrarle como subtotal
		if(this.valueAnterior != this.desplegableNivel.value){

			//modificamos el valor del texto que muestra el subtotal de puntos, dependiendo la opcion
			//seleccionada por el usuario
			if(this.desplegableNivel.value == (int)buildIndexScenas.facil-1){
				this.txtPuntosSubTotal.text = "Puntos facil: "+this.puntosFacil;

			}else if(this.desplegableNivel.value == (int)buildIndexScenas.moderado-1){
				this.txtPuntosSubTotal.text = "Puntos moderado: "+this.puntosModerado;
				
			}else if(this.desplegableNivel.value == (int)buildIndexScenas.dificil-1){
				this.txtPuntosSubTotal.text = "Puntos dificil: "+this.puntosDificil;
				
			}

			//actualizamos el valor de la ultima seleccion, que corresponderá a la actual
			this.valueAnterior = this.desplegableNivel.value;
		}
	}

	void inicializarPuntaje(string archivo,ref float puntaje){
		this.cargarDatos.nombreArchivoDatos = archivo;
		this.datosList = this.cargarDatos.cargarTodos();

		if(this.datosList != null){
			foreach (var item in datosList){
				puntaje += item.puntos;
			}
		}	
	}

	public void seleccionPanelActualizar(){
		//activamos o desactivamos los paneles que contienen los elementos con la representacion de la puntuacion
		//lo hacemos dependiendo la seleccion que se haya realizado por el usuario en el dropdown		
		if(this.desplegableNivel.value == (int)buildIndexScenas.facil-1){
			this.panelFacil.SetActive(true);
			this.panelModerado.SetActive(false);
			this.panelDificil.SetActive(false);
		}else if(this.desplegableNivel.value == (int)buildIndexScenas.moderado-1){
			this.panelModerado.SetActive(true);
			this.panelFacil.SetActive(false);
			this.panelDificil.SetActive(false);
		}else if(this.desplegableNivel.value == (int)buildIndexScenas.dificil-1){
			this.panelDificil.SetActive(true);
			this.panelModerado.SetActive(false);
			this.panelFacil.SetActive(false);
		}
	}
}
