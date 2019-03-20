using UnityEngine;
using System.Collections;

public class MY_TrackedController : MonoBehaviour
{
    SteamVR_TrackedController trackedController;
    public GameObject grababbleObject;
    FixedJoint joint;

    public bool isSldrBtnClicked;
    public bool isHelpMode;
    public bool isConstrainMode;
    public bool isMovingAxisMode;

    public bool isGrabbing;

    public Vector3 firstGrabedPos;
    public Quaternion firstGrabedQua;

    public Vector3 deltaPos;
    public Quaternion deltaQua;

    public Vector3 va, vb, v, w;
    public Vector3 vaDush, vbDush;

    public GameObject ControllerDummy;
    public GameObject Quad;
    public GameObject ConstrainAxis;

    public Vector3 vaDushProj, vbDushProj;

    public Material[] QuadMatDummy;
    public Renderer QuadRenderer;
    public Renderer ConstrainAxisRenderer;
    public Color[] ConstrainAxisColors;


    void Start()
    {
        isSldrBtnClicked = false;
        isHelpMode = false;
        isConstrainMode = false;
        isMovingAxisMode = false;

        firstGrabedPos = transform.position;
        firstGrabedQua = transform.rotation;

        trackedController = gameObject.GetComponent<SteamVR_TrackedController>();

        if (trackedController == null)
        {
            trackedController = gameObject.AddComponent<SteamVR_TrackedController>();
        }

        trackedController.MenuButtonClicked += new ClickedEventHandler(DoMenuButtonClicked);
        trackedController.MenuButtonUnclicked += new ClickedEventHandler(DoMenuButtonUnClicked);
        trackedController.TriggerClicked += new ClickedEventHandler(DoTriggerClicked);
        trackedController.TriggerUnclicked += new ClickedEventHandler(DoTriggerUnclicked);
        trackedController.SteamClicked += new ClickedEventHandler(DoSteamClicked);
        trackedController.PadClicked += new ClickedEventHandler(DoPadClicked);
        trackedController.PadUnclicked += new ClickedEventHandler(DoPadUnclicked);//ミスじゃｎ
        trackedController.PadTouched += new ClickedEventHandler(DoPadTouched);
        trackedController.PadUntouched += new ClickedEventHandler(DoPadTouched);
        trackedController.Gripped += new ClickedEventHandler(DoGripped);
        trackedController.Ungripped += new ClickedEventHandler(DoUngripped);

        joint = gameObject.GetComponent<FixedJoint>();


        //va = transform.position;
        //vb = grababbleObject.transform.position;
        

        v = va - vb;
        w = GameObject.Find("HelpModePoint").transform.forward;

        Quad = GameObject.Find("Quad");
        ControllerDummy = GameObject.Find("ControllerDummy");
        ConstrainAxis = GameObject.Find("ConstrainAxis");

        

        
        QuadMatDummy = GameObject.Find("QuadDummy").GetComponent<Renderer>().materials; //ダミーのマテリアル配列を格納

        QuadRenderer = Quad.GetComponent<Renderer>();
        QuadRenderer.material = QuadMatDummy[0];
        //grababbleObject.GetComponent<Renderer>().material = QuadMatDummy[0];
        //grababbleObject.GetComponent<Renderer>().materials[1] = null;

        ConstrainAxisRenderer = ConstrainAxis.GetComponent<Renderer>();

        ConstrainAxisColors = new Color[3];
        ConstrainAxisColors[0] = new Color(0, 15, 15, 0.1f);//通常
        ConstrainAxisColors[1] = new Color(0, 105, 105, 0.1f);//Enter
        ConstrainAxisColors[2] = new Color(0, 0, 0, 0.3f);//Constrain

        ConstrainAxis.SetActive(false);



    }//Start


