using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
 
public class ViewSwitcher : MonoBehaviour {

	public GameObject _Earth;
	public GameObject _Sun;
	public GameObject _Moon;
	public GameObject _North1;
	public GameObject _CameraRig;
    public GameObject _Environment;

	[SerializeField]Text ViewText;
	[SerializeField]
    public Toggle ActualSizeToggle;
	[SerializeField]Text angle;

	[SerializeField]Toggle ViewAxisToggle;
	[SerializeField]
    public Toggle ResetRotationToggle;
	
	public float deltaAngle = 90.0F;
	
	public float speed = 1.0f;
	public float EarthToCameraAngle = 30.0f;

	public bool isRotatingView;
	public bool isEarthView;
	public bool isSpaceView;

	public bool isActualSize;
	public bool isViewAxisAndFixRotation;
    public bool isReset;

	public bool isResettingRotation;

	public bool isMovingEnvironment;
	public int changingViewTransit = -1;

	public Quaternion fromRotation;
	public Quaternion toRotation;

	Quaternion toTargetRot;
	Quaternion requiredRot;

	public Vector3 fromPosition_view;
	public Vector3 toPosition_view;
	public Quaternion fromRotation_view;
	public Quaternion toRotation_view;

	public float subAngle;

	public float t = 0;
	public float rotateDurationTime;
	public float switchViewDurationTime;

	public Vector3 initPos_moon;
	public Quaternion initRot_moon;
	public Vector3 initPos_earth;
	public Quaternion initRot_earth;
	public Vector3 initPos_sun;
	public Quaternion initRot_sun;

	[SerializeField]
	public static float sunAngle;
	[SerializeField]
	public static Vector3 sunVec;

	public Vector3 earthVec;
	public Vector3 cameraVec;
	public Vector3 northVec;
	public Vector3 northPos;

	public float distance_North1ToEarth;
	public Vector3 North1ToEarthVec;

    public Transform rotateTarget;
    public Vector3 rotateTargetPositon;
    public Quaternion rotateTargetRotation;
    public Quaternion rotateTargetRotationOrig;
    

    void Awake(){
        //DontDestroyOnLoad(GameObject.Find("Environment"));
        //DontDestroyOnLoad(GameObject.Find("SteamVR"));
    }

	// Use this for initialization
	void Start () {

		_Earth = GameObject.Find("Earth");
		_Moon = GameObject.Find("Moon");
		_Sun = GameObject.Find("Sun");
		_North1 = GameObject.Find("North1");
		_CameraRig = GameObject.FindWithTag("CameraRig");
        _Environment = GameObject.Find("Environment");

		changingViewTransit = -1;

		isRotatingView = false;

		isEarthView = false;
		isSpaceView = true;
		isActualSize = false;
		isViewAxisAndFixRotation = false;
        isReset = false;

		isResettingRotation = false;

		isMovingEnvironment = false;

		rotateDurationTime = 1.0f;
		switchViewDurationTime = 2.0f;

		earthVec = _Earth.transform.eulerAngles;

		initPos_moon = _Moon.transform.position;
		initRot_moon = _Moon.transform.rotation;
		initPos_earth = _Earth.transform.position;
		initRot_earth = _Earth.transform.rotation;
		initPos_sun = _Sun.transform.position;
		initRot_sun = _Sun.transform.rotation;

		sunAngle = 0.0f;
		sunVec = new Vector3(0, 0, 0);

		distance_North1ToEarth = Vector3.Distance(_Earth.transform.position, _North1.transform.position);


	}//Start()



