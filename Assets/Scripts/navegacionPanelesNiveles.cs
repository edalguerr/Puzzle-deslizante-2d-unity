using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class navegacionPanelesNiveles : MonoBehaviour {
	
	GameObject panel1;
	GameObject panel2;
	GameObject panel3;
	GameObject imagenes;
	GameObject imagenes2;
	GameObject imagenes3;
	List<GameObject> modelElements;
	List<GameObject> modelElements2;
	List<GameObject> modelElements3;
	public Button facil;
	public Button moderado;
	public Button dificil;
	// Use this for initialization
	void Start () {
	
		panel1 = GameObject.FindGameObjectWithTag("panelFacil");
		panel2 = GameObject.FindGameObjectWithTag("panelModerado");
		panel3= GameObject.FindGameObjectWithTag("panelDificil");
		imagenes = GameObject.FindGameObjectWithTag("imagenes");
		imagenes2 = GameObject.FindGameObjectWithTag("imagenes2");
		imagenes3 = GameObject.FindGameObjectWithTag("imagenes3");

		modelElements = new List<GameObject>(imagenes.transform.childCount);
        for(int i = 0; i < imagenes.transform.childCount; i++)
        {
            GameObject g = imagenes.transform.GetChild(i).gameObject;
            modelElements.Insert(i, g);
        }

		modelElements2 = new List<GameObject>(imagenes2.transform.childCount);
        for(int i = 0; i < imagenes2.transform.childCount; i++)
        {
            GameObject g = imagenes2.transform.GetChild(i).gameObject;
            modelElements2.Insert(i, g);
        }

		modelElements3 = new List<GameObject>(imagenes3.transform.childCount);
        for(int i = 0; i < imagenes3.transform.childCount; i++)
        {
            GameObject g = imagenes3.transform.GetChild(i).gameObject;
            modelElements3.Insert(i, g);
        }


		//activamos la pestaña por defecto al cargar la escena
		int panelSeleccionado = PlayerPrefs.GetInt("panelSeleccion");
		if(panelSeleccionado <= (int)buildIndexScenas.facil){
			
			this.panel1.SetActive(true);
			this.panel2.SetActive(false);
			this.panel3.SetActive(false);
		}
		else if(panelSeleccionado == (int)buildIndexScenas.moderado){
			
			this.panel1.SetActive(false);
			this.panel2.SetActive(true);
			this.panel3.SetActive(false);
		}
		else{

			this.panel1.SetActive(false);
			this.panel2.SetActive(false);
			this.panel3.SetActive(true);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		//para que se mantenga seleccionado el boton del tab actual
		if(this.panel1.activeSelf){
			this.facil.Select();
		}
		else if(this.panel2.activeSelf){
			this.moderado.Select();
		}
		else if(this.panel3.activeSelf){
			this.dificil.Select();
		}
		
	}

	public void activarPanel1(bool panel1){
		if(!panel1){
			if(this.panel1.activeSelf){
				for(int i = 0; i < modelElements.Count ; i++){
				modelElements[i].GetComponent<Image>().CrossFadeAlpha(0, 1.0f, false);
        		}	
				StartCoroutine("activarPanel1set",panel1);
			}
		}
		else{
			if(!this.panel1.activeSelf){
				StartCoroutine("activarPanel1set",panel1);
				//este playerPrefs lo agregamos para que al regresar al menu de seleccion de imagenes, se active el ultimo
				//panel de imagenes usado(facil, moderado, dificil), y favorecer la experiencia de usuario
				PlayerPrefs.SetInt("panelSeleccion",(int)buildIndexScenas.facil);
			}		
		}
		
	}

	public void activarPanel2(bool panel2){
		if(!panel2){
			if(this.panel2.activeSelf){
				for(int i = 0; i < modelElements2.Count ; i++){
				modelElements2[i].GetComponent<Image>().CrossFadeAlpha(0, 1.0f, false);
        		}	
				StartCoroutine("activarPanel2set",panel2);
			}
		}
		else{
			if(!this.panel2.activeSelf){
				StartCoroutine("activarPanel2set",panel2);
				//este playerPrefs lo agregamos para que al regresar al menu de seleccion de imagenes, se active el ultimo
				//panel de imagenes usado(facil, moderado, dificil), y favorecer la experiencia de usuario
				PlayerPrefs.SetInt("panelSeleccion",(int)buildIndexScenas.moderado);
			}
		}
	}
	public void activarPanel3(bool panel3){
		if(!panel3){
			if(this.panel3.activeSelf){
				for(int i = 0; i < modelElements3.Count ; i++){
				modelElements3[i].GetComponent<Image>().CrossFadeAlpha(0, 1.0f, false);
        		}	
				StartCoroutine("activarPanel3set",panel3);
			}
		}
		else{
			if(!this.panel3.activeSelf){
				StartCoroutine("activarPanel3set",panel3);
				//este playerPrefs lo agregamos para que al regresar al menu de seleccion de imagenes, se active el ultimo
				//panel de imagenes usado(facil, moderado, dificil), y favorecer la experiencia de usuario
				PlayerPrefs.SetInt("panelSeleccion",(int)buildIndexScenas.dificil);
			}
		}
	}

	IEnumerator activarPanel1set(bool panel1){
		yield return new WaitForSeconds(1.1f);
		this.panel1.SetActive(panel1);
		
	}

	IEnumerator activarPanel2set(bool panel2){
		yield return new WaitForSeconds(1.1f);
		this.panel2.SetActive(panel2);
		
	}

	IEnumerator activarPanel3set(bool panel3){
		yield return new WaitForSeconds(1.1f);
		this.panel3.SetActive(panel3);
		
	}
	
}
