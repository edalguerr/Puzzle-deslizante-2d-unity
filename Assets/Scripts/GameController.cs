using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum spritesUIAudio{
		activarSonido,
		desactivarSonido,
		activarMusica,
		desactivarMusica
	}

public class GameController : MonoBehaviour {
	
	public int correctasAux;
	private guardarCargarRecords guardarCargarDatos;
	public Animator panelGanar;
	public GameObject panelAux;
	private imagenesPatrones imagenesPatronesGO;
	private bool desactivarSonido = false;
	private bool desplegarMusica = true;
	private bool desplegarAjustes = false;
	private AudioSource movimientoSonido;
	//private AudioSource musicaFondo;
	
	public AudioClip[] sonidos;
	private Image btnMusicaImg;
	private Image btnSonidoMovimientosImg;
	public Text movimientos;
	public Text tiempo;
	public Slider slideProgreso;
	public GameObject panelMenu;
	public GameObject btnSiguienteScena;
	[HideInInspector]
	public int col,row;
	public int sizeRow;
	public int sizeCol;
	public int posxBlank;
	public int posyBlank;
	public int posxBlankFinal, posyBlankFinal;
	private int countPoint = 0;

	private int indice;

	[HideInInspector]
	public StepByStepController actual;
	
	[HideInInspector]
	public bool finJuego = false;

	[HideInInspector]
	public bool verificarClick = false;
	public List<GameObject> imageKeyList;
	public List<GameObject> imageOfPictureList;
	public List<GameObject> checkPointList;
	
	public  GameObject[,] imageKeyArray;
	public GameObject[,] imageOfPictureArray;
	public GameObject[,] checkPointArray;
	public GameObject txtRecordGO;

	//indice en los array para identificar a el srpite blank
	//tiene valor de cero en los arrays
	int indBlank = 0;
	//posicion del sprite blank en la lista de sprites
	public int posicionBlank;
	//public string spriteBlankNombre = "blank";

	[HideInInspector]
	public List<Sprite> listaActual;
	[HideInInspector]
	public List<Sprite> spritesImagen1;
	
	//lista de sprites usados en el boton desplegable de ajustes que activa y desactiva
	//el sonido y la musica; estan almacenados en el enum spritesUIAudio
	public List<Sprite> sonidoMusicaUI;
	private int cantMovimientos = 0;
	// Use this for initialization
	DateTime inicialTiempo, finTiempo;
	TimeSpan dif;
	int segundos, minutos;
	string segundosCad, minutosCad;

	
	private void Awake() {
		int nveces = PlayerPrefs.GetInt("nNiveles");
		PlayerPrefs.SetInt("nNiveles", 1+nveces);
		
		this.btnMusicaImg = GameObject.FindGameObjectWithTag("btnMusica").GetComponent<Image>();
		this.btnSonidoMovimientosImg = GameObject.FindGameObjectWithTag("btnSonidoMovimiento").GetComponent<Image>();
		this.panelMenu.SetActive(false);
		this.imagenesPatronesGO = gameObject.GetComponent<imagenesPatrones>();
	}
	void Start () {
		this.guardarCargarDatos = gameObject.GetComponent<guardarCargarRecords>();
		this.indice = this.imagenesPatronesGO.indice;
		//Debug.Log(this.imagenesPatronesGO.indice+" :indice: "+this.indice);
		if(this.indice+1 >= this.imagenesPatronesGO.nroImagenes){
			this.btnSiguienteScena.SetActive(false);
		}

		this.movimientos.text = ": 00";
		inicializarImagen();
		inicializandoArrays();	
		this.movimientoSonido = this.gameObject.GetComponent<AudioSource>();

		//accedemos al segundo audio source del objeto
		//this.musicaFondo = this.gameObject.GetComponents<AudioSource>()[1];
		this.inicialTiempo = DateTime.Now;
		this.slideProgreso.minValue = 0f;
		this.slideProgreso.maxValue = this.sizeCol * this.sizeRow;
		
		this.establecerMusica2();
		this.establecerSonido2();
	}
	
