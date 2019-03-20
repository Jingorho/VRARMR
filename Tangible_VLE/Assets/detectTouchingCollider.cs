using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectTouchingCollider : MonoBehaviour {
    public rotationSpin _rotationSpin;
    public GameObject[] North;
    public bool tempTouchingCheck;
    public int touchingColliderNum;

	// Use this for initialization
	void Start () {
        _rotationSpin = GameObject.Find("Earth").GetComponent<rotationSpin>();

        North = new GameObject[4];
		North[0] = GameObject.Find("North0");
        North[1] = GameObject.Find("North1");
        North[2] = GameObject.Find("North2");
        North[3] = GameObject.Find("North3");

        tempTouchingCheck = false;
        touchingColliderNum = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//if(_rotationSpin.isForceRotationByController) {
        //    Debug.Log("===");
            for(int i=0; i<North.Length; i++) {
                //tempTouchingCheck = North[i].GetComponent<detectTouchingCollider>.isTouchingToController;
                Debug.Log("====================Non");

                if(tempTouchingCheck){
                    touchingColliderNum = i;
                    Debug.Log("===========================Touching is " + touchingColliderNum);
                }
            }//for

        //}//if isForceRotationByController
	}//Update


    public GameObject GetTouchingCollider() {
        return North[touchingColliderNum];
    }
}
