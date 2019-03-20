using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour {
    ViewSwitcher _viewSwitcher;

	// Use this for initialization
	void Start () {
        _viewSwitcher = GameObject.Find("Environment").GetComponent<ViewSwitcher>();

    }
	
	// Update is called once per frame
	void Update () {
        if (_viewSwitcher.isEarthView){
			if (_viewSwitcher.isMovingEnvironment) {
				transform.localRotation = GameObject.Find ("Earth").transform.rotation;
			}
        }
	}
}
