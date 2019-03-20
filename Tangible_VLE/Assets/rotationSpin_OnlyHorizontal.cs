using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class rotationSpin_OnlyHorizontal : MonoBehaviour {
	public static float selfSpinDegreePerSec;
	public static float revolutionSpinDegreePerSec;
	public static bool isSelfSpin;
	public static bool isRevolutionSpin;
    public bool isForceRotationByController;

    public Vector3 lastRotation;
    public Vector3 firstRotation;

    public Transform dummyPointer;
    public Vector3 dummyOffset;
    public Vector3 earthOffset;

	public TrailRenderer trailRenderer_earth;

	[SerializeField]
    public Toggle EarthRotationToggle;
	[SerializeField]
    public Toggle EarthRevolutionToggle;
    public float initPositionY;

    public Rigidbody EarthRigidbody;
    
	// Use this for initialization
	void Start () {
        selfSpinDegreePerSec = 20; //360 / (60 * 60 * 24);
		revolutionSpinDegreePerSec = 1; //360 / (60 * 60 * 24 * 365);
		isSelfSpin = false;
		isRevolutionSpin = false;
        isForceRotationByController = false;

        lastRotation = transform.eulerAngles;
        firstRotation = transform.eulerAngles;

		trailRenderer_earth = GameObject.Find("Earth").GetComponent<TrailRenderer>();

        initPositionY = transform.position.y;

        EarthRigidbody = GameObject.Find("Earth").GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0)){ 
			isSelfSpin = !isSelfSpin;

		}else if(Input.GetKeyDown(KeyCode.Alpha1)){ 
			isRevolutionSpin =!isRevolutionSpin;
			trailRenderer_earth.enabled = !trailRenderer_earth.enabled;

		}


		if(isSelfSpin){ 
			SelfSpin();
            //EarthRotationToggle.isOn  = true;
		}else{
			//EarthRotationToggle.isOn  = false;
		}

		if(isRevolutionSpin){
			RevolutionSpin();
			trailRenderer_earth.enabled = true;
			//EarthRevolutionToggle.isOn  = true;
		}else{ 
			trailRenderer_earth.enabled = false;
			//EarthRevolutionToggle.isOn  = false;
		}


        if(isForceRotationByController) {
            ForceRotationByController();
        }


        /////////////////////////////////////////
        /*
        Vector3 currentVec = transform.position;
        currentVec.x = transform.position.x;
        currentVec.y = initPositionY;
        currentVec.z = transform.position.z;
        transform.position = currentVec;
        */
        /////////////////////////////////////////

	}//Update


    public void switchIsSelfSpin_forToggle(){
        if(EarthRotationToggle.isOn) {
            isSelfSpin = true;
        }else{
            isSelfSpin = false;
        }
    }
    public void switchIsRevolutionSpin_forToggle(){
        if(EarthRevolutionToggle.isOn) {
            isRevolutionSpin = true;
        }else{
            isRevolutionSpin = false;
        }
    }



	public void SelfSpin(){
        //Horizontalに限定したrigidbodyを解放
        EarthRigidbody.constraints = RigidbodyConstraints.None;

		transform.Rotate(0, selfSpinDegreePerSec * Time.deltaTime, 0);
	}

	public void RevolutionSpin(){ 
        //Horizontalに限定したrigidbodyを解放
        EarthRigidbody.constraints = RigidbodyConstraints.None;

		transform.RotateAround(
			 GameObject.Find("Sun").transform.position,
			 transform.up,
			 revolutionSpinDegreePerSec * Time.deltaTime
		);
	}
    

    public void SetForceRotationByController() {
        isForceRotationByController = true;
    }

    public void ForceRotationByController() {
        Vector3 controllerPos = transform.position;

        GameObject leftController = GameObject.FindWithTag("leftController");
        GameObject rightController = GameObject.FindWithTag("rightController");

        if(leftController.GetComponent<MY_TrackedController>().isTouching){
            controllerPos = leftController.transform.position;
            Debug.Log("left desho---------------------");
        }else if(rightController.GetComponent<MY_TrackedController>().isTouching){
            controllerPos = rightController.transform.position;
            Debug.Log("right desho;;;;;;;;;;;;;;;;;");
        }else {
            Debug.Log("else desho~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        //GameObject earthColliders = GameObject.Find("EarthColliders").GetComponent<>
        //Vector3 fromPos = 
        //transform.Rotate(controllerPos - transform.rotation.eulerAngles);
        //transform.LookAt( (firstRotation - lastRotation) + controllerPos);
        transform.LookAt( new Vector3(0, firstRotation.y - lastRotation.y, 0) + controllerPos);
        //Quaternion.LookRotation(controllerPos - transform.position);
        //dummyPointer.LookAt(controllerPos);

        //transform.eulerAngles = earthOffset + (dummyPointer.eulerAngles - dummyOffset);
    }


    public void RegistFirstRotate(){
        firstRotation = transform.eulerAngles;
        dummyOffset = dummyPointer.eulerAngles;
        earthOffset = transform.eulerAngles;
    }

    public void RegistLastRotate(){
        lastRotation = transform.eulerAngles;
    }
}
