using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Sensor{

	//timestamp and curValue are the only ones changing
	//public Image src = GameObject.Find("Icon").GetComponent<Image>();

	public readonly int maxValue;
	public readonly int minValue;
	public readonly String description;
	public readonly String sensorType;
	public int curValue;
	public String timestamp;
	public Gerät parent;

	public Sensor(String type, int max, int min, Gerät parent){
		sensorType = type;
		String description = "This is a " + type.ToLower() + " sensor and it belongs to the dummy machine.";
		this.maxValue = max;
		this.minValue = min;
		curValue = 180;
	}

		public void updateCurValue(/*int cur, String time*/){
			curValue = curValue+10;
			//timestamp = time;
		}
}