using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour {

	// this is the UI.Text or other UI element you want to toggle
	public MaskableGraphic imageToToggle;
	public Text sensorText;
	public static bool sensorsFound;

	private float interval = 1f;
	private float startDelay = 2f;
	private bool currentState = true;
	private bool defaultState = true;
	private bool isBlinking = false;
	private Sensor s;

	public AudioClip clip;

	void Start(){
		imageToToggle.enabled = defaultState;
		sensorText.enabled = defaultState;
		if (sensorsFound) {
			StartBlink ();
		}
	}

	//load again and update the current values
	void Update(){
		if (sensorsFound) {
			StartBlink ();
		}
	}

	public void StartBlink(){
		// do not invoke the blink twice - needed if you need to start the blink from an external object
		if (isBlinking)
			return;
		if (imageToToggle != null && sensorText != null) {
			isBlinking = true;
			InvokeRepeating ("ToggleState", startDelay, interval);
		}
	}

	public void ToggleState(){
		imageToToggle.enabled = !imageToToggle.enabled;
		sensorText.enabled = !sensorText.enabled;

		// plays the clip at (0,0,0)
		if (clip)
			AudioSource.PlayClipAtPoint(clip,Vector3.zero);
	}

	public void showAvailableSensors(){
//		print("button for sensors pushed");
		GameObject canvas = GameObject.Find("Available Sensors Canvas");
		Text avSensors = GameObject.Find("Available Sensors").GetComponent<Text>();
		if (canvas.activeSelf == true) {
			canvas.SetActive (false);
		} else {
			canvas.SetActive(true);
			sensorText = GameObject.Find("SensorText").GetComponent<Text>();
			if(sensorText.text.CompareTo("0") == 0){
				avSensors.text = "no sensors found";
			}else{
				avSensors.text = "Sensors Found: ";
				/*ArrayList sn = Control.sensorNames;
				foreach(string element in sn){
					avSensors.text += element;
					avSensors.text += ", ";
				}*/
				//delete last comma
			}
		}
	}
}