	// Update is called once per frame
	void Update () {
		if(!this.finJuego){
			//obteniendo tiempo en la partida
			this.finTiempo = DateTime.Now;
			dif = this.finTiempo-this.inicialTiempo;
			this.segundos = (int)dif.Seconds;
			this.minutos = (int)dif.TotalMinutes;

			//actualizando y dandole formato al texto que muestra el tiempo
			this.segundosCad =  ((this.segundos > 9)?this.segundos.ToString():"0"+this.segundos);
			this.minutosCad = ((this.minutos > 9)?this.minutos.ToString():"0"+this.minutos);
			this.tiempo.text = this.minutosCad+":"+this.segundosCad; 
		}

		if(this.verificarClick && !this.finJuego){
			//Debug.Log(this.posxBlank+" : "+this.posyBlank);
			//Debug.Log(this.row+" : "+this.col);
			if(this.posxBlank != this.row || this.posyBlank != this.col){ //verificamos que no fue clickeado la casilla blank
				
				
				//Debug.Log("entro");
		
				if(this.posxBlank != this.row && this.posyBlank == this.col){
					if(Mathf.Abs(this.posxBlank - this.row) == 1f){//nos aseguramos que mueva la casilla contigua
						//movemos en vertical
						this.moverSprites();
					}
						
				}
				else if(this.posxBlank == this.row && this.posyBlank != this.col){
					if(Mathf.Abs(this.posyBlank-this.col) == 1f){
						//movemos en horizontal
						this.moverSprites();
					}

				}

				//verificamos si el rompecabezas esta completado
				if(this.posxBlankFinal == this.posxBlank && this.posyBlank == this.posyBlankFinal){
					
					int contCorrectas = this.verificarCantidadCorrectas();
					
					if(!(contCorrectas == (this.sizeCol * this.sizeRow))){
						//TO DO: eliminar este if
						contCorrectas = this.correctasAux;
					}
					//si todas las fichas estan correctas el nivel está completado
					if(contCorrectas == (this.sizeCol * this.sizeRow)){
						this.finJuego = true; //activamos la variable que finaliza el juego(armado del puzzle) 
		
						//aqui calculamos los puntos obtenidos
						float maxTiempo = 180f; //minutos = 3 horas
						float maxMovimientos = 5000f;
						float porcTiempo = 0.65f, porcMovimientos = 0.35f;
						float tiempoFormato = (int)this.dif.TotalMinutes+(this.dif.Seconds/100f); //tiempo en formato decimal ej: 4.3 minutos = 4:30  
						//float puntuacionPerfecta = 1864.2f;
						
						float puntos = (maxMovimientos-(float)this.cantMovimientos)*porcMovimientos + (maxTiempo-tiempoFormato)*porcTiempo;
						bool record = false;
						//guardando datos sencillos
						PlayerPrefs.SetInt("mins", (int)this.dif.TotalMinutes);
						PlayerPrefs.SetInt("segs",(int)this.dif.Seconds);
						PlayerPrefs.SetInt("movimientos",this.cantMovimientos);
						PlayerPrefs.SetFloat("puntos",puntos);

						Debug.Log("Felicidades has Ganado");
						Debug.Log("puntos: "+ puntos);

						//aqui llamaremos al metodo que guardara la informacion en el archivo y verificaremos si se 
						//ha fijado un nuevo record
						DatosGuardar datos = this.guardarCargarDatos.cargar(this.indice);
						
						if(datos == null){
							//cuando no se encuentre datos de la imagen, entonces se fijará un nuevo record, ya que debe ser la primera vez que se juega con dicha imagen
							this.guardarCargarDatos.guardar(this.indice, this.cantMovimientos, (int)this.dif.TotalMinutes, this.dif.Seconds, puntos);
							Debug.Log("nuevo record: "+puntos);
							record = true;
							//this.txtRecordGO.SetActive(true);
						}
						else{
							if(puntos > datos.puntos){
								//fijamos un nuevo record
								this.guardarCargarDatos.actualizar(new DatosGuardar(this.indice, this.cantMovimientos,(int)this.dif.TotalMinutes, this.dif.Seconds, puntos));
								Debug.Log("record anterior: "+datos.puntos);
								Debug.Log("nuevo record: "+puntos);
								record = true;
								//this.txtRecordGO.SetActive(true);
							}
							Debug.Log("record anterior: "+datos.puntos);
						}

						int estado = PlayerPrefs.GetInt("sonido");
						Debug.Log("estado audio completado: "+estado);
						if(estado == 1){
							//!this.desactivarSonido
							//reproduciendo sonido de puzzle completado
							int indSonidoGanar = 1;
							this.movimientoSonido.PlayOneShot(this.sonidos[indSonidoGanar]);
							//this.StartCoroutine("reproducirAplausos");
						}
						this.StartCoroutine("irAFelicitaciones",record);			
					}
					//Debug.Log(contCorrectas+" : "+this.sizeCol);
					//Debug.Log("fin: "+this.finJuego);
				}
			}
			this.verificarClick = false;
		}
	}

