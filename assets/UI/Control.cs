using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Control : MonoBehaviour {

	public static Control obj;
	//data from sensors
	public GameObject[] buttons;
	public Gerät currMachine;
	private int amountOfSensors;
	private static Sensor sensor;

	private Text sensorText;

	void Awake(){
		//Singleton
		if(obj == null){
			DontDestroyOnLoad (gameObject);
			obj = this;
		}else if(obj != this){
			Destroy(gameObject);
		}
		//deactivate sensor buttons upper right corner
		buttons = GameObject.FindGameObjectsWithTag("gerätesensor");
		GameObject[] canvases = GameObject.FindGameObjectsWithTag("Canvas");
		foreach(GameObject can in canvases){
			Canvas canvas = can.GetComponent<Canvas>();
			canvas.enabled = false;
		}
		//disable button menu
		GameObject.Find("SensorsButtonGroup").SetActive(false);
		GameObject.Find ("Text KR").SetActive (false);
	}

	//read and write from File
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = File.Create(Application.persistentDataPath + "datastorage.dat");
		Data data = new Data();
		//add information to the instance
		bf.Serialize(stream, data);
		stream.Close();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "datastorage.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = File.Open(Application.persistentDataPath + "datastorage.dat", FileMode.Open);
			Data data = (Data) bf.Deserialize(stream);
			stream.Close();
			//add information from file back to object
		}
	}		
}

[Serializable]
class Data{
	//contains data to save in a file
	public String sensorType;
	public int amountOfSensors;
	public int curValue;
	public int maxValue;
	public int minValue;
}