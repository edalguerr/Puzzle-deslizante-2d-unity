using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepController : MonoBehaviour {
	public GameController gameController;
	public int col, row;
	// Use this for initialization
	void Start () {
		this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
		this.gameController.col = this.col;
		this.gameController.row = this.row;
		this.gameController.actual = this;
		this.gameController.verificarClick = true;
	}
}
