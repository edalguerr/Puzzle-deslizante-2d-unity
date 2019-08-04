using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class guardarCargarRecords : MonoBehaviour {
	public string nombreArchivoDatos;
    private const string facilArchivo = "facil";
    private const string moderadoArchivo = "moderado";
    private const string dificilArchivo = "dificil";

    public string FacilArchivo
    {
        get
        {
            return facilArchivo;
        }
    }

    public  string ModeradoArchivo
    {
        get
        {
            return moderadoArchivo;
        }
    }

    public  string DificilArchivo
    {
        get
        {
            return dificilArchivo;
        }
    }

   
    // Use this for initialization
    void Start () {
		//File.Delete(Application.persistentDataPath+"/facil");
		//File.Delete(Application.persistentDataPath+"/facil2");
		//File.Delete(Application.persistentDataPath+"/facil3");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void guardar(int indiceImagen, int movimientos, int minutos, int segundos,float puntos){
		FileStream file = null;
		try{
			BinaryFormatter bf = new BinaryFormatter();
			file = new FileStream(Application.persistentDataPath+"/"+this.nombreArchivoDatos, FileMode.Append, FileAccess.Write);
			
			DatosGuardar datos = new DatosGuardar(indiceImagen,movimientos,minutos,segundos,puntos);
			bf.Serialize(file,datos);

		}catch(Exception e){
			Debug.Log("ha ocurrido un error: "+e.Message);


		}finally{
			file.Close();
		}
		
	}

	public DatosGuardar cargar(int indiceImagen){
		FileStream file = null;
		try{

			BinaryFormatter bf = new BinaryFormatter();
			file = new FileStream(Application.persistentDataPath+"/"+this.nombreArchivoDatos, FileMode.OpenOrCreate, FileAccess.Read);
			DatosGuardar datos = (DatosGuardar)bf.Deserialize(file);

			while(datos != null){
				if(indiceImagen == datos.indiceImagen){
					return datos;
				}
				datos = (DatosGuardar)bf.Deserialize(file);
			}

		}catch(System.Runtime.Serialization.SerializationException ){

		}catch(Exception e){
			Debug.Log("ha ocurrido un error: "+e.Message);
			Debug.Log("entro al exception: "+ e.GetType());
			Debug.Log("entro al exception: "+ e.Source);

		}finally{
			file.Close();
		}

		//en caso que la imagen no se haya encontrado
		return null;
	}

	//devuelve una lista con los datos de los records de cada imagen
	public List<DatosGuardar> cargarTodos(){
		FileStream file = null;
		List<DatosGuardar> datosRecordsList = null;

		try{

			BinaryFormatter bf = new BinaryFormatter();
			file = new FileStream(Application.persistentDataPath+"/"+this.nombreArchivoDatos, FileMode.OpenOrCreate, FileAccess.Read);
			
			DatosGuardar datos = (DatosGuardar)bf.Deserialize(file);
			datosRecordsList = new List<DatosGuardar>();

			while(datos != null){
				datosRecordsList.Add(datos);
				datos = (DatosGuardar)bf.Deserialize(file);
			}

		}catch(System.IO.EndOfStreamException ){

		}catch(Exception e){
			Debug.Log("ha ocurrido un error: "+e.Message);
			Debug.Log("entro al exception: "+ e.GetType());
			Debug.Log("entro al exception: "+ e.Source);

		}finally{
			file.Close();
		}

		//devuelve una lista con los datos de los records de cada imagen
		return datosRecordsList;
	}

	public void actualizar(DatosGuardar nuevo){
		FileStream file = null, temporal = null;
		bool error = false;
		try{

			BinaryFormatter bf = new BinaryFormatter();
			BinaryFormatter bfTemporal = new BinaryFormatter();
			file = new FileStream(Application.persistentDataPath+"/"+this.nombreArchivoDatos, FileMode.OpenOrCreate, FileAccess.Read);
			temporal = new FileStream(Application.persistentDataPath+"/temporal", FileMode.Create, FileAccess.Write);
			
			DatosGuardar datos = (DatosGuardar)bf.Deserialize(file);

			while(datos != null){
				if(nuevo.indiceImagen == datos.indiceImagen){
					bfTemporal.Serialize(temporal,nuevo);
				}else{
					bfTemporal.Serialize(temporal,datos);
				}
				datos = (DatosGuardar)bf.Deserialize(file);
			}

		}catch(System.IO.EndOfStreamException ){

		}catch(Exception e){
			Debug.Log("ha ocurrido un error: "+e.Message);
			Debug.Log("entro al exception: "+ e.GetType());
			Debug.Log("entro al exception: "+ e.Source);

			error = true;

		}finally{
			file.Close();
			temporal.Close();

			if(!error){
				//eliminando el archivo con informacion desactualiada, luego se renombra el acrhivo que tiene la 
				//informacion actualizada
				File.Delete(Application.persistentDataPath+"/"+this.nombreArchivoDatos);
				File.Move(Application.persistentDataPath+"/temporal", Application.persistentDataPath+"/"+this.nombreArchivoDatos);
			}
		}
	}
}

[Serializable]
public class DatosGuardar
{
	public int indiceImagen;
	public int movimientos;
	public int minutos;
	public int segundos;
	public float puntos;
	public DatosGuardar(int indiceImagen, int movimientos, int minutos, int segundos, float puntos)
	{
		this.indiceImagen = indiceImagen;
		this.movimientos = movimientos;
		this.minutos = minutos;
		this.segundos = segundos;
		this.puntos = puntos;
	}
}
