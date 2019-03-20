using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadRotation : MonoBehaviour {


    public GameObject isVRObj;
    public isVR isvr;
    public bool isVRFlg;

    //コントローラからトリガの情報取得
    public MY_TrackedController myControllerLeft;
    public MY_TrackedController myControllerRight;
    public bool isSldrBtnClickedLeft;
    public bool isSldrBtnClickedRight;

    public Vector3 quadRotation;

    public Slider XRot;
    public Slider YRot;
    public Slider ZRot;

    
    // Use this for initialization
    void Start () {

        isVRObj = GameObject.Find("isVR");
        isvr = isVRObj.GetComponent<isVR>();
        isVRFlg = isvr.isVRFlg;


        quadRotation = transform.eulerAngles;

    }
	
	// Update is called once per frame
	void Update () {

        if (isVRFlg){
            quadRotation = transform.eulerAngles;

            //コントローラからトリガの情報を取得
            isSldrBtnClickedLeft = myControllerLeft.isSldrBtnClicked;
            isSldrBtnClickedRight = myControllerRight.isSldrBtnClicked;


            //Quad -> スライダ
            //スライダ更新用のボタンが押されてない状態のときのみ、スライダの値をQuadから更新する
            if (!isSldrBtnClickedLeft && !isSldrBtnClickedRight)
            {

                //ジンバルロック?が発生しているので，QuadのEulerをそのまま
                //sldrvalueに変換してはだめ
                XRot.value = (int)quadRotation.x;
                YRot.value = (int)quadRotation.y;
                ZRot.value = (int)quadRotation.z;

                Debug.Log(quadRotation + " " + Quaternion.Euler(quadRotation));
            }
        }//idVRFlg

    }
}
