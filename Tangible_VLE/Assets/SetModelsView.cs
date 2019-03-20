using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModelsView : MonoBehaviour {
	//public bool isEarthModelsView;

    public GameObject _Terrain;
    public GameObject _Sun_right;
    public GameObject _Sun_left;
    public GameObject _Earth;
    public GameObject _Lamp;

	// Skyboxのマテリアル
	public Material skybox;

	// Use this for initialization
	void Start () {
        _Terrain = GameObject.Find ("Terrain");
        _Sun_left = GameObject.Find ("Sun_left");
        _Sun_right = GameObject.Find ("Sun_right");
        _Earth = GameObject.Find ("Earth");
        _Lamp = GameObject.Find ("Lamp");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}//Upate

    public void SetEarthModelsView(){

        RenderSettings.skybox = skybox; // Skyboxを変更する

		_Terrain.SetActive (true);
		_Sun_left.GetComponent<MeshRenderer>().enabled = false;
        _Sun_right.GetComponent<MeshRenderer>().enabled = false;
		_Earth.SetActive (false);
        _Lamp.SetActive(true);
        

    }


    public void UnSetEarthModelsView(){

        _Terrain.SetActive (false);
		_Sun_left.GetComponent<MeshRenderer>().enabled = true;
        _Sun_right.GetComponent<MeshRenderer>().enabled = true;
		_Earth.SetActive (true);
        _Lamp.SetActive(false);
        

    }
}
