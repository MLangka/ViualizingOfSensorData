/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES

		public GameObject btnGroup;
		private GameObject[] canvases;
		private Canvas canvas;

        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
			canvas = GameObject.Find("GerätCanvas").GetComponent<Canvas>();
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS


        #region PRIVATE_METHODS

		private IEnumerator showInfo(){
			//enable button for machinefound
			Button btn = GameObject.Find("MachineFound").GetComponent<Button>();
			GameObject.Find("MachineFound").GetComponent<Animator>().SetBool("targetFound", true);
			//show the button shortly
			yield return new WaitForSeconds (1.5f);
			GameObject.Find("MachineFound").GetComponent<Animator>().SetBool("targetFound", false);
			yield return new WaitForSeconds (1);
			//open canvas with information
			canvas.GetComponent<Animator>().SetBool("found", true);
		}

        private void OnTrackingFound(){
			//create the machine object for this image
			Control.obj.currMachine = new Gerät("KR QUANTEC Extra");
			//activate buttons for different sensors and current canvas 
			btnGroup.SetActive(true);
			canvas.enabled = true;
			//what to do only if we are in the general machine view
			if(!GameObject.Find("SensorCanvas").GetComponent<Canvas>().enabled){
				StartCoroutine(showInfo());
				//show the buttons with all sensors
				Control.obj.currMachine.showButtons();
			}

            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
			GameObject.Find("MachineFound").GetComponent<Animator>().SetBool("targetFound", false);
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}