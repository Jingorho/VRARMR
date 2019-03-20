using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirectionLightAsSun : MonoBehaviour {

	[SerializeField] public float _sunAngle;
	[SerializeField] public Vector3 _sunVec;

	// Use this for initialization
	void Start () {
		_sunAngle = 30.0f;
		_sunVec = new Vector3(30.0f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		_sunAngle = ViewSwitcher.sunAngle;
		_sunVec = ViewSwitcher.sunVec;
		//transform.rotation = Quaternion.Euler( (-1) * _sunAngle, 0, 0) ;
		transform.rotation = Quaternion.Euler( _sunVec ) ;
	}
}
