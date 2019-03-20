using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MY_TrackedController_OnlyHorizontal : MonoBehaviour {
    SteamVR_TrackedController trackedController;
	GameObject grababbleObject;
    FixedJoint joint;
    
    public Rigidbody earthRigidbody;
    public ViewSwitcher _viewSwitcher;
    public rotationSpin _rotationSpin;

    public Vector3 controllerVec_post;
    public Vector3 controllerVec_pre;
    public Vector3 controllerVec_delta;

    public bool isTouching;
    public bool isGrabbing;

    public bool isEarthGrabbing;
    public bool isMoonGrabbing;

    ///*
    public bool isUIOn_Ctl;
    public bool isToggleSelected_Ctl;
    public bool isFirstApprear_Ctl;
    public float padClickedNum_Ctl;
    //*/
    public bool nextTriggerState;

    public GameObject followingUI;
    

    void Start () {
        isTouching = false;
        isGrabbing = false;
        isEarthGrabbing = false;
        isMoonGrabbing = false;

        earthRigidbody = GameObject.Find("Earth").GetComponent<Rigidbody>();
        _viewSwitcher = GameObject.Find("Environment").GetComponent<ViewSwitcher>();
        _rotationSpin = GameObject.Find("Earth").GetComponent<rotationSpin>();

        trackedController = gameObject.GetComponent<SteamVR_TrackedController> ();

        if (trackedController == null) {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController> ();
        }

        trackedController.MenuButtonClicked += new ClickedEventHandler (DoMenuButtonClicked);
        trackedController.MenuButtonUnclicked += new ClickedEventHandler (DoMenuButtonUnClicked);
        trackedController.TriggerClicked += new ClickedEventHandler (DoTriggerClicked);
        trackedController.TriggerUnclicked += new ClickedEventHandler (DoTriggerUnclicked);
        trackedController.SteamClicked += new ClickedEventHandler (DoSteamClicked);
        trackedController.PadClicked += new ClickedEventHandler (DoPadClicked);
        trackedController.PadUnclicked += new ClickedEventHandler (DoPadClicked);
        trackedController.PadTouched += new ClickedEventHandler (DoPadTouched);
        trackedController.PadUntouched += new ClickedEventHandler (DoPadTouched);
        trackedController.Gripped += new ClickedEventHandler (DoGripped);
        trackedController.Ungripped += new ClickedEventHandler (DoUngripped);

		joint = gameObject.GetComponent<FixedJoint> ();

        controllerVec_pre = transform.position;
        controllerVec_post = transform.position;

        
        //FollowingUIの初期値をセット
        isUIOn_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn;
        padClickedNum_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().padClickedNum;
        isToggleSelected_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected;
        isFirstApprear_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isFirstApprear;
        


    }//Start()




    void Update() {

        if(isTouching) { 
            //jointedControllerQua = this.transform.rotation;
        }
        controllerVec_post = transform.position;
        controllerVec_delta = controllerVec_pre - controllerVec_post;
        controllerVec_pre = transform.position;
        


    }//Update()








    public void DoMenuButtonClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoMenuButtonClicked");
    }

    public void DoMenuButtonUnClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoMenuButtonUnClicked");
    }

    public void DoTriggerClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoTriggerClicked");
        //逆側の手で更新されてるかもしれないFollowingUIの変数の値で更新してから
        isUIOn_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn;
        isToggleSelected_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected;
        
        if(isUIOn_Ctl) {
            isToggleSelected_Ctl = !isToggleSelected_Ctl;
            //GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected = isToggleSelected_Ctl;

        //オブジェクトの操作が出来るのは、UIがオフのときだけ
        } else{
            
            
            //if (_viewSwitcher.isViewAxisAndFixRotation){
                //_viewSwitcher.ResetRotation();
                //_viewSwitcher.t = 0;
                /*
                Vector3 EarthAxisVec = GameObject.Find("EarthAxis").transform.rotation.eulerAngles;
                GameObject.Find("Earth").GetComponent<Rigidbody>().AddTorque(
     //                   new Vector3(EarthAxisVec.x * 0.1f, 0, EarthAxisVec.z * 0.1f)
                        new Vector3(0, 0, EarthAxisVec. * 0.3f)
                    );
                */
                /*
                if(isTouching) { 
                    //EarthRigidbody.constraints = RigidbodyConstraints.FreezePosition;
                    grab ();
                }
                */
                /*
                if (isTouching){
                    Debug.Log("isTouching and isForceRotationByController ====== true");
                    _rotationSpin.isForceRotationByController = true;
                }else {
                    _rotationSpin.isForceRotationByController = false;
                }
                _rotationSpin.RegistFirstRotate();
                //*/

                //_rotationSpin.ForceRotationByController(controllerQua);
                /*Rigidbody EarthRigidbody = GameObject.Find("Earth").GetComponent<Rigidbody>();
                EarthRigidbody.constraints = 
                    RigidbodyConstraints.FreezePosition |
                    RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;*/
            
                //ForceRotationByController();

            //}else { 
                Debug.Log("Constraints rigidbody");
                //_rotationSpin.isForceRotationByController = false;
                //EarthRigidbody.constraints = RigidbodyConstraints.None;

                //Horizontal
                GameObject.Find("Earth").GetComponent<rotationSpin_OnlyHorizontal>().EarthRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                GameObject.Find("Moon").GetComponent<rotationSpin_moon_OnlyHorizontal>().MoonRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    		    grab ();
            //}
        }//isUIOn
        
        //今の手で更新された値でFollowingUIを更新
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn= isUIOn_Ctl;
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected = isToggleSelected_Ctl;
        

    }//DoTriggerClicked

    public void DoTriggerUnclicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoTriggerUnclicked");
        if (_viewSwitcher.isViewAxisAndFixRotation){
            _rotationSpin.isForceRotationByController = false;
            _rotationSpin.RegistLastRotate();

            release ();

        } else{
            release ();
        }
    }

    public void DoSteamClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoSteamClicked");
    }

    public void DoPadClicked(object sender, ClickedEventArgs e) {
        //逆側の手で更新されてるかもしれないFollowingUIの変数の値で更新してから
        isUIOn_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn;
        padClickedNum_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().padClickedNum;
        isToggleSelected_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected;

       
        if (isUIOn_Ctl){
            //isToggleSelected_Ctl = false; //padがクリックされたら（Toggleが次に進んだら）トリガーオンだろうがオフだろうが
            /*
            switch (Mathf.FloorToInt(padClickedNum_Ctl)){
                case 0:
                    nextTriggerState = GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected;
                    break;
                case 1:
                    nextTriggerState = GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected;
                    break;
                case 2:
                    nextTriggerState = GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected;
                    break;
                case 3:
                    nextTriggerState = GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected;
                    break;
            }//switch
            */

            if(padClickedNum_Ctl > 3.0f){ //バグ？で2回ずつ呼び出されてしまうので、しゃーないから0.5ずつ増やす
                padClickedNum_Ctl = -0.5f;
            }
            padClickedNum_Ctl = (float)(padClickedNum_Ctl + 0.5f);
            

            
            //Toggle更新後のFollowingUIのToggleSelected
            nextTriggerState = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected;
            

        }
        Debug.Log ("DoPadClicked : " + padClickedNum_Ctl + ": " + Mathf.FloorToInt(padClickedNum_Ctl));

        //今の手で更新された値でFollowingUIを更新
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isToggleSelected = isToggleSelected_Ctl;
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn = isUIOn_Ctl;
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().padClickedNum = padClickedNum_Ctl;
    }

    //バグ?UnClickedは呼び出されず、Clickedが2回呼び出される
    public void DoPadUnclicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadUnclicked");
    }

    public void DoPadTouched(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadTouched");
    }

    //バグ?Untachedは呼び出されず、Tachedが2回呼び出される
    public void DoPadUntouched(object sender, ClickedEventArgs e) {
        Debug.Log ("DoPadUntouched");
    }


    public void DoGripped(object sender, ClickedEventArgs e) {
        //逆側の手で更新されてるかもしれないFollowingUIの変数の値で更新してから
        isUIOn_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn;
        isFirstApprear_Ctl = GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isFirstApprear;

        isUIOn_Ctl = !isUIOn_Ctl;//UIが表示されてたらオフに、表示されてなかったらオンに
        isFirstApprear_Ctl = true;
        
        //今の手で更新された値でFollowingUIを更新
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isUIOn = isUIOn_Ctl;
        GameObject.Find("FollowingUI").GetComponent<ControlMenuByContoller>().isFirstApprear = isFirstApprear_Ctl;

        Debug.Log ("DoGripped");
    }

    public void DoUngripped(object sender, ClickedEventArgs e) {
        Debug.Log ("DoUngripped");
    }
	


	
    void grab() {
        if (grababbleObject == null || joint.connectedBody != null) {
            return;
        }
        joint.connectedBody = grababbleObject.GetComponent<Rigidbody> ();

        //掴んでるなうフラグ立てる
        isGrabbing = true;

        //地球掴んでるなうフラグ立てる
        if(grababbleObject == GameObject.Find("Earth")) {
            isEarthGrabbing = true;
            isMoonGrabbing = false;

        //月掴んでるなうフラグ立てる
        }else if(grababbleObject == GameObject.Find("Moon")){
            isEarthGrabbing = false;
            isMoonGrabbing = true;
        }

		Debug.Log("jointtttttttttttttttttttttttttttttttttttt");
    }



    void release() {
        //Horizontalに限定したConstraintsを解放
        GameObject.Find("Earth").GetComponent<rotationSpin_OnlyHorizontal>().EarthRigidbody.constraints =　RigidbodyConstraints.None;
        GameObject.Find("Moon").GetComponent<rotationSpin_moon_OnlyHorizontal>().MoonRigidbody.constraints = RigidbodyConstraints.None;

        if (joint.connectedBody == null) {
            return;
        }
        joint.connectedBody = null;
        isGrabbing = false;
        isEarthGrabbing = false;
        isMoonGrabbing = false;
    }

    void ForceRotationByController(){
         //移動量に応じて角度計算
        float xAngle = controllerVec_delta.y * 100;
        float yAngle = -1 * controllerVec_delta.x * 100;
        float zAngle = 0;

        //回転
        GameObject.Find("Earth").transform.Rotate(xAngle, yAngle, zAngle, Space.World);
        
    }

    void OnTriggerEnter(Collider other) {
        grababbleObject = other.gameObject;
        isTouching = true;
		Debug.Log("OnTriggerEnter");
    }



    void OnTriggerExit(Collider other) {
        grababbleObject = null;
        isTouching = false;
		Debug.Log("OnTriggerExit");
    }

}//class