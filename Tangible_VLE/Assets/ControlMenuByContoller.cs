using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuByContoller : MonoBehaviour {
    public bool isUIOn;
    public bool isToggleSelected;
    public bool isFirstApprear;
    public float padClickedNum;
    
    public GameObject followingUI;


    public CanvasGroup canvasGroup;
        

    /*
    public GameObject earthRotationToggle;
    public GameObject moonRevolutionToggle;
    public GameObject earthRevollutionToggle;
    public GameObject resetToggle;
    */
    
	// Use this for initialization
	void Start () {
		isUIOn = false;
        padClickedNum = 0.0f;
        isToggleSelected = false;

        followingUI = GameObject.Find("FollowingUI");
        isFirstApprear = true;

        /*
        earthRotationToggle = GameObject.Find("EarthRotationToggle");
        moonRevolutionToggle = GameObject.Find("MoonRevolutionToggle");
        earthRevollutionToggle = GameObject.Find("EarthRevollutionToggle");
        resetToggle = GameObject.Find("ResetRotationToggle");
        */
        
	}//Start()
	

	// Update is called once per frame
	void Update () {
		if(isUIOn){
            if(isFirstApprear) {
                //followingUI.SetActive(true);
                SwitchUIVisible(true);
                isFirstApprear = false;
            }

            //if(GameObject.Find("followingUI").active){
            if (followingUI.active) {
                SetToggle(padClickedNum);
            }
        
        //サイドボタンをもう一度押されたらすぐオフ
        }else{

            if(isFirstApprear) {
                SwitchUIVisible(false);
                //GameObject.Find("followingUI").SetActive(false);
                isFirstApprear = false;
            }
        }//isUIOn

	}//Update()


    void SwitchUIVisible(bool isVisible){
        if(isVisible){
            canvasGroup.alpha = 1.0f;
        }else{
            canvasGroup.alpha = 0.0f;
        }
    }//SwitchUIVisible()




    void SetToggle(float _padClickedNum){
        //バグ？で2回ずつ呼び出されてしまうので、しゃーなしにpadClickedNum/2で算出。
        switch (Mathf.FloorToInt(_padClickedNum)){
            //メニュー表示してパッドを押すまでの最初の1回
            case 0:

                GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = true;
                GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;

                //UIが表示されてる状態でトグルを押すトリガーが押されたら
                if(isToggleSelected) {
                    GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = true;
                    //Debug.Log("isToggleSelected");
                    }else{
                    GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = false;
                }

                break;
                    
            //パッドを1回押したら
            case 1:
                //GameObject.Find("ActualSizeToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = true;
                GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;

                //UIが表示されてる状態でトグルを押すトリガーが押されたら
                if(isToggleSelected) {
                    GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = true;
                    //Debug.Log("isToggleSelected");
                    }else{
                    GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = false;
                }

                break;

            //パッドを2回押したら
            case 2:
                //GameObject.Find("ActualSizeToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = true;
                GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;

                //UIが表示されてる状態でトグルを押すトリガーが押されたら
                if(isToggleSelected) {
                    GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = true;
                    //Debug.Log("isToggleSelected");
                    }else{
                    GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = false;
                }

                break;

            //パッドを3回押したら
            case 3:
                //GameObject.Find("ActualSizeToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = true;

                //UIが表示されてる状態でトグルを押すトリガーが押されたら
                if(isToggleSelected) {
                    GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = true;
                    //Debug.Log("isToggleSelected");
                    }else{
                    GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = false;
                }

                break;

            /*  
            //パッドを4回押したら
            case 4:
                GameObject.Find("ActualSizeToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("MoonRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("EarthRevolutionToggle").GetComponent<ControlMenu_Toggle>().isSelected = false;
                GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isSelected = true;

                //UIが表示されてる状態でトグルを押すトリガーが押されたら
                if(isToggleSelected) {
                    GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = true;
                    //Debug.Log("isToggleSelected");
                    }else{
                    GameObject.Find("ResetRotationToggle").GetComponent<ControlMenu_Toggle>().isToggleOn = false;
                }

                break;
                */
        }//switch
    }//SetToggle

}//class
