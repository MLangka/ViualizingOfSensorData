using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class quit : MonoBehaviour{

	private Animator popup;

	public void QuitProgram(){
		//pop up window
		popup = GameObject.Find("Quit").GetComponent<Animator>();
		popup.SetBool("quit", true);

		GameObject.Find("ReturnToDashboard").GetComponent<Button>().onClick.AddListener(returnToDashboard);
		GameObject.Find("QuitApp").GetComponent<Button>().onClick.AddListener(quitApp);
		GameObject.Find("closeWindow").GetComponent<Button>().onClick.AddListener(closeWindow);
	}

	private void returnToDashboard(){

	}

	private void quitApp(){
		//TODO maybe call save here to save data into a file? from control
		//close the whole application
		Application.Quit();
	}

	private void closeWindow(){
		popup.SetBool("quit", false);
	}
}