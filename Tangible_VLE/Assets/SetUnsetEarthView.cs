using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUnsetEarthView : MonoBehaviour {
    public GameObject _Environment;
	public GameObject _North1;

	public bool adjustPosRot;

	public bool isZeroPos;
	public bool isZeroRot;

	// Use this for initialization
	void Start () {
		_Environment = GameObject.Find("Environment");
		_North1 = GameObject.Find("North1");

		adjustPosRot = false;

		isZeroPos = false;
		isZeroRot = false;
	}
	
	// Update is called once per frame
	void Update () {
		

		if(adjustPosRot){
		//if(Input.GetKeyDown(KeyCode.L)) {
            
           Debug.Log("Pos : ( " + Mathf.Round(_North1.transform.position.x) + ", " + Mathf.Round(_North1.transform.position.y) + ", " + Mathf.Round(_North1.transform.position.z) + ") ");
            Debug.Log("Rot : ( " + Mathf.Round(_North1.transform.eulerAngles.x) + ", " + Mathf.Round(_North1.transform.eulerAngles.y) + ", " + Mathf.Round(_North1.transform.eulerAngles.z) + ") ");
            
			
            if(isZeroCheckPosRot()) {
                adjustPosRot = false;

            }else {
                if(!isZeroPos) {
                    //North1を原点に戻す（Terrainの位置が原点なので）
                    Vector3 _EnvironmentPos = _Environment.transform.position;
			        _EnvironmentPos.x = 0 - _North1.transform.position.x; //position
                    _EnvironmentPos.y = 0 - _North1.transform.position.y;
                    _EnvironmentPos.z = 0 - _North1.transform.position.z;
                    _Environment.transform.position = _EnvironmentPos;
                }
                
                if(!isZeroRot) {
                    //North1を上を向かせる
			
                    //Environmentにとっての上向きベクトルとNorth1にとってのベクトルの差分
			        //Vector3 axis = transform.up - _North1.transform.eulerAngles;
                    Vector3 axis = GameObject.Find("DummyOrigin").transform.up - _North1.transform.eulerAngles;
			        float sub = axis.x;//差分ベクトルのx成分

			        _Environment.transform.RotateAround (
				        _North1.transform.position,
				        new Vector3 (axis.x, 0, 0),
				        (-1) * axis.x
			        );

			        _Environment.transform.RotateAround (
				        _North1.transform.position,
				        new Vector3 (0, axis.y, 0),
				        (-1) * axis.y
			        );

			        _Environment.transform.RotateAround (
				        _North1.transform.position,
				        new Vector3 (0, 0, axis.z),
				        (-1) * axis.z
			        );
                }

            }//is isZeroCheckPosRot
            
            
			
        }//if adjustPosRot
	}//Update



    bool isZeroCheckPosRot() {
        if(Mathf.Round(_North1.transform.position.x) == 0f &&
           Mathf.Round(_North1.transform.position.y) == 0f &&
           Mathf.Round(_North1.transform.position.z) == 0f ) {
            isZeroPos = true;
        }

        if(Mathf.Round(_North1.transform.eulerAngles.x) == 0f &&
           Mathf.Round(_North1.transform.eulerAngles.y) == 0f &&
           Mathf.Round(_North1.transform.eulerAngles.z) == 0f ) {
            isZeroRot = true;
        }

        if(isZeroPos == true && isZeroRot == true){
            return true;
        }else{
            return false;
        }

    }//CheckPosRot
}
