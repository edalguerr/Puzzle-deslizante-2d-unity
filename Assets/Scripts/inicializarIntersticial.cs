using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inicializarIntersticial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string[] testDeviceIDs = new string[]{"test device id"};
		EasyGoogleMobileAds.GetInterstitialManager().SetTestDevices(true, testDeviceIDs);
		EasyGoogleMobileAds.GetInterstitialManager().PrepareInterstitial("");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