	private int verificarCantidadCorrectas(){
		int contCorrectas = 0;
		Sprite tmpKey, tmpPicture;
		bool error = false;

		for (int i = 0; i < this.sizeRow; i++)
		{
			for (int j = 0; j < this.sizeCol; j++)
			{	
				tmpKey = this.imageKeyArray[i,j].GetComponent<SpriteRenderer>().sprite;
				tmpPicture = this.imageOfPictureArray[i, j].GetComponent<SpriteRenderer>().sprite;

				if(tmpKey != tmpPicture){
					error = true;
				}
				else{
					contCorrectas++;
				}
			}

		}

		//todas las fichas aun no estan completamente ordenadas, el nivel aun no finaliza
		if(error){
			this.finJuego =false;
		}

		return contCorrectas;
	}
	public void establecerSonido(){
		
		this.desactivarSonido = !this.desactivarSonido;
		if(this.desactivarSonido){
			this.btnSonidoMovimientosImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.desactivarSonido];
			PlayerPrefs.SetInt("sonido",2);//sonido desactivado
		}
		else{
			this.btnSonidoMovimientosImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.activarSonido];
			PlayerPrefs.SetInt("sonido",1);//sonido activo
		}
	}

	public void establecerSonido2(){
		
		int estado = PlayerPrefs.GetInt("sonido");
		//no asignada o activada
		if(estado == 2){
			this.btnSonidoMovimientosImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.desactivarSonido];
			PlayerPrefs.SetInt("sonido",2);//sonido desactivado
		}
		else{
			this.btnSonidoMovimientosImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.activarSonido];
			PlayerPrefs.SetInt("sonido",1);//sonido activo
		}
	}

	public void establecerMusica(){
		this.desplegarMusica = !this.desplegarMusica;

		if(this.desplegarMusica){
			//this.musicaFondo.Play();
			this.btnMusicaImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.activarMusica];
			PlayerPrefs.SetInt("musica",1);//musica activa
		}
		else{
			//this.musicaFondo.Stop();
			this.btnMusicaImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.desactivarMusica];
			PlayerPrefs.SetInt("musica",2);//musica desactivada
		}		
		
	}

	public void establecerMusica2(){
		int estado = PlayerPrefs.GetInt("musica");

		//no asignada o activada
		if(estado == 0 || estado == 1){
			//this.musicaFondo.Play();
			this.btnMusicaImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.activarMusica];
			PlayerPrefs.SetInt("musica",1);//musica activa
		}
		else{
			//this.musicaFondo.Stop();
			this.btnMusicaImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.desactivarMusica];
			PlayerPrefs.SetInt("musica",2);//musica desactivada
		}		
		
	}
	
	
	public void irFelicitaciones(bool record){
		//SceneManager.LoadScene("felicitaciones",LoadSceneMode.Single);
		panelAux.SetActive(true);
		this.txtRecordGO.SetActive(record);
		this.panelGanar.SetBool("activar",true);
		
		Debug.Log("record valor: "+record);
		int estado = PlayerPrefs.GetInt("sonido");
		Debug.Log("estado audio felicitaciones: "+estado);
		//sonido activo
		if(estado == 1){
			//!this.desactivarSonido
			//reproduciendo sonido de puzzle completado
			int indSonidoGanar = 2;
			this.movimientoSonido.PlayOneShot(this.sonidos[indSonidoGanar]);
			//this.StartCoroutine("reproducirAplausos");
		}
		
	}
	void inicializarImagen(){	
		//inicializando la imagen key(ordenada), segun la imagen seleccionada por el usuario en el menu de imagenes
		this.spritesImagen1 = this.imagenesPatronesGO.imagenSeleccionada();
		this.inicializarImageKey();

		this.inicializarListaDesorden(this.imagenesPatronesGO.patronSelecionado());
		//this.inicializarListaDesorden(this.patronDesorden1);
		
		
	}

	void inicializarImageKey(){
		for (int i = 0; i < this.spritesImagen1.Count ; i++)
		{	this.imageKeyList[i].GetComponent<SpriteRenderer>().sprite = this.spritesImagen1[i];
			//this.imageKeyList[i].GetComponent<SpriteRenderer>().sprite = this.spritesImagen2[i];
		}
	}
	void inicializarListaDesorden(int []imagenes){
		for (int i = 0; i < imagenes.Length; i++)
		{
			if(imagenes[i] == this.indBlank){
				this.listaActual.Add(this.spritesImagen1[this.posicionBlank]);
				//this.listaActual.Add(this.spritesImagen2[this.posicionBlank]);
			}
			else{
				this.listaActual.Add(this.spritesImagen1[imagenes[i]-1]);	
				//this.listaActual.Add(this.spritesImagen2[imagenes[i]-1]);	
			}
			
		}
	}

	IEnumerator irAFelicitaciones(bool record){
		yield return new WaitForSeconds(1f);
		this.irFelicitaciones(record);
	}
	void inicializandoArrays(){
		//initialization de arrays
		this.imageKeyArray = new GameObject[this.sizeRow, this.sizeRow];
		this.imageOfPictureArray = new GameObject[this.sizeRow, this.sizeCol];
		this.checkPointArray = new GameObject[this.sizeRow, this.sizeCol];

		StepByStepController tmp;
		
		for (int i = 0; i < this.sizeRow; i++) //sizeRow run
		{
			for (int j = 0; j < this.sizeCol; j++) //sizeCol run
			{
				
				this.checkPointArray[i, j] = this.checkPointList[this.countPoint];
				this.imageKeyArray[i, j] = this.imageKeyList[this.countPoint];
				this.imageOfPictureArray[i, j] = this.imageOfPictureList[this.countPoint];
				
				//asignando imagenes en desorden segun el patron asignado
				this.imageOfPictureArray[i, j].GetComponent<SpriteRenderer>().sprite = this.listaActual[this.countPoint];
				
				//asignando posiciones
				tmp = this.checkPointArray[i,j].GetComponent<StepByStepController>();
				tmp.col = j;
				tmp.row = i;
				this.countPoint++;
			}
		}
		
	}

	void moverSprites(){
		Sprite tmp;
		int estado = PlayerPrefs.GetInt("sonido");
		Debug.Log("estado audio movimiento: "+estado);
		if(estado == 1){
			//!this.desactivarSonido
			//reproducir sonido de movimiento
			int indSonidoMov = 0;
			this.movimientoSonido.PlayOneShot(this.sonidos[indSonidoMov]);
		}
		
		//Debug.Log("mover");
		this.cantMovimientos++;
		this.movimientos.text = ": "+((this.cantMovimientos > 9)?this.cantMovimientos.ToString():"0"+this.cantMovimientos);
		//actualizando sprites
		tmp = this.imageOfPictureArray[posxBlank, posyBlank].GetComponent<SpriteRenderer>().sprite;
		this.imageOfPictureArray[posxBlank, posyBlank].GetComponent<SpriteRenderer>().sprite = this.imageOfPictureArray[row, col].GetComponent<SpriteRenderer>().sprite;
		this.imageOfPictureArray[row, col].GetComponent<SpriteRenderer>().sprite = tmp;

		//actualizando posiciones del elemento blank
		this.posxBlank = this.actual.row;
		this.posyBlank = this.actual.col;

		//actualizando valor del slide de progreso
		this.slideProgreso.value = this.verificarCantidadCorrectas();
	}

	public void desplegarAjustesPanel(){
		this.desplegarAjustes = !this.desplegarAjustes;
		this.panelMenu.SetActive(this.desplegarAjustes);
	}

	public void irSiguiente(){

		if(this.indice+1 < this.imagenesPatronesGO.nroImagenes){
		
			Scene actual = SceneManager.GetActiveScene();

			//asignamos el indice de la imagen, el cual será almacenado en memoria temporalmente
			//el cual será usado para cargar la escena con la siguiente imagen
			//los indices de las imagenes van desde el cero(0) en adelante
			if(actual.buildIndex == (int)buildIndexScenas.facil){
				PlayerPrefs.SetInt("imgNivelfacil",1+this.indice);
				//Debug.Log("resultado: "+(1+this.indice));
				//Debug.Log("resultado2: "+PlayerPrefs.GetInt("imgNivelfacil"));
				SceneManager.LoadScene((int)buildIndexScenas.facil, LoadSceneMode.Single);
			}
			else if(actual.buildIndex == (int)buildIndexScenas.moderado){
				PlayerPrefs.SetInt("imgNivelModerado",1+this.indice);
				SceneManager.LoadScene((int)buildIndexScenas.moderado, LoadSceneMode.Single);
			}
			else if(actual.buildIndex == (int)buildIndexScenas.dificil){
				PlayerPrefs.SetInt("imgNivelDificil",1+this.indice);
				SceneManager.LoadScene((int)buildIndexScenas.dificil, LoadSceneMode.Single);
			}
		}
	}
}
