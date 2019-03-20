using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsSwitcher : MonoBehaviour {

    public GameObject cube;
    public GameObject rectangular;
    public GameObject regTriPyramid;
    public GameObject regSqPyramid;
    public GameObject cylinder;
    public GameObject cone;
    public GameObject sphere;

    public GameObject active3DModel;
    public GameObject characterUI;
    public GameObject points;

    public GameObject MainCameraDummy;

    public GameObject CanvasForWipe;
    public GameObject GuideText;


	// Use this for initialization
	void Start () {
        cube = GameObject.Find("Cube");
        rectangular = GameObject.Find("Rectangular");
        regTriPyramid = GameObject.Find("RegTriPyramid");
        regSqPyramid = GameObject.Find("RegSqPyramid");
        cylinder = GameObject.Find("Cylinder");
        cone = GameObject.Find("Cone");
        sphere = GameObject.Find("Sphere");

        MainCameraDummy = GameObject.Find("MainCameraDummy");

        CanvasForWipe = GameObject.Find("CanvasForWipe");
        GuideText = GameObject.Find("GuideText");


        cube.SetActive(true);
        rectangular.SetActive(false);
        regTriPyramid.SetActive(false);
        regSqPyramid.SetActive(false);
        cylinder.SetActive(false);
        cone.SetActive(false);
        sphere.SetActive(false);

        CanvasForWipe.SetActive(false);
        GuideText.SetActive(false);

        active3DModel = cube;
        characterUI = active3DModel.transform.Find("CharUI").gameObject;
        points = active3DModel.transform.Find("points").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cube.SetActive( !cube.activeSelf );
            if(cube.activeSelf){
                active3DModel = cube;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            regSqPyramid.SetActive(!regSqPyramid.activeSelf);
            if (regSqPyramid.activeSelf)
            {
                active3DModel = regSqPyramid;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            regTriPyramid.SetActive(!regTriPyramid.activeSelf);
            if (regTriPyramid.activeSelf)
            {
                active3DModel = regTriPyramid;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cone.SetActive(!cone.activeSelf);
            if (cone.activeSelf)
            {
                active3DModel = cone;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cylinder.SetActive(!cylinder.activeSelf);
            if (cylinder.activeSelf) {
                active3DModel = cylinder;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            rectangular.SetActive(!rectangular.activeSelf);
            if (rectangular.activeSelf)
            {
                active3DModel = rectangular;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            sphere.SetActive(!sphere.activeSelf);
            if (sphere.activeSelf) {
                active3DModel = sphere;
            }
        }


        if(Input.GetKeyDown(KeyCode.C)){
            characterUI = active3DModel.transform.Find("CharUI").gameObject;
            characterUI.SetActive ( !characterUI.activeSelf);
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            points = active3DModel.transform.Find("points").gameObject;
            points.SetActive(!points.activeSelf);
        }


        //カメラの方向を向かせる
        if(characterUI.activeSelf) {
            List<GameObject> childList = GetAllChildren.GetAll(characterUI);
            foreach (GameObject obj in childList) {
                obj.transform.LookAt( MainCameraDummy.transform.position );
            }         
        }



        if (Input.GetKeyDown(KeyCode.W))
        {
            CanvasForWipe.SetActive(!CanvasForWipe.activeSelf);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            GuideText.SetActive(!GuideText.activeSelf);
        }

    }//Update()
}
