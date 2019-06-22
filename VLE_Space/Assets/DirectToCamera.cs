using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectToCamera : MonoBehaviour
{
	public GameObject camera;
	public Vector3 initPos;
	public Quaternion initRot;

	public float speed = 1.0f;
	public float offset = 300.0f;
	public bool settingEarthPos = false;
	public bool landing = false;
	public bool landed = false;
	public bool walking = false;
	public GameObject aimPoint;

	public GameObject sun;
	public GameObject sunTx;

	float lsx;
	float lsy;
	float lsz;

	float landx;
	float landy;
	float landz;

    // Start is called before the first frame update
    void Start()
    {
    	RenderSettings.skybox.SetFloat("_SunSize", 0f);
    	RenderSettings.skybox.SetFloat("_Exposure", 0f);
    	RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1.89f);


    	sun = GameObject.Find("Sun");
    	sunTx = GameObject.Find("SunTx");
     	camera = GameObject.Find("Main Camera");
     	aimPoint = GameObject.Find("Japan");
     	initPos = transform.position;
     	initRot = transform.rotation;

     	int maxi = 10;
		landx = 7.0f;
		landy = 7.0f;
		landz = 7.0f;

		lsx = (landx - transform.localScale.x)/maxi;
		lsy = (landy - transform.localScale.y)/maxi;
		lsz = (landz - transform.localScale.z)/maxi;

		// sun.transform.parent = this.transform;
	    
    }

    // Update is called once per frame
    void Update()
    {	
    	float step =  speed * Time.deltaTime * 60; // calculate distance to move
    	// float dif = (camera.transform.position - transform.position).magnitude;
    	float dif = (camera.transform.position - aimPoint.transform.position).magnitude;
    	
    	if(Input.GetKey(KeyCode.R)){
	    	ResetPos();
	    }


    	if(dif >= offset){
	    	if(Input.GetKeyDown(KeyCode.A)){
	    		settingEarthPos = true;
	    	}
	    	if(Input.GetKeyUp(KeyCode.A)){
	    		settingEarthPos = false;
	    	}
	    }

    	if(settingEarthPos){
    		if(dif < offset){
    			settingEarthPos = false;
    		}
    		transform.position = Vector3.MoveTowards(transform.position, camera.transform.position, step);
    		// transform.position = Vector3.MoveTowards(aimPoint.transform.position, camera.transform.position, step);
    		// transform.LookAt(camera.transform.position);
    		Quaternion targetRotation = Quaternion.LookRotation(camera.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    		// transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(camera.transform.position), step);
    		// print(dif);
    	}

    	if(dif <= offset){
	    	if(Input.GetKey(KeyCode.S)){
	    		landing = true;
	    	}
	    }

	    if(landing){
	    	sunTx.GetComponent<SpriteRenderer>().enabled = false;
    		RenderSettings.skybox.SetFloat("_SunSize", 0.07f);
			RenderSettings.skybox.SetFloat("_Exposure", 1.3f);
    		RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1.0f);
 			
	    	transform.RotateAround(camera.transform.position, camera.transform.right, 80.0f * Time.deltaTime);
	    	// transform.localScale += new Vector3(lsx, lsy, lsz);
			// float ang = Vector3.Angle(camera.transform.up, transform.up);
	    	float ang = Vector3.Angle(camera.transform.up, aimPoint.transform.up);
	    	
	    	// if(Mathf.Abs(landx - transform.localScale.x) <= 0.1f){
	    		print(ang);
		    	if(Mathf.Abs(ang) <= 50.0f){
		    		print(transform.position);
		    		// transform.position = new Vector3(transform.position.x, transform.position.y + 60.0f, transform.position.z);
		    		print("aaa");
		    		print(transform.position);
		    		
		    		// transform.localScale += new Vector3(lsx, lsy, lsz);
		    		// print(lsx);
		    		// if(landx - transform.localScale.x <= 0.1f){
	    				sun.transform.parent = GameObject.Find("Env").transform;
	    				landed = true;
			    		landing = false;
			    		
			    	// }
		    	}
		    // }
	    }

	    if(landed){
	    // 	if(Input.GetKeyDown(KeyCode.W)){
	    // 		walking = true;
	    // 	}else if(Input.GetKeyUp(KeyCode.W)){
	    // 		walking = false;
	    // 	}
	    // }
		    if(Input.GetKey(KeyCode.W)){
		    	transform.position += -camera.transform.forward * speed * Time.deltaTime;
		    }else if(Input.GetKey(KeyCode.Z)){
		    	transform.position += camera.transform.forward * speed * Time.deltaTime;
		    }

		}

        // Check if the position of the cube and sphere are approximately equal.
        // if (Vector3.Distance(transform.position, camera.position) < 0.001f)
        // {
            // Swap the position of the cylinder.
            // target.position *= -1.0f;
    }//Update()


    public void ResetPos(){
	    sunTx.GetComponent<SpriteRenderer>().enabled = true;

    	transform.position = initPos;
    	transform.rotation = initRot;
    }
}
