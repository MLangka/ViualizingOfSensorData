using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currTime : MonoBehaviour {

	public Text textField;

	// Use this for initialization
	void Start () {
		//textField = (Text) gameObject.GetComponent(typeof(Text));
		if (textField != null) {
			textField.text = System.DateTime.Now.ToString ("dd/MM/yyyy HH:mm");
		}
		InvokeRepeating("updateTime", 60, 60);
	}
	
	// Update is called once per frame
	void updateTime () {
		textField.text = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm");
	}
}
