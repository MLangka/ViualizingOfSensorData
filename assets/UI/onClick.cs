using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClick : MonoBehaviour {

	private GameObject[] canvases;
	private Canvas sensorCanvas;

	public void showButtons(){
		GameObject obj1 = GameObject.Find("Sensor1");
		GameObject obj2 = GameObject.Find("Sensor2");
		GameObject obj3 = GameObject.Find("Sensor3");
		if(obj1 != null){
			obj1.GetComponent<Animator>().SetBool("open", !obj1.GetComponent<Animator>().GetBool("open"));
		}
		if(obj2 != null){
			obj2.GetComponent<Animator>().SetBool("open", !obj2.GetComponent<Animator>().GetBool("open"));
		}
		if(obj3 != null){
			obj3.GetComponent<Animator>().SetBool("open", !obj3.GetComponent<Animator>().GetBool("open"));
		}
	}
}