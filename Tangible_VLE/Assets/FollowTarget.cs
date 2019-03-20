using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    public GameObject followTarget;
    public Vector3 initDistanceMenuCubeToConotroller;

	// Use this for initialization
	void Start () {
		followTarget = GameObject.Find("MenuCube");
        //initDistanceMenuCubeToConotroller = transform.position - followTarget.transform.position;
        initDistanceMenuCubeToConotroller = new Vector3(0.0f, 1.5f, 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = followTarget.transform.position;
        transform.position = followTarget.transform.position;// + initDistanceMenuCubeToConotroller;
        //transform.rotation = GameObject.Find("MenuCube").transform.rotation;
        transform.rotation = followTarget.transform.rotation;

	}
}
