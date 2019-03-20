using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotationSpin_moon_OnlyHorizontal : MonoBehaviour {

	public float revolutionSpinDegreePerSec_moon;
	public bool isRevolutionSpin;
	[SerializeField]
    public Toggle MoonRevolutionToggle;
	public TrailRenderer trailRenderer_moon;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public Vector3 axis;

    public MY_TrackedController_OnlyHorizontal mytrackedController_left;
    public MY_TrackedController_OnlyHorizontal mytrackedController_right;

    public float initPositionY;

    public Rigidbody MoonRigidbody;

	// Use this for initialization
	void Start () {
		revolutionSpinDegreePerSec_moon = 50; //360 / (60 * 60 * 24 * 365);
		isRevolutionSpin = false;

		trailRenderer_moon = GameObject.Find("Moon").GetComponent<TrailRenderer>();
		
        offsetX = 0f;
        offsetY = 0f;
        offsetZ = 0f;

        initPositionY = transform.position.y;

        MoonRigidbody = GameObject.Find("Moon").GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha2)){ 
			isRevolutionSpin = !isRevolutionSpin;
			//trailRenderer_moon.enabled = !trailRenderer_moon.enabled;
		}

		if(isRevolutionSpin){
			RevolutionSpin();
            trailRenderer_moon.enabled = true;
			//MoonRevolutionToggle.isOn  = true;
		}else{
            trailRenderer_moon.enabled = false;
			//MoonRevolutionToggle.isOn  = false;
		}

        //axis = GameObject.Find("EarthAxis").transform.eulerAngles;
        //axis = new Vector3(0, GameObject.Find("EarthAxis").transform.eulerAngles.y, 0);
        //http://ponkotsu-hiyorin.hateblo.jp/entry/2015/11/15/043345
        axis = transform.TransformDirection (GameObject.Find("EarthAxis").transform.up);
        //axis = transform.TransformDirection (GameObject.Find("EarthAxis").transform.eulerAngles);
		



        /////////////////////////////////////////
        /*
        Vector3 currentVec = transform.position;
        currentVec.x = transform.position.x;
        currentVec.y = initPositionY;
        currentVec.z = transform.position.z;
        transform.position = currentVec;
        */
        /////////////////////////////////////////




	}

    public void switchIsRevolutinoSpin_forToggle(){
        if(MoonRevolutionToggle.isOn){
            isRevolutionSpin = true;
        }else{
            isRevolutionSpin = false;
        }
        
    }


	void RevolutionSpin(){
        //Horizontalに限定したrigidbodyを解放
        MoonRigidbody.constraints = RigidbodyConstraints.None;

		// 月の軌道: https://ja.wikipedia.org/wiki/%E6%9C%88%E3%81%AE%E8%BB%8C%E9%81%93
		// 地球（地軸を無視した状態）の水平面から5.14度傾いている
		transform.RotateAround(
			GameObject.Find("Earth").transform.position, //地球を中心に
            //GameObject.Find("EarthAxis").transform.eulerAngles,
            ///*
           axis,//GameObject.Find("Earth").transform.eulerAngles.z),
            //*/
            /*
			new Vector3(
				GameObject.Find("EarthAxis").transform.eulerAngles.x - 23.4f,
				GameObject.Find("EarthAxis").transform.eulerAngles.y + 90,
				GameObject.Find("EarthAxis").transform.eulerAngles.z - 5.14f),*/
			revolutionSpinDegreePerSec_moon * Time.deltaTime
		);
	}


}
