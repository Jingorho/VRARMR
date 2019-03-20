using UnityEngine;
using System.Collections;

public class MY_TrackedController : MonoBehaviour {
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

    public bool isUIOn;
    public float padClickedNum;
    public bool isToggleSelected;

    public GameObject followingCubeUI;
    public bool isFirstApprear;
    


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

        isUIOn = false;
        padClickedNum = 0.0f;
        isToggleSelected = false;

        followingCubeUI = GameObject.Find("FollowingCubeUI");
        isFirstApprear = true;

    }//Start()




    void Update() {
        if(isTouching) { 
            //jointedControllerQua = this.transform.rotation;
        }
        controllerVec_post = transform.position;
        controllerVec_delta = controllerVec_pre - controllerVec_post;
        controllerVec_pre = transform.position;


        /*
        if(Input.GetKeyDown(KeyCode.J)){
             isUIOn = true;
        }
         if(Input.GetKeyDown(KeyCode.L)){
             isUIOn = false;
        }
         if(Input.GetKeyDown(KeyCode.G)){
             isToggleSelected = true;
        }
        if (Input.GetKeyDown(KeyCode.H)){
             isToggleSelected = false;
        }

        
        //サイドボタンを押されたらすぐオン
        if(isUIOn){
            if(isFirstApprear) {
                followingCubeUI.SetActive(true);
                isFirstApprear = false;
            }
            
            if(followingCubeUI.active) {
                //バグ？で2回ずつ呼び出されてしまうので、しゃーなしにpadClickedNum/2で算出。
                switch (Mathf.FloorToInt(padClickedNum)){
                    //メニュー表示してパッドを押すまでの最初の1回
                    case 0:
                        //GameObject.Find("ActualSizeToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isSelected = true;
                        GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isSelected = false;

                        //UIが表示されてる状態でトグルを押すトリガーが押されたら
                        if(isToggleSelected) {
                            GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isToggleOn = true;
                            //Debug.Log("isToggleSelected");
                         }else{
                            GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isToggleOn = false;
                        }

                        break;
                    
                    //パッドを1回押したら
                    case 1:
                        //GameObject.Find("ActualSizeToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isSelected = true;
                        GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isSelected = false;

                        //UIが表示されてる状態でトグルを押すトリガーが押されたら
                        if(isToggleSelected) {
                            GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isToggleOn = true;
                            //Debug.Log("isToggleSelected");
                         }else{
                            GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isToggleOn = false;
                        }

                        break;

                    //パッドを2回押したら
                    case 2:
                        //GameObject.Find("ActualSizeToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isSelected = true;
                        GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isSelected = false;

                        //UIが表示されてる状態でトグルを押すトリガーが押されたら
                        if(isToggleSelected) {
                            GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isToggleOn = true;
                            //Debug.Log("isToggleSelected");
                         }else{
                            GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isToggleOn = false;
                        }

                        break;

                    //パッドを3回押したら
                    case 3:
                        //GameObject.Find("ActualSizeToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                        GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isSelected = true;

                        //UIが表示されてる状態でトグルを押すトリガーが押されたら
                        if(isToggleSelected) {
                            GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isToggleOn = true;
                            //Debug.Log("isToggleSelected");
                         }else{
                            GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isToggleOn = false;
                        }

                        break;

                        
                        //パッドを4回押したら
                        case 4:
                            GameObject.Find("ActualSizeToggle").GetComponent<controlMenu>().isSelected = false;
                            GameObject.Find("EarthRotationToggle").GetComponent<controlMenu>().isSelected = false;
                            GameObject.Find("MoonRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                            GameObject.Find("EarthRevolutionToggle").GetComponent<controlMenu>().isSelected = false;
                            GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isSelected = true;

                            //UIが表示されてる状態でトグルを押すトリガーが押されたら
                            if(isToggleSelected) {
                                GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isToggleOn = true;
                                //Debug.Log("isToggleSelected");
                             }else{
                                GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isToggleOn = false;
                            }

                            break;
                       
                }//followingCubeUI.activeのときのみ
            }
        
        //サイドボタンをもう一度押されたらすぐオフ
        }else{

            if(isFirstApprear) {
               // if(!followingCubeUI) {
                    followingCubeUI.SetActive(false);
                    isFirstApprear = false;
               // }
            }
        }//isUIOn
        */

    }//Update()








    public void DoMenuButtonClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoMenuButtonClicked");
    }

    public void DoMenuButtonUnClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoMenuButtonUnClicked");
    }

    public void DoTriggerClicked(object sender, ClickedEventArgs e) {
        Debug.Log ("DoTriggerClicked");

        if(isUIOn) {
            isToggleSelected = !isToggleSelected;
        
        //オブジェクトの操作が出来るのは、UIがオフのときだけ
        } else{
            
            Rigidbody EarthRigidbody = GameObject.Find("Earth").GetComponent<Rigidbody>();
            if (_viewSwitcher.isViewAxisAndFixRotation){
                //_viewSwitcher.ResetRotation();
                //_viewSwitcher.t = 0;
                /*
                Vector3 EarthAxisVec = GameObject.Find("EarthAxis").transform.rotation.eulerAngles;
                GameObject.Find("Earth").GetComponent<Rigidbody>().AddTorque(
     //                   new Vector3(EarthAxisVec.x * 0.1f, 0, EarthAxisVec.z * 0.1f)
                        new Vector3(0, 0, EarthAxisVec. * 0.3f)
                    );
                */
                if(isTouching) { 
                    EarthRigidbody.constraints = RigidbodyConstraints.FreezePosition;
                    grab ();
                }
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

            }else { 
                _rotationSpin.isForceRotationByController = false;

                EarthRigidbody.constraints = RigidbodyConstraints.None;
    		    grab ();
            }
        }//isUIOn
       

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
        if(isUIOn){
            if(padClickedNum > 3.0f){ //バグ？で2回ずつ呼び出されてしまうので、しゃーないから0.5ずつ増やす
                padClickedNum = -0.5f;
            }
            padClickedNum = (float)(padClickedNum + 0.5f);
        }
        Debug.Log ("DoPadClicked : " + padClickedNum + ": " + Mathf.FloorToInt(padClickedNum));
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
        isFirstApprear = !isFirstApprear;
        isUIOn = !isUIOn;//UIが表示されてたらオフに、表示されてなかったらオンに
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