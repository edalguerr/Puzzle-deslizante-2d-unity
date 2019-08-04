using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerFelicitaciones : MonoBehaviour {
	public Text txtMovimientos;
	public Text txtTiempo;
	public Text txtPuntos;
	int mov, min, seg;
	float puntos;
	string segundosCad, minutosCad;
	GameController GControlle;
	// Use this for initialization
	void Start () {
	
		GControlle = gameObject.GetComponent<GameController>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GControlle.finJuego)
		{
			mov = PlayerPrefs.GetInt("movimientos");
			min = PlayerPrefs.GetInt("mins");
			seg = PlayerPrefs.GetInt("segs");
			puntos = PlayerPrefs.GetFloat("puntos");

			segundosCad = ((seg > 9)?seg.ToString():"0"+seg);
			minutosCad =  ((min > 9)?min.ToString():"0"+min);
			txtMovimientos.text = ": "+((mov > 9)?mov.ToString():"0"+mov);
			txtTiempo.text = minutosCad+":"+segundosCad; 
			txtPuntos.text = puntos+" Puntos";
		}
		
	}
}