	// Update is called once per frame
	void Update () {

		earthVec = _Earth.transform.eulerAngles;
		//cameraVec = _CameraRig.transform.eulerAngles;
		northVec = _North1.transform.eulerAngles;
		northPos = _North1.transform.position;
 
		sunAngle = Vector3.Angle(
			new Vector3(
				_North1.transform.eulerAngles.x,
				_North1.transform.eulerAngles.y,
				_North1.transform.eulerAngles.z),
			new Vector3(
				_Sun.transform.eulerAngles.x,
				_Sun.transform.eulerAngles.y,
				_Sun.transform.eulerAngles.z) );
					
		sunVec = 
				new Vector3(
					_Sun.transform.eulerAngles.x,
					_Sun.transform.eulerAngles.y,
					_Sun.transform.eulerAngles.z)
				-
				new Vector3(
					_North1.transform.eulerAngles.x,
					_North1.transform.eulerAngles.y,
					_North1.transform.eulerAngles.z);
		angle.text = sunAngle.ToString();


		if (Input.GetKey(KeyCode.R)) {
			ResetPosition();
		}


		//1回押下でSlerp発動するためDown
		if (Input.GetKeyDown(KeyCode.T)) {
			ResetRotation();
			t = 0;
		}

        if(Input.GetKeyDown(KeyCode.D)){
             cameraVec = _CameraRig.transform.eulerAngles;
        }

        if (Input.GetKey(KeyCode.K)) {
            cameraVec.x = _CameraRig.transform.eulerAngles.x;
			//for Debug
            if(GameObject.Find("Cube").activeSelf != false) { 
			    GameObject.Find("Cube").transform.eulerAngles = new Vector3(cameraVec.x, cameraVec.y, cameraVec.z);
			    GameObject.Find("Cube_bk").transform.eulerAngles = cameraVec;
            }

		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			if(!isActualSize){
				SetPositionToActualSize();
				//isActualSize = true;
			}else{
				ResetPosition();
				//isActualSize = false;
			}
		}


		if (Input.GetKeyDown(KeyCode.X)){
			if (!isViewAxisAndFixRotation) {
				GameObject.Find("EarthAxis").GetComponent<MeshRenderer>().enabled = true;
				ViewAxisToggle.isOn = true;
			}else{
				GameObject.Find("EarthAxis").GetComponent<MeshRenderer>().enabled = false;
				ViewAxisToggle.isOn = false;
			}
			isViewAxisAndFixRotation = !isViewAxisAndFixRotation;
		}

		/*
		if (Input.GetKeyDown(KeyCode.Space)) {
			isRotatingView = true;
			
			//Environmentの今のRotation
			fromRotation = Quaternion.Euler(new Vector3(
				transform.localEulerAngles.x, 
				transform.localEulerAngles.y,
				transform.localEulerAngles.z));

			//Environmentの今のRotationにdeltaAngleだけ足した角度
			toRotation = Quaternion.Euler(new Vector3(
				transform.localEulerAngles.x, 
				transform.localEulerAngles.y + deltaAngle,
				transform.localEulerAngles.z));
			t = 0;
		}
		*/


		if (Input.GetKeyDown(KeyCode.S)) {
			t = 0;
			isSpaceView = true;
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			//SpaceViewからEarthViewに切り替え
			//if(isSpaceView){
			
			GameObject target = _North1;
			GameObject mainCamera = _CameraRig;

			// ターゲットの位置からカメラの方向に向くための回転
			toTargetRot = Quaternion.LookRotation(mainCamera.transform.position - target.transform.position);
			// ターゲットの現在の向きからカメラの方向に向くために必要な回転
			requiredRot = toTargetRot * Quaternion.Inverse(target.transform.rotation);


			//HMD視点-Earth間ベクトル
			Vector3 deltaVec = _CameraRig.transform.position - _North1.transform.position;

			fromPosition_view = transform.position; //Environment(Earth)のposition
			toPosition_view = transform.position + deltaVec; //Environmentを、HMD視点-Earth間ベクトル分だけ平行移動した位置

			fromRotation_view = Quaternion.Euler( _North1.transform.eulerAngles );
			toRotation_view = Quaternion.Euler( _CameraRig.transform.eulerAngles );

			toRotation_view = Quaternion.Euler(fromRotation_view.eulerAngles + requiredRot.eulerAngles);


			subAngle = Vector3.Angle(
				new Vector3(
				_North1.transform.eulerAngles.x,
				_North1.transform.eulerAngles.y,
				_North1.transform.eulerAngles.z),
				new Vector3(
				_CameraRig.transform.eulerAngles.x,
				_CameraRig.transform.eulerAngles.y,
				_CameraRig.transform.eulerAngles.z));
			
			t = 0;

			isEarthView = true;
			isMovingEnvironment = true;

            _Moon.transform.parent = _Earth.transform;
			_Sun.transform.parent = _Earth.transform;

		}//if E

		

		if(isRotatingView){
			
			if(t < 1){
				t += Time.deltaTime/rotateDurationTime;
				//2点間の弧状補間ベクトルを求めるSlerp(from, to, 割合)。割合を徐々に増加させ、移動を図る
				transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);
				RenderSettings.skybox.SetFloat("_Rotation", -(transform.localEulerAngles.y + 1/90) );
			}else{ 
				isRotatingView = false;
			}
		}//if RotatingView



