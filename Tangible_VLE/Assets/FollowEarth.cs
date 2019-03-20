using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEarth : MonoBehaviour {
    public MY_TrackedController mytrackedController_left;
    public MY_TrackedController mytrackedController_right;

	// Use this for initialization
	void Start () {
		//mytrackedController_left = GameObject.FindWithTag("leftController").GetComponent<MY_TrackedController>();
        //mytrackedController_right = GameObject.FindWithTag("rightController").GetComponent<MY_TrackedController>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GameObject.Find("Earth").transform.position;

        if(mytrackedController_left.isGrabbing || mytrackedController_right.isGrabbing) {
            transform.rotation = GameObject.Find("Earth").transform.rotation;
        }else {

        }

	}//Update
}
