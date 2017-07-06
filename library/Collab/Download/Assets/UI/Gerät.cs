using UnityEngine;
using UnityEngine.UI;
using System;

public class Gerät{

	public String name;
	public bool active;
	public Sensor[] machineSensors;
	private GameObject obj1;
	private GameObject obj2;
	private GameObject obj3;

	//get data from agent
	public Gerät(String machineName){
		this.name = machineName;
		machineSensors = new Sensor[3];
		machineSensors[0] = new Sensor("temperature", 300, 200, this);
		machineSensors[1] = new Sensor("ultraschall", 400, 100, this);
		machineSensors[2] = new Sensor("humid", 100, 0, this);
	}

	public void showButtons(){
		obj1 = GameObject.Find("Sensor1");
		obj2 = GameObject.Find("Sensor2");
		obj3 = GameObject.Find("Sensor3");
		if(machineSensors[0] != null && obj1 != null){
			Image img = obj1.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>("Sprites/" + machineSensors[0].sensorType);
			obj1.GetComponent<Button>().onClick.AddListener(delegate{openSensorView(machineSensors[0]);});
		}
		if(machineSensors[1] != null && obj2 != null){
			Image img = obj2.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>("Sprites/" + machineSensors[1].sensorType);
			obj2.GetComponent<Button>().onClick.AddListener(delegate{openSensorView(machineSensors[1]);});
		}
		if(machineSensors[2] != null && obj3 != null){
			Image img = obj3.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>("Sprites/" + machineSensors[2].sensorType);
			obj3.GetComponent<Button>().onClick.AddListener(delegate{openSensorView(machineSensors[2]);});
		}
/*		Text txt = GameObject.Find("Sensor Button").GetComponent<Text>();
		if(txt != null){
			txt.text = machineSensors.Length.ToString ();
		}*/
	}

	public void openSensorView(Sensor currSensor){
		GameObject[] canvases = GameObject.FindGameObjectsWithTag("Gerät");
		foreach(GameObject canvas in canvases){
			canvas.GetComponent<Canvas>().enabled = false;
		}
		Canvas sensorCanvas = GameObject.Find("SensorCanvas").GetComponent<Canvas>();
		sensorCanvas.enabled = true;
		GameObject.Find("Fire KR").GetComponent<ParticleSystem>().Play();
		Text sensorName = GameObject.Find("SensorName").GetComponent<Text>();
		sensorName.text = currSensor.sensorType;
		Text maxValue = GameObject.Find("MaxValue").GetComponent<Text>();
		maxValue.text = currSensor.maxValue.ToString();
		Text minValue = GameObject.Find("MinValue").GetComponent<Text>();
		minValue.text = currSensor.minValue.ToString();
		Button backToMachine = GameObject.Find("backToMachine").GetComponent<Button>();
		backToMachine.onClick.AddListener(delegate{back(currSensor);});
	}

	private void back(Sensor currSensor){
		GameObject.Find("SensorCanvas").GetComponent<Canvas>().enabled = false;
		//Canvas obj = GameObject.Find(currSensor.parent.name).GetComponent<Canvas>();
		Canvas obj = GameObject.Find("KR QUANTEC Extra").GetComponent<Canvas>();
		obj.enabled = true;
		GameObject.Find("Fire KR").GetComponent<ParticleSystem>().Stop();
	}
}