		if(isEarthView){
			ViewText.text = "Earth View [Key: S]";

			if (isMovingEnvironment){
				if (t <= 1){
					if(changingViewTransit == 3){

						/*********************** Environment移動フェーズ ***********************/
                         /*
                         * - カメラが地球上に降り立つように、地球（と宇宙全体）を回転させるフェーズ
                         * - 2. カメラを中心として、カメラのx軸方向の回転。カメラが少し地面を向くように
                         * - 3. カメラをNorth1よりも近く、地球に接しない程度に地球の中心に向かって移動させる
                         */

						t += Time.deltaTime / switchViewDurationTime;

                        
                        /*
						transform.RotateAround(
							_Earth.transform.position,
                            //_CameraRig.transform.position, 
							new Vector3(_CameraRig.transform.eulerAngles.x, cameraVec.y, cameraVec.z),
                            //new Vector3(InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x, 0, 0),
                            //GameObject.Find("DummyDirection_forRotateX").transform.eulerAngles,
							-30 * Time.deltaTime);
						*/
                        

					   ///*
						transform.position = Vector3.Lerp(
							transform.position,
							transform.position + North1ToEarthVec,
							(distance_North1ToEarth - _Earth.transform.localScale.x/2) * 0.3f * Time.deltaTime);
						//*/
						
						if (t >= 1){
                            //Earthの回転に追従させたかったので親子関係を使って無理やり回転させた. 
				            //申し訳ないのでここでリセットする
				            _Moon.transform.parent = null;
				            _Sun.transform.parent = null;
				            _Moon.transform.parent = GameObject.Find("Environment").transform;
				            _Sun.transform.parent = GameObject.Find("Environment").transform;
				            
							changingViewTransit = -1;//トランジットの順番をリセットして
							isMovingEnvironment = false;//トランジットのフェーズを抜ける
                            
						}//リセット

					} else if (changingViewTransit == 2){

						/*********************** Environment回転調整フェーズ ***********************/
                        /*
                         * - カメラが地球上に降り立つように、地球（と宇宙全体）を回転させるフェーズ
                         * - 1. カメラを中心として、カメラのy軸方向の回転。カメラと反対方向を向くように
                         */
                        
						t += Time.deltaTime / switchViewDurationTime;

						transform.RotateAround(
							_CameraRig.transform.position,
							new Vector3(0, _CameraRig.transform.eulerAngles.y, 0), 
							90 * Time.deltaTime);
                        //*/
					   
						if (t >= 1){
							North1ToEarthVec = _North1.transform.position - _Earth.transform.position;
							changingViewTransit = 3;
                            cameraVec = _CameraRig.transform.eulerAngles;
							t = 0;
						}//リセット

					} else if (changingViewTransit == 1){

						/*********************** Environment移動フェーズ ***********************/
                        /*
                         * - カメラに向かって地球（と宇宙全体）を近づける
                         * - 地球上の指定した地域（North1）に接触したら止める（North1をカメラで見下ろすような視点）
                         */

						t += Time.deltaTime / switchViewDurationTime;
						//2点間の線形補間ベクトル（弧状補間ベクトルは、DollyZoomの実装にあたって無駄な機能と思われたためやめた）を
						//求めるSlerp(from, to, 割合)。割合を徐々に増加させ、移動を図る
						transform.position = Vector3.Lerp(fromPosition_view, toPosition_view, t);//回転と違いSkyboxは移動させようがない
						if (t >= 1){
							changingViewTransit = 2;
							t = 0;
						}//タイマーリセット

					} else if (changingViewTransit == 0){

						/*********************** Environment回転フェーズ ***********************/
                        /* 
                         * - 地球上で指定した地域（North1）を、地球を軸の中心としてカメラに相対する方向（DummyDirection）に向かせる
                         * - 同時に、地球の地軸の傾きをカメラのローカルy軸方向に平行にする（ルートパースペクティブに
                         *   なった時点で地球の座標空間は実世界のy方向（HMDつけて立ってる空間）に対応するべきだから）
                         */

						t += Time.deltaTime / switchViewDurationTime;

						

						// 指定した方向への滑らかな回転は以下を参考にした
						// http://tama-lab.net/2017/06/unity%E3%81%A7%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E3%82%92%E5%9B%9E%E8%BB%A2%E3%81%95%E3%81%9B%E3%82%8B%E6%96%B9%E6%B3%95%E3%81%BE%E3%81%A8%E3%82%81/
						//CameraRigのy軸逆方向を向くダミーオブジェクト. Qua -> Vecで-1かける -> Quaに戻すの計算だとy軸方向の計算に狂いが出る
						rotateTarget = GameObject.Find("DummyDirection").transform;
						rotateTargetPositon = rotateTarget.transform.position;
						//Transform target = GameObject.Find("DebugCamera").transform; Vector3 targetPositon = target.transform.position;
						// 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
						if (_Earth.transform.position.y != rotateTarget.transform.position.y){
							rotateTargetPositon = new Vector3(rotateTarget.transform.position.x, transform.position.y, rotateTarget.transform.position.z);
						}
						// Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
						// Quaternion targetRotation =  Quaternion.Euler( (-1) * target.rotation.eulerAngles + new Vector3 ( 0, _North1.transform.localEulerAngles.y, 0)  );
						rotateTargetRotation = Quaternion.Euler(rotateTarget.transform.rotation.eulerAngles + new Vector3(0, (-1) * _North1.transform.localEulerAngles.y, 0));
						rotateTargetRotationOrig = rotateTarget.transform.rotation;
                        
						//Earthを回転させる. 
						_Earth.transform.rotation = Quaternion.Slerp(_Earth.transform.rotation, rotateTargetRotation, Time.deltaTime);
						//Skyboxを回転させる. http://makers.hatenablog.com/entry/2014/03/01/053213


                        

						if (t >= 1) {

                            /*
                             *  Environment移動フェーズのために回転後の位置関係から移動初期値設定  
                             */

						    //HMD視点-_North1間ベクトル
						    Vector3 deltaVec = _CameraRig.transform.position - _North1.transform.position;

						    fromPosition_view = transform.position; //Environment(Earth)のposition
						    toPosition_view = transform.position + deltaVec; //Environmentを、HMD視点-Earth間ベクトル分だけ平行移動した位置

							changingViewTransit = 1;
							t = 0;

						}//タイマーリセット

					}else if(changingViewTransit == -1) {
                        t += Time.deltaTime / switchViewDurationTime;
                        /*
                         *  地軸の傾きを補正
                         */
                        rotateTarget = _CameraRig.transform;
						rotateTargetPositon = rotateTarget.transform.position;

                        if (_Earth.transform.position.y != rotateTarget.transform.position.y){
							rotateTargetPositon = new Vector3(rotateTarget.transform.position.x, transform.position.y, rotateTarget.transform.position.z);
						}

                        //回転して向かせたい向き（回転）をQuaternionで指定
						//rotateTargetRotation = Quaternion.Euler( new Vector3(0, _CameraRig.transform.eulerAngles.y, 0) ); // = Quaternion.LookRotation(targetPositon - transform.position);
                        rotateTargetRotation = _CameraRig.transform.rotation;

                        //Quaternion.Slep(from, to, rate)
						_Earth.transform.rotation = Quaternion.Slerp(_Earth.transform.rotation, rotateTargetRotation, Time.deltaTime);


                        if (t >= 1) {
                            changingViewTransit = 0;
							t = 0;
                        }
						
                    }// if(changingViewTransit)


				}//if t<1
			
			
			//if isMovingEnvironment
			} else {
                /* 視点切り替えフェーズが終わり、切り替え時の座標調整の準備 */
				SteamVR_Fade.Start(Color.black, 5.0f); 

				_CameraRig.GetComponent<PositionRecenter> ().SetPos();
				_Environment.GetComponent<SetUnsetEarthView> ().adjustPosRot = true;
				_Environment.GetComponent<SetModelsView> ().SetEarthModelsView();
                
				//SceneManager.LoadScene ("EarthViewScene");
				isEarthView = false;
			}//if isMovingEnvironment
		}//if isEarthView


