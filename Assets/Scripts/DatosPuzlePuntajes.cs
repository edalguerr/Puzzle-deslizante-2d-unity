using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DatosPuzlePuntajes : MonoBehaviour {

	public Text txtIndice;
	public Text txtTiempo;
	public Text txtMovimientos;
	public Text txtPuntaje;

	public int indice;

	guardarCargarRecords cargarDatos;

	// Use this for initialization
	private void Awake() {
		this.cargarDatos = GameObject.FindGameObjectWithTag("GameController").GetComponent<guardarCargarRecords>();
	}

	void Start () {
		
		//actualizamos el nombre del archivo, segun el archivo que necesitemos acceder
		//para ello usamos los tags de un componente padre para identificar que archivo se debe acceder
		string tag = gameObject.GetComponentInParent<Image>().tag;
		if(tag.Equals("panelFacil")){
			this.cargarDatos.nombreArchivoDatos = this.cargarDatos.FacilArchivo;
		}
		else if(tag.Equals("panelModerado")){
			this.cargarDatos.nombreArchivoDatos = this.cargarDatos.ModeradoArchivo;
		}
		else if(tag.Equals("panelDificil")){
			this.cargarDatos.nombreArchivoDatos = this.cargarDatos.DificilArchivo;
		}

		DatosGuardar datosGuardar = cargarDatos.cargar(indice);
		if(datosGuardar != null){
			this.txtIndice.text = "#"+(datosGuardar.indiceImagen+1).ToString();
			this.txtMovimientos.text = datosGuardar.movimientos.ToString();
 
			string segundosCad =  ((datosGuardar.segundos > 9)?datosGuardar.segundos.ToString():"0"+datosGuardar.segundos);
			string minutosCad = ((datosGuardar.minutos > 9)?datosGuardar.minutos.ToString():"0"+datosGuardar.minutos);
		
			this.txtTiempo.text = minutosCad+":"+segundosCad; 
			this.txtPuntaje.text = datosGuardar.puntos.ToString();
		}
		else{
			this.txtIndice.text = "#"+(this.indice + 1).ToString();
			this.txtTiempo.text = "--:--";
			this.txtMovimientos.text ="--:--";
			this.txtPuntaje.text = "--:--";
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
