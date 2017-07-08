using UnityEngine;
using UnityEngine.UI;
using System;

public class Gerät : ScriptableObject {

	public Sensor[] machineSensors;
	//private String description;
	private String name;
	private GameObject obj1;
	private GameObject obj2;
	private GameObject obj3;

	//get id from picture used and then get data from agent
	public Gerät(String machineName/*, String descr*/){
		this.name = machineName;
		//this.description = descr;
		machineSensors = new Sensor[3];
		machineSensors[0] = new Sensor("temperatur", 300, 200, this);
		machineSensors[1] = new Sensor("ultraschall", 400, 100, this);
		machineSensors[2] = new Sensor("uv", 0, 200, this);
		//textOnCanvas();
	}

	private void textOnCanvas(){
		GameObject.Find("Gerätname").GetComponent<Text>().text = name;
		String sens = "";
		for(int i = 0; i < machineSensors.Length; i++){
			sens = sens + machineSensors[i].sensorType;
			if (i != (machineSensors.Length - 1))
				sens += ", ";
		}
		GameObject.Find("Sensoren").GetComponent<Text>().text = sens;
	}

	public void showButtons(){
		obj1 = GameObject.Find("Sensor1");
		obj2 = GameObject.Find("Sensor2");
		obj3 = GameObject.Find("Sensor3");
		int amountOfSensors = 0;
		if(machineSensors[0] != null && obj1 != null){
			Image img = obj1.GetComponent<Image>();
			img.sprite = Resources.Load<Sprite>("Sprites/" + machineSensors[0].sensorType);
			obj1.GetComponent<Button>().onClick.AddListener(delegate{openSensorView(machineSensors[0]);});
			amountOfSensors++;
		}
		if (machineSensors[1] != null) {
			if(obj2 != null){
				Image img = obj2.GetComponent<Image>();
				img.sprite = Resources.Load<Sprite>("Sprites/" + machineSensors[1].sensorType);
				obj2.GetComponent<Button>().onClick.AddListener(delegate{openSensorView(machineSensors[1]);});
				amountOfSensors++;
			}
		}else{
			obj2.SetActive (false);
		}
		if(machineSensors[2] != null){
			if(obj3 != null){
				Image img = obj3.GetComponent<Image>();
				img.sprite = Resources.Load<Sprite>("Sprites/" + machineSensors[2].sensorType);
				obj3.GetComponent<Button>().onClick.AddListener(delegate{openSensorView(machineSensors[2]);});
				amountOfSensors++;
			}
		}else{
			obj3.SetActive (false);
		}
		Text txt = GameObject.Find("SensorText").GetComponent<Text>();
		if(txt != null){
			txt.text = amountOfSensors.ToString();
		}
	}

	public void openSensorView(Sensor currSensor){
		Canvas sensorCanvas = GameObject.Find("SensorCanvas").GetComponent<Canvas>();
		sensorCanvas.enabled = true;
		sensorCanvas.GetComponent<Animator>().SetBool("found", true);
		Canvas machineCanvas = GameObject.Find("GerätCanvas").GetComponent<Canvas>();
		machineCanvas.enabled = true;
		machineCanvas.GetComponent<Animator>().SetBool("found", false);

		GameObject.Find("Fire KR").GetComponent<ParticleSystem>().Play();
		Text sensorName = GameObject.Find("SensorName").GetComponent<Text>();
		sensorName.text = currSensor.sensorType;
		Text maxValue = GameObject.Find("MaxValue").GetComponent<Text>();
		maxValue.text = currSensor.maxValue.ToString();
		Text minValue = GameObject.Find("MinValue").GetComponent<Text>();
		minValue.text = currSensor.minValue.ToString();
		GameObject.Find("CloseSensor").GetComponent<Button>().onClick.AddListener(delegate{closeSensorCanvas(sensorCanvas);});
		//Button backToMachine = GameObject.Find("backToMachine").GetComponent<Button>();
		//backToMachine.onClick.AddListener(delegate{back(currSensor);});
	}

	private void closeSensorCanvas(Canvas sensorCanvas){
		sensorCanvas.GetComponent<Animator>().SetBool("found", false);
		Text txt = GameObject.Find("SensorText").GetComponent<Text>();
		if(txt != null){
			txt.text = "0";
		}
		GameObject btnGroup = GameObject.Find("SensorsButtonGroup");
		if(btnGroup != null){
			btnGroup.SetActive(false);
		}
		Animator sensorButton = GameObject.Find("Sensor Button").GetComponent<Animator>();
		sensorButton.SetBool("sensorAvailable", false);
		Control.obj.currMachine = null;
	}

	private void back(Sensor currSensor){
		GameObject.Find("SensorCanvas").GetComponent<Canvas>().enabled = false;
		//Canvas obj = GameObject.Find(currSensor.parent.name).GetComponent<Canvas>();
		Canvas obj = GameObject.Find("KR QUANTEC Extra").GetComponent<Canvas>();
		obj.enabled = true;
		GameObject.Find("Fire KR").GetComponent<ParticleSystem>().Stop();
	}
}