		if(isSpaceView){
			ViewText.text = "Space View [Key: E]";
			_Environment.GetComponent<SetUnsetEarthView> ().adjustPosRot = false;
			_Environment.GetComponent<SetModelsView> ().UnSetEarthModelsView();


			if(t < 1){
				t += Time.deltaTime/switchViewDurationTime;
				//2点間の弧状補間ベクトルを求めるSlerp(from, to, 割合)。割合を徐々に増加させ、移動を図る
				transform.position = Vector3.Lerp(toPosition_view, fromPosition_view, t);//回転と違いSkyboxは移動させようがない
				//transform.rotation = Quaternion.Slerp(fromRotation_view, toRotation_view, t);
			
			}else{ 
				isSpaceView = false;
				//ViewText.text = "Earth View";
			}//if t<1
		}//if isSpaceView

		/*
		if(isActualSize){ 
		}else{
		}
		*/


		if(isResettingRotation){
			//回転結構時間かかるのでこの場合は2秒かける
			if(t < 2){
				t += Time.deltaTime/switchViewDurationTime;
				_Earth.transform.rotation = Quaternion.Slerp(
					_Earth.transform.rotation,
					initRot_earth,
					speed * Time.deltaTime);
			}else{ 
				isResettingRotation = false;
				
			}//if t<1
		}//if isResettingRotation
		