    private void Update()
    {
        Debug.DrawLine(GameObject.Find("HelpModePoint").transform.position, w, Color.blue);

        if (isGrabbing)
        {
            if (grababbleObject != null)
            {
                if (isHelpMode || isConstrainMode)
                {

                    //va = transform.position;
                    va = ControllerDummy.transform.position;
                    vb = grababbleObject.transform.position;
                    //Vector2 wNormal = w.normalized;
                    //vaDush = Vector3.Dot(va, wNormal) * wNormal;
                    //vbDush = Vector3.Dot(vb, wNormal) * wNormal;

                    vaDushProj = Vector3.Project(va, w);//Vector3クラスに投影関数がある。引数に渡すwは正規化する必要は無い
                    vbDushProj = Vector3.Project(vb, w);
                    Debug.DrawLine(va, vb, Color.cyan);
                    Debug.DrawLine(vaDushProj, vbDushProj, Color.yellow);

                    grababbleObject.transform.position += (vaDushProj - vbDushProj);




                }
                ////isConstrainMode
                //else if (isConstrainMode)
                //{






                //Default or isMovingAxisMode
                //}
                else {
                    /*
                     * 頑張っていろいろやったけどダミーのやり方が一番スマート
                     * 
                    //最初につかんだ位置とコントローラの距離を保つため、Quadの位置からオフセットを引いておく

                    //deltaPos = transform.position - grababbleObject.transform.position;
                    //deltaQua = ControllerDummy.transform.rotation * Quaternion.Inverse(grababbleObject.transform.rotation);

                    //Vector3 grababblePosWithOffset = grababbleObject.transform.position - firstGrabedPos;
                    //grababbleObject.transform.position = grababblePosWithOffset + deltaPos; //常にオフセットを加味した位置にdeltaを足していく
                    //grababbleObject.transform.position += deltaPos;

                    //距離のあるオブジェクトをいい感じに追従させるためにRotateAroundが使えた。
                    //float angle;
                    //Vector3 axis;
                    //deltaQua.ToAngleAxis(out angle, out axis);
                    //grababbleObject.transform.RotateAround(transform.position, axis, angle);
                    */


                    //grab開始直前までダミーをQuadの位置と同期させて、
                    //grab開始直後にダミーの移動回転をコントローラに依拠するようにすれば
                    //初期位置ずれなく追従させられる
                    grababbleObject.transform.position = ControllerDummy.transform.position;
                    grababbleObject.transform.rotation = ControllerDummy.transform.rotation;

             



               }//Modes



            }//if(grababbleObject != null)


        }//if(isGrabbing)





        if (Input.GetKeyDown(KeyCode.D))
        {
            ConstrainAxis.SetActive(false);

            isMovingAxisMode = false;
            isConstrainMode = false;
            isHelpMode = false;

            Quad.layer = LayerMask.NameToLayer("Grabbable");
            ConstrainAxis.layer = LayerMask.NameToLayer("NonGrabbable");

        }//Input D



        ///*
        if (Input.GetKeyDown(KeyCode.O))
        {
            isConstrainMode = false;
            isHelpMode = false;

            //isMovingAxisMode
            if (!isMovingAxisMode)
            {
                ConstrainAxis.SetActive(true);
                ConstrainAxisRenderer.material.color = ConstrainAxisColors[0];

                //Quad.layer = LayerMask.NameToLayer("NonGrabbable");
                Quad.layer = LayerMask.NameToLayer("Grabbable");
                ConstrainAxis.layer = LayerMask.NameToLayer("Grabbable");

                isMovingAxisMode = true;
                
            }

        }//Input O
        //*/


        ///*
        if (Input.GetKeyDown(KeyCode.P))
        {
            isMovingAxisMode = false;
            isHelpMode = false;

            //isConstrainMode
            if (!isConstrainMode)
            {
                ConstrainAxis.SetActive(true);
                ConstrainAxis.GetComponent<Renderer>().material.color = ConstrainAxisColors[2];

                Quad.layer = LayerMask.NameToLayer("Grabbable");
                ConstrainAxis.layer = LayerMask.NameToLayer("NonGrabbable");

                w = ConstrainAxis.transform.up;

                isConstrainMode = true;
            }

        }//Input P
        //*/



        if (Input.GetKeyDown(KeyCode.S))
        {
            isMovingAxisMode = false;
            isConstrainMode = false;

            if (!isHelpMode)
            {
                Quad.GetComponent<QuadAdjuster>().isMovingToHelpMode = true;
                ConstrainAxis.SetActive(false);

                GameObject.Find("Quad").GetComponent<QuadAdjuster>().helpModePos = GameObject.Find("HelpModePoint").transform.position;
                GameObject.Find("Quad").GetComponent<QuadAdjuster>().helpModeRot = GameObject.Find("HelpModePoint").transform.eulerAngles;
                w = GameObject.Find("HelpModePoint").transform.forward;

                isHelpMode = true;
            }

        }//Input S


        




    }//Update



