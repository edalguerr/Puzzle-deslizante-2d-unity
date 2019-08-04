using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inicializarIntersticial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] testDeviceIDs = new string[]{"C8E78010532B7281E46C46AC5BD5C2ED"};
		EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIDs);
		EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial("ca-app-pub-6253241462828803/6198376069");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
