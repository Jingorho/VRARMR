using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControlMenu_Toggle : MonoBehaviour {
    public bool isSelected;
    public bool isToggleOn;

    public Toggle toggle;

	// Use this for initialization
	void Start () {
        isSelected = false;
        isToggleOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isSelected){
            toggle.Select();
            //Debug.Log("toggle.select()");

            if(isToggleOn){
                //Debug.Log("toggle.isOn: " + toggle.isOn);
                toggle.isOn = true;

            }else{
                toggle.isOn = false;
            }
        }else{

        }


        
	}
}