    public void DoMenuButtonClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoMenuButtonClicked");
    }

    public void DoMenuButtonUnClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoMenuButtonUnClicked");
    }

    public void DoTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (!isSldrBtnClicked)
        {
            //grab();
            grabViaTransform();
        }
    }

    public void DoTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        //release();
        releaseViaTransform();
    }

    public void DoSteamClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoSteamClicked");
    }

    public void DoPadClicked(object sender, ClickedEventArgs e)
    {
        isSldrBtnClicked = true;
        Debug.Log("DoPadClicked");
    }

    public void DoPadUnclicked(object sender, ClickedEventArgs e)
    {
        isSldrBtnClicked = false;
        Debug.Log("DoPadUnclicked");
    }

    public void DoPadTouched(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoPadTouched");
    }

    public void DoPadUntouched(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoPadUntouched");
    }

    public void DoGripped(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoGripped");
    }

    public void DoUngripped(object sender, ClickedEventArgs e)
    {
        Debug.Log("DoUngripped");
    }

    void grab()
    {
        Debug.Log("grabbbbbbbbbbbb");
        //Debug.Log(grababbleObject.GetComponent<Rigidbody>());

        if (grababbleObject == null || joint.connectedBody != null)
        {
            return;
        }
        
        joint.connectedBody = grababbleObject.GetComponent<Rigidbody>();
    }

    void release()
    {
        Debug.Log("reeeeeeeeeeeeeeeee");
        if (joint.connectedBody == null)
        {
            return;
        }

        joint.connectedBody = null;
    }





    public void grabViaTransform()
    {
        Debug.Log("grabbb");

        if (grababbleObject != null)
        {
            isGrabbing = true;
        }
    }

    public void releaseViaTransform()
    {
        Debug.Log("releaseeee");
        grababbleObject = null;
        isGrabbing = false;
       //grababbleObject = null;
       
    }






    void OnTriggerEnter(Collider other)
    {

        if (!isGrabbing) {
            grababbleObject = other.gameObject;

            //ConstrainAxisがあったら優先する
            if (isMovingAxisMode) {
                if (other.name == "ConstrainAxis") {
                    grababbleObject = other.gameObject;
                }
            }

            
            ///*
            //接触判定時に色変え
            if (other.name == "Quad")
            {
                QuadRenderer.material = QuadMatDummy[1];//

            }
            else if (other.name == "ConstrainAxis")
            {
                ConstrainAxisRenderer.material.color = ConstrainAxisColors[1];
            }//
            //*/


        }//isGrabbing


       

        Debug.Log("enterrrrrrrrrrrrrrr in " + other.gameObject);
    }//Enter



    void OnTriggerExit(Collider other)
    {
        if (!isGrabbing)
        {
            ControllerDummy.transform.position = transform.position;
            ControllerDummy.transform.rotation = transform.rotation;
            //grababbleObject = null;

        }


        ///*
        //接触判定時に色変え
        if (other.name == "Quad")
        {
            QuadRenderer.material = QuadMatDummy[0];
        }
        else if (other.name == "ConstrainAxis")
        {
            ConstrainAxisRenderer.material.color = ConstrainAxisColors[0];
        }//
        //*/

        Debug.Log("exittttttttttttttttt");
    }//Exit



    
    private void OnTriggerStay(Collider other)
    {
        //初期位置の更新。つかんでるときは更新しない
        if (!isGrabbing)
        {
            if (grababbleObject != null && (other.name == "Quad" || other.name == "ConstrainAxis"))
            {
                Debug.DrawLine(grababbleObject.transform.position, transform.position, Color.green);

                //grab開始直前までダミーの位置回転をgrababbleObjectに同期させて、
                //grab開始直後からダミーの位置をコントローラで制御&&grababbleObjectをそれに追従させる
                ControllerDummy.transform.rotation = grababbleObject.transform.rotation;
                ControllerDummy.transform.position = grababbleObject.transform.position;
                
                /*
                 * 頑張っていろいろやったけどダミーのやり方が一番スマート
                //firstGrabedPos = transform.position - other.ClosestPointOnBounds(this.transform.position);
                //firstGrabedQua = transform.rotation * Quaternion.Inverse(grababbleObject.transform.rotation);
                 */
            }
        }//if !isGrabbing

    }//OnTriggerStay

}//class