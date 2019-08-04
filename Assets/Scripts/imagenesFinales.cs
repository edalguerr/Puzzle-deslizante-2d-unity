using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagenesFinales : MonoBehaviour {

	public List<Sprite> imagenesFinalesList;
	private Image imageGO;
	private imagenesPatrones imagenesPatronesGO;
	
	private void Awake() {
		this.imageGO = GameObject.FindGameObjectWithTag("imgGanar").GetComponent<Image>();
		this.imagenesPatronesGO = gameObject.GetComponent<imagenesPatrones>();
		this.imageGO.sprite = this.imagenesFinalesList[imagenesPatronesGO.indice];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
