using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotationController : MonoBehaviour {

    public GameObject isVRObj;
    public isVR isvr;
    public bool isVRFlg;

    //コントローラからトリガの情報取得
    public MY_TrackedController myControllerLeft;
    public MY_TrackedController myControllerRight;
    public bool isSldrBtnClickedLeft;
    public bool isSldrBtnClickedRight;

    public Slider XRot;
    public Slider YRot;
    public Slider ZRot;

    public void Start()
    {
        isVRObj = GameObject.Find("isVR");
        isvr = isVRObj.GetComponent<isVR>();
        isVRFlg = isvr.isVRFlg;
    }
    
    public void Update() {
        
        if (isVRFlg)
        {
            //コントローラからトリガの情報を取得
            isSldrBtnClickedLeft = myControllerLeft.isSldrBtnClicked;
            isSldrBtnClickedRight = myControllerRight.isSldrBtnClicked;
            //isSldrBtnClickedLeft = SteamVR_TrackedController.padPressed;



            if (isSldrBtnClickedLeft || isSldrBtnClickedRight)
            {
                myControllerLeft.GetComponent<LaserPointer>().enabled = true;
                myControllerRight.GetComponent<LaserPointer>().enabled = true;

            }
            else
            {
                myControllerLeft.GetComponent<LaserPointer>().enabled = false;
                myControllerRight.GetComponent<LaserPointer>().enabled = false;
            }

        //isVRFlg
        }else{
            
        }//isVRFlg

    }//Update()
    

    public void UpdateObjectPosition(Transform ControlledObject)
    {
        //スライダ -> Quadを回転
        //スライダ更新用のボタンが押されてる状態のときのみ、Quadの値をスライダから更新する
        if (isSldrBtnClickedLeft || isSldrBtnClickedRight){

            Vector3 newRotation = new Vector3(XRot.value, YRot.value, ZRot.value);
            ControlledObject.rotation = Quaternion.Euler(newRotation);

        } else {
            
        }
    }
}
