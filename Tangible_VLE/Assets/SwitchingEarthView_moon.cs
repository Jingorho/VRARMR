using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingEarthView_moon : MonoBehaviour {
    public GameObject _Environment;
    public GameObject _Earth;
    public GameObject _Terrain;
    public GameObject _DummyTerrainEdge;

	// Use this for initialization
	void Start () {
		_Environment = GameObject.Find("Environment");
        _Earth = GameObject.Find("Earth");
        _Terrain = GameObject.Find("Terrain");
        _DummyTerrainEdge = GameObject.Find("DummyTerrainEdge");
	}
	
	// Update is called once per frame
	void Update () {
        
        //if(Input.GetKeyDown(KeyCode.H)){
		if(_Environment.GetComponent<SetUnsetEarthView>().adjustPosRot){
            float EarthToMoonDistance = Vector3.Distance(transform.position, _Earth.transform.position);
            float EarthToTerrainDistance = Vector3.Distance( _DummyTerrainEdge.transform.position, _Earth.transform.position);//Vector3で(w,h,d)

            if(EarthToMoonDistance < EarthToTerrainDistance){
                transform.position += (0.5f * (EarthToTerrainDistance/EarthToMoonDistance)) * (transform.position - _Earth.transform.position);
                transform.localScale = new Vector3(1200, 1200, 1200);

            }else {
                _Environment.GetComponent<SetUnsetEarthView>().adjustPosRot = false;
            }
            Debug.Log("Moon: " + EarthToMoonDistance + " Terrain:" + EarthToTerrainDistance + " " + (EarthToTerrainDistance/EarthToMoonDistance));
            
        }//if adjustPosRot
	}//Update
}