		//Actual Sizeの設定のときのみトグル表示
		if(
			(_Earth.transform.position == initPos_earth) && 
			(_Earth.transform.rotation == initRot_earth) && 

			(_Moon.transform.position == new Vector3(30, 0, 0)) && 
			(_Moon.transform.rotation == initRot_moon) && 
			
			(_Sun.transform.position == new Vector3(11700, 0, 0)) && 
			(_Sun.transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0)) )
		){
			
			ActualSizeToggle.isOn  = true;

		}else{ 

			ActualSizeToggle.isOn  = false;
		}



        if(isReset){
            ResetPosition();
            isReset = false;
        }




	}//Update


    public void switchIsActualSizeFlg_forToggle(){
        if(ActualSizeToggle.isOn){
            isActualSize = true;
        }else{
            isActualSize = false;
        }
    }
    public void switchIsResetFlg_forToggle(){
        if(ResetRotationToggle.isOn){
            isReset = true;
        }else{
            isReset = false;
        }
    }


    //オブジェクトを初期位置に戻す関数
	void ResetPosition(){ 
        Debug.Log("Reset Position and Rotation");

		//ちなみに月の位置は: 地球直径1, 月の直径0.27, 月までの距離適当に2とした場合, 5.14度傾いているので
		//三角関数的なことをすると高さ辺が出てくる 0.18
		_Moon.transform.position = initPos_moon;
		_Moon.transform.rotation = initRot_moon;
		_Earth.transform.position = initPos_earth;
		_Earth.transform.rotation = initRot_earth;
		_Sun.transform.position = initPos_sun;
		_Sun.transform.rotation = initRot_sun;

        changingViewTransit = -1;

		
        //switchIsResetFlg_forToggle();
        //GameObject.Find("ResetRotationToggle").GetComponent<controlMenu>().isToggleOn = false;
        //ResetRotationToggle.isOn = false;
        //isReset = false;
	}


	public void ResetRotation() {
		//_Earth.transform.rotation = initRot_earth;
        changingViewTransit = -1;
		isResettingRotation = true;
	}


    //オブジェクトを実際の縮尺にする関数
	void SetPositionToActualSize(){ 
		/*
		太陽を1としたときの縮尺
		- 太陽の大きさ https://kids.gakken.co.jp/kagaku/110ban/text/1073.html
		- 太陽から地球までの距離15000万km / 地球の直径12800km = 11700
		- 月から地球までの距離384400km / 地球の直径12800km = 30
		*/

		//地球はデフォルトで(0, 0, 0)設置なのでinitに戻す
		_Earth.transform.position = initPos_earth;
		_Earth.transform.rotation = initRot_earth;

		//月から地球までの距離に設定
		_Moon.transform.position = new Vector3(30, 0, 0);
		_Moon.transform.rotation = initRot_moon;

		//太陽から地球までの距離と太陽の大きさに設定
		_Sun.transform.position = new Vector3(11700, 0, 0);
		_Sun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		//_Sun.transform.localScale = new Vector3(109.25f, 109.25f, 109.25f);

		Debug.Log("Set position to actual size");
	}



}//class