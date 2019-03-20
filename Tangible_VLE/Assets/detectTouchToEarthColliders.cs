using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectTouchToEarthColliders : MonoBehaviour {
    public bool isTouchingToController;
	// Use this for initialization
	void Start () {
		isTouchingToController = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider col){
        isTouchingToController = true;
        Debug.Log("####################################");
    }

    void OnTriggerExit (Collider col){
        isTouchingToController = false;
        Debug.Log("JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJ");
    }
}
