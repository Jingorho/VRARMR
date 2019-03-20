using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadAdjuster : MonoBehaviour
{

    public float moveSpeed;
    public float rotSpeed;

    public Vector3 initRot;
    public Vector3 initPos;

    public Vector3 helpModePos;
    public Vector3 helpModeRot;

    public bool isHelpModeByKey;
    public bool isMovingToHelpMode;
    public float timeOut;

    // Use this for initialization
    void Start()
    {
        rotSpeed = 0.3f;
        moveSpeed = 0.03f;
        timeOut = 0f;

        initPos = transform.position;
        initRot = transform.eulerAngles;

        helpModePos = GameObject.Find("HelpModePoint").transform.position;
        helpModeRot = GameObject.Find("HelpModePoint").transform.eulerAngles;

        isHelpModeByKey = false;
        isMovingToHelpMode = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position -= transform.forward * moveSpeed;
            }
            else
            {
                //transform.Rotate(new Vector3(0, 1.0f * rotSpeed, 0), Space.World);
                transform.Rotate(new Vector3(0, 1.0f * rotSpeed, 0));

            }
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += transform.forward * moveSpeed;
            }
            else
            {

                //transform.Rotate(new Vector3(0, -1.0f * rotSpeed, 0), Space.World);
                transform.Rotate(new Vector3(0, -1.0f * rotSpeed, 0));
            }
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position -= transform.up * moveSpeed;
            }
            else
            {
                //transform.Rotate(new Vector3(-1.0f * rotSpeed, 0, 0), Space.World);
                transform.Rotate(new Vector3(-1.0f * rotSpeed, 0, 0));
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += transform.up * moveSpeed;
            }
            else
            {
                //transform.Rotate(new Vector3(1.0f * rotSpeed, 0, 0), Space.World);
                transform.Rotate(new Vector3(1.0f * rotSpeed, 0, 0));
            }
        }

        else if (Input.GetKey(KeyCode.Z))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position += transform.right * moveSpeed;
            }
            else
            {
                //transform.Rotate(new Vector3(0, 0, -1.0f * rotSpeed), Space.World);
                transform.Rotate(new Vector3(0, 0, -1.0f * rotSpeed));
            }
        }

        else if (Input.GetKey(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.position -= transform.right * moveSpeed;
            }
            else
            {
                //transform.Rotate(new Vector3(0, 0, 1.0f * rotSpeed), Space.World);
                transform.Rotate(new Vector3(0, 0, 1.0f * rotSpeed));
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRotation();
            Debug.Log("Reset!!!!!!!!!!!");
        }

        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            //切り替え
            if (isHelpModeByKey)
            {
                isHelpModeByKey = false;
            }
            else
            {
                isHelpModeByKey = true;
                isMovingToHelpMode = true;
            }


            //コントローラ側(MY_TrackerController.cs内の変数)に切り替えた後の値を代入
            //if (GameObject.Find("Controller (right)").activeSelf)
            //{
                GameObject.Find("Controller (right)").GetComponent<MY_TrackedController>().isHelpMode = isHelpModeByKey;
            //}
            //if (GameObject.Find("Controller (left)").activeSelf)
            //{
                GameObject.Find("Controller (left)").GetComponent<MY_TrackedController>().isHelpMode = isHelpModeByKey;
            //}
        }//S
        */



        //Sキーが押されたら、Quadを補助モードの座標に。滑らかに処理したいので、Update関数の中で処理
        if (isMovingToHelpMode)
        {
            timeOut += 0.01f;
            //スピードは割と速めでいいかな
            transform.position = Vector3.MoveTowards(transform.position, helpModePos, 2.0f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(helpModeRot), 90.0f * Time.deltaTime);

            //滑らか処理じゃなくする場合はこう
            //transform.position = helpModePos;
            //transform.eulerAngles = helpModeRot;

            //目的地に到着したらこのifを抜ける
            if (timeOut > 1.1f)

            //if ((transform.position == helpModePos) && (transform.rotation == Quaternion.Euler(helpModeRot)))
            {
                isMovingToHelpMode = false;
                timeOut = 0f;
            }
        }//isMovingToHelpMode




    }//Update()


    public void ResetRotation()
    {
        transform.position = initPos;
        transform.eulerAngles = initRot;
    }


}//class