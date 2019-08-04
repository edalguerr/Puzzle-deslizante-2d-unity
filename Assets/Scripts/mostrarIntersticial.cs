using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mostrarIntersticial : MonoBehaviour {
	
	int nVeces = 0;
	int nVecesPunt = 0;

	// Use this for initialization
	void Start () {
		if(SceneManager.GetActiveScene().buildIndex == (int)buildIndexScenas.puntajes){
		  nVecesPunt = PlayerPrefs.GetInt("nPuntajes");
		}
		else if(SceneManager.GetActiveScene().buildIndex != (int)buildIndexScenas.puntajes){
		  nVeces = PlayerPrefs.GetInt("nNiveles");
		}

		if(nVeces >= 4 || nVecesPunt >= 4){
			EasyGoogleMobileAds.GetInterstitialManager().ShowInterstitial();
			
			if(nVeces >= 4)
				PlayerPrefs.SetInt("nNiveles", 0);
			if(nVecesPunt >= 4)
				PlayerPrefs.SetInt("nPuntajes", 0);

			Debug.Log("punt: "+nVecesPunt+":niv: "+nVeces);
		}	
	}
	 
	// Update is called once per frame
	void Update () {
		
	}
}
